using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Extensions;
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

        #endregion Properties

        #region Construction

        public FileDataService(string BaseFolder, IEndpointRegister EndpointRegister)
        {
            this.EndpointRegister = EndpointRegister;
            this.baseFolder = BaseFolder;
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
        }
        #endregion AddOrReplaceAuthentication

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value)
        {
            bool result = false;

            string filename = this.getFilename(Action);

            if (this.checkAndCreateDirs(filename))
            { 
                if (File.Exists(filename))
                {
                    string text = await File.ReadAllTextAsync(filename, Encoding.UTF8);
                    var content = JsonConvert.DeserializeObject<IEnumerable<T>>(text);
                    if (content.ToList().Contains(Value))
                    {
                        //Update
                        content = content.Replace(content.ToList().IndexOf(Value), Value);
                    }
                    else
                    {
                        //Append
                        content = content.Add(Value);
                    }
                    var newContent = JsonConvert.SerializeObject(content);
                    await File.WriteAllTextAsync(filename, newContent);

                    result = true;
                }
            }

            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters)
        {
            bool result = false;

            string filename = this.getFilename(Action);

            if (File.Exists(filename))
            {
                string text = await File.ReadAllTextAsync(filename, Encoding.UTF8);
                var content = JsonConvert.DeserializeObject<IEnumerable<T>>(text);

                int index = -1;

                foreach (T item in content)
                {
                    index++;
                    string itemText = JsonConvert.SerializeObject(item);
                    if (itemText.Contains(Parameters[0].ToString()))
                    {
                        break;  //TODO: think of logic here, focussing on id, perhaps a beginswith is better
                    }
                }

                content = content.Remove(index);

                var newContent = JsonConvert.SerializeObject(content);
                await File.WriteAllTextAsync(filename, newContent);

                result = true;
            }

            return result;
        }
        #endregion DeleteAsync

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters)
        {
            T result = default;

            string filename = this.getFilename(Action);

            if (File.Exists(filename))
            {
                string text = File.ReadAllText(filename, Encoding.UTF8);
                result = JsonConvert.DeserializeObject<T>(text);
            }

            return result;
        }
        #endregion GetAsync

        #region getFilename
        private string getFilename(string Action)
        {
            string result = Path.Join(Directory.GetCurrentDirectory(), Path.Join(this.baseFolder, this.EndpointRegister.GetEndpoint(Action)));
            return result;
        }
        #endregion getFilename

        #region checkAndCreateDirs
        private bool checkAndCreateDirs(string Filename)
        {
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

        #region dispose
        protected override void dispose()
        {
            this.EndpointRegister = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
