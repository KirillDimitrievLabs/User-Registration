using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;
using YouTrackSharp;
using YouTrackSharp.Management;
using Newtonsoft.Json;
using UserRegistration.Components.PluginSystem;
using System.Net.Http;

namespace YouTrack
{
    public class YouTrack : IDestination
    {
        //public string ConnectionType { get => nameof(TokenAuth); }
        public string Name { get => nameof(YouTrack); }
        private BearerTokenConnection Connection { get; set; }
        private string Token { get; set; }

        public YouTrack(Dictionary<object, object> Config)
        {
            Token = Config["Token"].ToString();
            Connection = new BearerTokenConnection(Config["Url"].ToString(), Config["Token"].ToString());
        }

        public async Task<List<string>> ReadUsers()
        {
            var exsistingUsers = await Connection.CreateUserManagementService().GetUsers();
            List<string> exsistingUserStrList = new List<string>();

            foreach (var exsistingUser in exsistingUsers)
            {
                exsistingUserStrList.Add(exsistingUser.FullName);
            }
            return exsistingUserStrList;
        }

        public async Task<List<string>> ReadGroups()
        {
            var client = await Connection.GetAuthenticatedHttpClient();

            var response = await client.GetAsync($"rest/admin/group");
            var groupJson = JsonConvert.DeserializeObject<List<Group>>(
                await response.Content.ReadAsStringAsync());

            List<string> exsistingUserStr = new List<string>();
            foreach (var groupName in groupJson)
            {
                exsistingUserStr.Add(groupName.Name);
            }

            return exsistingUserStr;
        }

        public async Task Save(UserDestinationModel userToSave)
        {
            await Connection
                .CreateUserManagementService()
                .CreateUser(userToSave.Login, userToSave.FullName, userToSave.Email, "", "Password1");
            
            foreach (var userGroup in userToSave.Groups)
            {
                await Connection
                    .CreateUserManagementService()
                    .AddUserToGroup(userToSave.Login, userGroup);
            }
        }

        public async Task Delete(string userLogin)
        {
            HttpClient httpClient = new HttpClient();
            char ch = '"';
            var userRequest = new HttpRequestMessage(new HttpMethod("GET"), $"https://dimitrievdiplom.myjetbrains.com/hub/api/rest/users?fields=id&query={ch+userLogin.Replace(" ","%20")+ch}");
            userRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Token}");
            var user = await httpClient.SendAsync(userRequest);

            Root root = JsonConvert.DeserializeObject<Root>(await user.Content.ReadAsStringAsync());

            HttpClient httpClient1 = new HttpClient();
            
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("DELETE"), $"https://dimitrievdiplom.myjetbrains.com/hub/api/rest/users/{root.users[0].id}");
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Token}");
            
            var response = await httpClient1.SendAsync(request);
        }
    }
    public class User
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public int skip { get; set; }
        public int top { get; set; }
        public int total { get; set; }
        public List<User> users { get; set; }
    }
}