using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Extensions;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Services.DataService.FileDataService
{
    public class FileDataService : BaseObject, IDataService
    {
        #region Properties
        public IEndpointRegister EndpointRegister { get; set; }

        private string baseFolder;
        private ILogger logger;

        #endregion Properties

        #region Construction

        public FileDataService(string BaseFolder, IEndpointRegister EndpointRegister, ILogger Logger = null)
        {
            this.EndpointRegister = EndpointRegister;
            this.baseFolder = BaseFolder;
            this.logger = Logger;
        }

        #endregion Construction

        #region Methods

        #region AddOrReplaceAuthentication: does nothing in this case.
        /// <summary>
        /// does nothing in this case.
        /// </summary>
        /// <param name="Authenticationstring"></param>
        public void AddOrReplaceAuthentication(string Authenticationstring)
        {
            //do nothing.
            this.logger.Log("FileDataService.AddOrReplaceAuthentication: did nothing.");
        }
        #endregion AddOrReplaceAuthentication

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            IEnumerable<T> result = new List<T>();
            foreach (T value in await this.GetAllAsync<T>(Action))
            {
                if (FilterMethod(value))
                {
                    result.Add(value);
                }
            }
            return result;
        }
        #endregion GetAllAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value) where T : IBaseObject
        {
            this.logger?.Log($"FileDataService.CreateOrUpdateAsync: entered");
            bool result = false;

            result = await this.CreateOrUpdateAsync<T>(Action, Value, x => x.Id.Equals(Value.Id));

            this.logger?.Log($"FileDataService.CreateOrUpdateAsync: exited with result '{result}'");
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, IEnumerable<T> Values) where T : IBaseObject
        {
            bool result = true;
            //TODO: optimize
            foreach (T item in Values)
            {
                result &= await this.CreateOrUpdateAsync<T>(Action, item);
            }
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            this.logger?.Log($"FileDataService.DeleteAsync: entered");
            bool result = false;

            result = await this.DeleteAsync<T>(Action, x => x.Id.Equals(Parameters[0].ToString()));

            this.logger?.Log($"FileDataService.DeleteAsync: exited, result '{result}'");
            return result;
        }
        #endregion DeleteAsync

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            this.logger?.Log($"FileDataService.GetAsync: entered");
            T result = default;

            string filename = this.getFilename(Action);
            this.logger?.Log($"FileDataService.GetAsync: generated file name '{filename}'");

            //TODO: think about, we are having issues when rturning a list of something / working with a list of something
            if (File.Exists(filename))
            {
                try
                {
                    this.logger?.Log($"FileDataService.GetAsync: file exists");
                    string text = File.ReadAllText(filename, Encoding.UTF8);
                    List<T> listOfAllAndEverything = JsonConvert.DeserializeObject<IEnumerable<T>>(text).ToList();
                    if (listOfAllAndEverything.Count() == 0)
                    {
                        //found nothing
                    }
                    else if (listOfAllAndEverything.Count() == 1)
                    {
                        result = listOfAllAndEverything[0];
                    }
                    else if (listOfAllAndEverything.Count() > 1)
                    {
                        result = listOfAllAndEverything.Where(x => x.Id.Equals(Parameters[0].ToString())).FirstOrDefault();
                    }
                    else
                    {
                        //won't work out here
                        this.logger?.Log($"FileDataService.GetAsync: found less than zero entries");
                    }
                }
                catch (Exception ex)
                {
                    this.logger?.Log($"FileDataService.GetAsync: raised exception '{ex.Message}' - '{ex.StackTrace}'", Contracts.Logger.Enums.LogLevel.Error);
                }
            }
            else
            {
                this.logger?.Log($"FileDataService.GetAsync: file does not exist", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"FileDataService.GetAsync: exited, result '{result}'");
            return result;
        }
        #endregion GetAsync

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            this.logger?.Log($"FileDataService.GetAllAsync: entered");
            IEnumerable<T> result = default;

            string filename = this.getFilename(Action);
            this.logger?.Log($"FileDataService.GetAllAsync: generated file name '{filename}'");

            //TODO: think about, we are having issues when rturning a list of something / working with a list of something
            if (File.Exists(filename))
            {
                try
                {
                    this.logger?.Log($"FileDataService.GetAllAsync: file exists");
                    string text = File.ReadAllText(filename, Encoding.UTF8);
                    result = JsonConvert.DeserializeObject<IEnumerable<T>>(text);
                }
                catch (Exception ex)
                {
                    this.logger?.Log($"FileDataService.GetAllAsync: raised exception '{ex.Message}' - '{ex.StackTrace}'", Contracts.Logger.Enums.LogLevel.Error);
                }
            }
            else
            {
                this.logger?.Log($"FileDataService.GetAllAsync: file does not exist", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"FileDataService.GetAllAsync: exited, result '{result}'");
            return result;
        }
        #endregion GetAllAsync

        #region getFilename
        private string getFilename(string Action)
        {
            string result = Path.Join(PathHelper.GetProgramPath(), Path.Join(this.baseFolder, this.EndpointRegister.GetEndpoint(Action))).ToLower().Replace("file:\\", "");
            return result;
        }
        #endregion getFilename

        #region checkAndCreateDirs
        private bool checkAndCreateDirs(string Filename)
        {
            //TODO: add logging
            bool result = false;

            string path = Path.GetDirectoryName(Filename);

            if (Directory.Exists(path))
            {
                result = true;
            }
            else
            {
                var info = Directory.CreateDirectory(path);
                if (info.Exists)
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion checkAndCreateDirs

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): entered");
            bool result = false;

            string filename = this.getFilename(Action);
            this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): generated file name '{filename}'");
            if (this.checkAndCreateDirs(filename))
            {
                this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): checked and created dirs successfully");
                if (File.Exists(filename))
                {
                    try
                    {
                        this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): file exists");
                        string text = await File.ReadAllTextAsync(filename, Encoding.UTF8);
                        var content = JsonConvert.DeserializeObject<IEnumerable<T>>(text);
                        if (content != null)
                        {
                            if (content.Any(FilterMethod))
                            {
                                //Update
                                List<Guid> idsToReplace = new List<Guid>();
                                foreach (T value in content)
                                {
                                    if (FilterMethod(value))
                                    {
                                        idsToReplace.Add(value.Id);
                                    }
                                }

                                if (idsToReplace.Count > 0)
                                {
                                    foreach (Guid idToReplace in idsToReplace)
                                    {
                                        content = content.RemoveAll(x => x.Id.Equals(idToReplace));
                                    }
                                }

                                content = content.Add(Value);
                                //TODO: check later
                                this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): updated content");
                            }
                            else
                            {
                                //Append
                                content = content.Add(Value);
                                this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): added content");
                            }
                        }
                        else
                        {
                            content = new List<T>() { Value };
                        }
                        var newContent = JsonConvert.SerializeObject(content);
                        await File.WriteAllTextAsync(filename, newContent);

                        result = true;
                    }
                    catch (Exception ex)
                    {
                        this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): raised exception '{ex.Message}' - '{ex.StackTrace}'", Contracts.Logger.Enums.LogLevel.Error);
                    }
                }
                else
                {
                    //Create File, append
                    this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): file does not exist", Contracts.Logger.Enums.LogLevel.Warning);
                    using (StreamWriter swFile = File.CreateText(filename))
                    {
                        this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): file created");
                        string content = JsonConvert.SerializeObject(new List<T>() { Value });
                        await swFile.WriteAsync(content);
                    }
                    this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): file filled");
                    result = true;
                }
            }
            else
            {
                this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): could not check and create dirs", Contracts.Logger.Enums.LogLevel.Error);
            }
            this.logger?.Log($"FileDataService.CreateOrUpdateAsync (delegate): exited, result '{result}'");
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            this.logger?.Log($"FileDataService.DeleteAsync (delegate): entered");
            bool result = false;

            string filename = this.getFilename(Action);
            this.logger?.Log($"FileDataService.DeleteAsync (delegate): generated file name '{filename}'");

            if (File.Exists(filename))
            {
                try
                {
                    this.logger?.Log($"FileDataService.DeleteAsync (delegate): file exists");
                    string text = await File.ReadAllTextAsync(filename, Encoding.UTF8);
                    var content = JsonConvert.DeserializeObject<IEnumerable<T>>(text);

                    int index = -1;

                    foreach (T item in content)
                    {
                        index++;

                        //given: Parameters[0] id the ID
                        if (FilterMethod.Invoke(item))
                        {
                            break;  //TODO: think of logic here, focussing on id, perhaps a beginswith is better
                        }
                    }

                    content = content.Remove(index);

                    var newContent = JsonConvert.SerializeObject(content);
                    await File.WriteAllTextAsync(filename, newContent);

                    result = true;
                }
                catch (Exception ex)
                {
                    this.logger?.Log($"FileDataService.DeleteAsync (delegate): raised exception '{ex.Message}' - '{ex.StackTrace}'", Contracts.Logger.Enums.LogLevel.Error);
                }
            }
            else
            {
                this.logger?.Log($"FileDataService.DeleteAsync (delegate): file does not exist", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"FileDataService.DeleteAsync (delegate): exited, result '{result}'");
            return result;
        }
        #endregion DeleteAsync

        #region dispose
        protected override void dispose()
        {
            this.EndpointRegister = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
