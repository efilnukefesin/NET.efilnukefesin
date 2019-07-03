using System;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace Builder
{
    class Program
    {
        #region Properties

        private const bool areTestsRequired = false;

        private const string build = "build";
        private const string test = "test";
        private const string pack = "pack";
        private const string @default = "default";

        //private const string slnPath = "../../../../../";
        //private const string srcPath = "../../../../../src";
        //private const string testPath = "../../../../../test";  //original: "./test"
        //private const string artifactsPath = "../../../../artifacts";
        private const string slnPath = ".";
        private const string srcPath = "./src";
        private const string testPath = "./test";  //original: "./test"
        private const string artifactsPath = "./artifacts";

        #endregion Properties

        #region Methods

        #region Main
        static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            var modeOption = app.Option<string>("--mode", "determining if beta or not", CommandOptionType.SingleValue);

            cleanArtifacts();

            app.OnExecute(() =>
            {
                bool isBeta = false;
                string modeValue = modeOption.Value();
                if (!string.IsNullOrEmpty(modeValue))
                {

                    if (modeValue.ToLower().Equals("beta"))
                    {
                        //value "beta"
                        isBeta = true;
                    }
                    else
                    {
                        //value something else than "beta"
                    }
                }
                else
                {
                    //no mode value
                }

                Target(build, () =>
                {
                    System.Console.WriteLine($"{build}: Looking for SLN...");
                    //var s = Path.GetFullPath(SrcPath);
                    var solution = Directory.GetFiles(slnPath, "*.sln", SearchOption.TopDirectoryOnly).First();
                    Run("dotnet", $"build {solution} -c Release");
                });
                Target(test, DependsOn(build), () =>
                {
                    System.Console.WriteLine($"{test}: Looking for Tests...");
                    try
                    {
                        var s = Path.GetFullPath(testPath);
                        var tests = Directory.GetFiles(testPath, "*.csproj", SearchOption.AllDirectories);

                        foreach (var test in tests)
                        {
                            System.Console.WriteLine($"{Program.test}: Adding Test '{test}'");
                            Run("dotnet", $"test {test} -c Release --no-build");
                        }
                    }
                    catch (System.IO.DirectoryNotFoundException ex)
                    {
                        if (areTestsRequired)
                        {
                            throw new Exception($"{test}: No tests found: {ex.Message}");
                        };
                    }
                    System.Console.WriteLine($"{test}: Stopped looking for Tests");
                });
                Target(pack, DependsOn(test), () =>
                {
                    var projects = Directory.GetFiles(srcPath, "*.csproj", SearchOption.AllDirectories);

                    foreach (var project in projects)
                    {
                        System.Console.WriteLine($"{pack}: Adding Project '{project}'");
                        
                        if (!isBeta)
                        {
                            Run("dotnet", $"pack {project} -c Release -o {artifactsPath} --no-build");  //TODO: check syntax - what am I doing here?
                        }
                        else
                        {
                            //do the beta pack
                            //--version-suffix is only working when you don't use Version in your csproj but VersionPrefix
                            Run("dotnet", $"pack {project} -c Release -o {artifactsPath} --no-build --version-suffix beta");  //TODO: check syntax - what am I doing here?
                        }
                    }
                });
                Target(@default, DependsOn(test, pack));
                RunTargetsAndExit(app.RemainingArguments);
            });

            app.Execute(args);
        }
        #endregion Main

        #region cleanArtifacts: deletes the artifact directory upfront
        /// <summary>
        /// deletes the artifact directory upfront
        /// </summary>
        private static void cleanArtifacts()
        {
            Directory.CreateDirectory($"{artifactsPath}");

            foreach (var file in Directory.GetFiles($"{artifactsPath}"))
            {
                File.Delete(file);
            }
        }
        #endregion cleanArtifacts

        #endregion Methods
    }
}
