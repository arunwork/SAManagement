using Newtonsoft.Json;
using SellerAccountManagement.Model.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerAccountManagement.Helpers
{
    public static class LocalStorageHelper
    {
        #region Fields

        public const string UserFile = "Users.json";

        #endregion


        #region Methods

        private static void SaveData(string fileName, string jsonString)
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain,
                                                                       typeof(System.Security.Policy.Url), typeof(System.Security.Policy.Url)))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.Create, isoStore))
                {
                    using (StreamWriter str = new StreamWriter(isoStream))
                    {
                        str.Write(jsonString);
                    }
                }
            }
        }

        async private static Task<string> GetData(string fileName)
        {
            string jsonString = "";
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain,
                                                                       typeof(System.Security.Policy.Url), typeof(System.Security.Policy.Url)))
            {
                if (isoStore.FileExists(fileName))
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.Open, isoStore))
                    {
                        using (StreamReader str = new StreamReader(isoStream))
                        {
                            jsonString = await str.ReadToEndAsync();
                        }
                    }
                }
            }
            return jsonString;
        }

        async public static Task InsertUser(User user)
        {
            IList<User> users = new List<User>();
            var existingUsersJson = await GetData(UserFile);
            if (string.IsNullOrEmpty(existingUsersJson))
            {
                users.Add(user);
            }
            else
            {
                users = JsonConvert.DeserializeObject<List<User>>(existingUsersJson);
                users.Add(user);
            }
            var usersJsonString = JsonConvert.SerializeObject(users);
            SaveData(UserFile, usersJsonString);
        }

        async public static Task<IList<User>> GetUsers()
        {
            var users = new List<User>();
            var existingUsersJson = await GetData(UserFile);
            if (!string.IsNullOrEmpty(existingUsersJson))
                users = JsonConvert.DeserializeObject<List<User>>(existingUsersJson);

            return users;
        }
        #endregion
    }
}
