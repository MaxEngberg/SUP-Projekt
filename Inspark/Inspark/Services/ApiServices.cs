﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Inspark.Models;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Inspark.Helpers;
using Inspark.Views;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
 using System.Runtime.InteropServices.ComTypes;
 using System.Security.Cryptography.X509Certificates;
using MvvmHelpers;

namespace Inspark.Services
{
    public class ApiServices
    {
        // Connection string for calls to our web api
        private string ConnectionString = "http://oruinsparkwebapi.azurewebsites.net/";

        //All API calls for chat and messanges
        //All API calls for chat and messanges
        //All API calls for chat and messanges

        public async Task<bool> AddViewToChat(View view, int ChatId)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(view);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/View/AddViewToChat/" + ChatId.ToString() + "/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ClearViews(int ChatId)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/Chat/RemoveUsersFromChatViewed/" + ChatId.ToString() + "/", null);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> CreatePrivateChat(string UserId_1, string UserId_2)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/Chat/CreateChat/" + UserId_1 + "/" + UserId_2 + "/", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PostPrivateMessage(int chatId, Message message)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(message);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/PrivateMessage/AddPrivateMessage/" + chatId.ToString() + "/", content);
            return response.IsSuccessStatusCode;
        }

        // All API calls for GroupChat
        // All API calls for GroupChat
        // All API calls for GroupChat

        public async Task<bool> CreateGroupChat(int groupId, GroupChat chat)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(chat);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/GroupChat/AddGroupChat/" + groupId.ToString() + "/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PostGroupMessage(int chatId, Message message)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(message);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/GroupMessage/AddGroupMessage/" + chatId.ToString() + "/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddViewToGroupChat(View view, int ChatId)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(view);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/View/AddViewToGroupChat/" + ChatId.ToString() + "/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ClearGroupViews(int ChatId)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/GroupChat/RemoveUsersFromGroupChatViewed/" + ChatId.ToString() + "/", null);
            return response.IsSuccessStatusCode;
        }



        //all api calls for IntroCode
        //all api calls for IntroCode
        //all api calls for IntroCode



        public async Task<bool> introCode(string intro)
        {
			var user = Settings.UserId;
            var client = new HttpClient();
			var response = await client.PostAsync(ConnectionString + "api/Group/AddUserToGroupByCode/"+ intro + "/"+ user + "/", null);
            return response.IsSuccessStatusCode;
        }


        
        // All API calls for Events.
        // All API calls for Events.
        // All API calls for Events.

        public async Task<bool> DeleteEvent(int eventId)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync(ConnectionString + "api/event/" + eventId + "/");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeEvent(Event events)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(events);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/Event/EditEvent/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateEvent(string tile, string location, DateTime date, string desc, string senderId)
        {
            var client = new HttpClient();
            var model = new Event()
            {
                Title = tile,
                Location = location,
                TimeForEvent = date,
                Description = desc,
                SenderId = senderId
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/Event/AddEvent/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<Event>> GetAllEvents()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/Event");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<Event>>(result);
            return list;
        }

        public async Task<ObservableCollection<Event>> GetAllEventsAttending()
        {
            var userid = Settings.UserId;
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/AttendingEvent/GetAttendingEventsOfUser/"+userid +"/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<Event>>(result);
            return list;
        }





        // All API calls for GroupEvents.
        // All API calls for GroupEvents.
        // All API calls for GroupEvents.

        public async Task<ObservableCollection<User>> AttendingsGroupEvent(int eventId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/AttendingGroupEvent/GetAttendingUsers/" + eventId + "/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<User>>(result);
            return list;
        }

        public async Task<Group> GetGroup(int id)
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(ConnectionString + "api/Group/" + id.ToString() + "/");
            var group = JsonConvert.DeserializeObject<Group>(json);
            return group;
        }

        public async Task<bool> DeleteGroupEvent(int eventId)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync(ConnectionString + "api/groupevent/" + eventId + "/");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeGroupEvent(GroupEvent groupEvents)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(groupEvents);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/GroupEvent/EditGroupEvent/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<GroupEvent>> GetAllGroupEvents()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/GroupEvent");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<GroupEvent>>(result);
            return list;
        }

        public async Task<bool> AttendingGroupEvent(AttendingGroupEventModel model)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/AttendingGroupEvent/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateGroupEvent(GroupEvent groupEvent)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(groupEvent);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/GroupEvent/AddGroupEvent/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<GroupEvent>> GetAllGroupEventsAttending()
        {
            var userId = Settings.UserId;
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/AttendingGroupEvent/GetAttendingGroupEventsOfUser/"+ userId +"/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<GroupEvent>>(result);
            return list;
        }


        // ALL API calls for Score
        // ALL API calls for Score
        // ALL API calls for Score

        public async Task<ObservableCollection<Score>> GetAllScore()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/result");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<Score>>(result);
            return list;
        }

        public async Task<bool> ChangeScore(Score score)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(score);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/result/editresult/", content);
            return response.IsSuccessStatusCode;
        }




        // All API calls for Groups.
        // All API calls for Groups.
        // All API calls for Groups.
        
        public async Task<int> GetGroupIdByName(string groupName)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/group/GetIdByName/" + groupName + "/");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                int id = JsonConvert.DeserializeObject<int>(result);
                return id;
            } else
            {
                return -1;
            }

        }


        public async Task<ObservableCollection<Group>> GetAllGroups()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/group");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<Group>>(result);
            return list;
        }

        public async Task<bool> AddUserToGroup(int groupId, string userId)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/group/AddUserToGroup/" + groupId + "/" + userId + "/", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync(ConnectionString + "api/group/" + groupId + "/");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserFromGroup(int groupId, string userId)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/Group/RemoveUserFromGroup/" + groupId + "/" + userId + "/", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeGroup(Group group)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(group);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/Group/EditGroup/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateGroup(Group group)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(group);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/Group/AddGroup", content);
            return response.IsSuccessStatusCode;
        }







        // All API calls for NewsPost.
        // All API calls for NewsPost.
        // All API calls for NewsPost.

        public async Task<bool> EditNewsPost(EditPostModel post)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/NewsPost/EditNewsPost", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteNewsPost(int id)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync(ConnectionString + "api/NewsPost/" + id.ToString());
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateNewsPost(NewsPost post)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/NewsPost/AddNewsPost/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<NewsPost>> GetAllNewsPosts()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/newspost");
            var success = response.IsSuccessStatusCode;
            if (success)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<NewsPost>>(result);
                return list;
            }
            else
            {
                var list = new ObservableCollection<NewsPost>();
                return list;
            }
        }







        // All API calls for GroupPost.
        // All API calls for GroupPost.
        // All API calls for GroupPost.

        public async Task<ObservableCollection<GroupPost>> GetAllGroupPosts()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/grouppost");
            var success = response.IsSuccessStatusCode;
            if (success)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<GroupPost>>(result);
                return list;
            }
            else
            {
                var list = new ObservableCollection<GroupPost>();
                return list;
            }
        }

        public async Task<bool> EditGroupPost(EditPostModel post)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/GroupPost/EditGroupPost", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGroupPost(int id)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync(ConnectionString + "api/GroupPost/" + id.ToString());
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateGroupPost(GroupPost post)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/GroupPost/AddGroupPost", content);

            return response.IsSuccessStatusCode;
        }








        // All API calls for Login and Register. 
        // All API calls for Login and Register. 
        // All API calls for Login and Register. 

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var keyValue = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("UserName", userName),
                new KeyValuePair<string, string>("Password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, ConnectionString+"token");
            request.Content = new FormUrlEncodedContent(keyValue);
            
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var jwt = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
            var accessToken = jwtDynamic.Value<string>("access_token");
            var accessTokenExpires = jwtDynamic.Value<DateTime>(".expires");
            Settings.AccessTokenExpires = accessTokenExpires;
            Settings.AccessToken = accessToken;
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel model)
        {
            var client = new HttpClient();
            var token = Settings.AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(ConnectionString + "api/Account/ChangePassword", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString+"api/account/register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditUser(EditUserModel model)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString+"api/User/EditUser", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<User>> GetAllUsers()
        {
           var client = new HttpClient();
           var response = await client.GetAsync(ConnectionString+"api/user/");
           response.EnsureSuccessStatusCode();
           var result = await response.Content.ReadAsStringAsync();
           var list = JsonConvert.DeserializeObject<ObservableCollection<User>>(result);
           return list;
        }

        public async Task<ObservableCollection<Section>> GetAllSections()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString+"api/section/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<Section>>(result);
            return list;
        }

        public async Task<User> GetLoggedInUser()
        {
            var userName = Settings.UserName;
            var client = new HttpClient();
            var json = await client.GetStringAsync(ConnectionString+"api/user/getbyusername/" + userName + "/");
            var user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }

        public async Task<User> GetSingelUser(string id)
        { 
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            var json = await client.GetStringAsync(ConnectionString+"api/getusername/" + id);
            var user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }

        public async Task<bool> AddUserToNewsPostViews(int postId, string userName)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/NewsPost/AddUserToNewsPostViewed/" + postId + "/" + userName + "/", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddUserToGroupPostViews(int postId, string userName)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(ConnectionString + "api/GroupPost/AddUserToGroupPostViewed/" + postId + "/" + userName + "/", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AttendingEvent(AttendingEventModel model)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ConnectionString + "api/AttendingEvent/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<Group>> GetAllGroupsByUserId()
        {
            var userId = Settings.UserId;
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/Group/GetGroupsFromUser/"+userId+"/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<Group>>(result);
            return list;
        }

        public async Task<ObservableCollection<User>> AttendingsEvent(int eventId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ConnectionString + "api/AttendingEvent/GetAttendingUsers/"+eventId+"/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<User>>(result);
            return list;
        }
    }
}
