using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using IronBoard.RBWebApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace IronBoard.RBWebApi.Application
{
   class RbHttpClient : IRbClient
   {
      private readonly RestClient _client;
      private readonly ReviewBoardRc _config;
      private string _authCookie;
      private static Dictionary<string, User> _userNameToUser;
      private static Dictionary<string, UserGroup> _groupNameToGroup;
      private static string _myName;

      public event Action<NetworkCredential> AuthenticationRequired;
      public event Action<string> AuthCookieChanged;

      public RbHttpClient(string projectRootFolder, string authCookie)
      {
         if (projectRootFolder == null) throw new ArgumentNullException("projectRootFolder");

         _config = new ReviewBoardRc(projectRootFolder);
         if(_config.Uri == null) throw new ArgumentException("config file doesn't contain ReviewBoard server url");
         _client = new RestClient(new Uri(_config.Uri, "api").ToString());
         _authCookie = authCookie;
      }

      public Uri ServerUri { get { return _config.Uri; } }

      public string AuthCookie
      {
         get { return _authCookie; }
         set
         {
            if(value != _authCookie)
            {
               _authCookie = value;
               if (AuthCookieChanged != null) AuthCookieChanged(value);
            }
         }
      }

      private RestRequest CreateRequest(string resource, Method method)
      {
         var request = new RestRequest(resource) {Method = method};
         if(AuthCookie != null) request.AddCookie("rbsessionid", AuthCookie);
         return request;
      }

      private RestRequest CreateRequest(Uri uri, Method method)
      {
         if (uri.IsAbsoluteUri)
         {
            string resource = uri.ToString().Substring(_client.BaseUrl.Length);
            return CreateRequest(resource, method);
         }

         return CreateRequest(uri.ToString(), method);
      }

      private RestResponse Execute(RestRequest request, int expectedCode)
      {
         var response = _client.Execute(request) as RestResponse;
         while (response.StatusCode == HttpStatusCode.Unauthorized)
         {
            var cred = new NetworkCredential();
            if (AuthenticationRequired != null) AuthenticationRequired(cred);
            if(string.IsNullOrEmpty(cred.UserName) || string.IsNullOrEmpty(cred.Password))
            {
               throw new AuthenticationException("username and password required");
            }
            SignRequest(request, cred);
            response = _client.Execute(request) as RestResponse;
            PickUpCookie(response);
         }

         if ((int)response.StatusCode != expectedCode)
         {
            string msg;

            if ((int)response.StatusCode != 0)
            {
               msg = string.Format("code: {0}\r\ndescription: {1}\r\nresponse:{2}\r\n",
                                   response.StatusCode,
                                   response.StatusDescription,
                                   response.Content);
            }
            else
            {
               msg = response.ErrorMessage;
            }

            throw new InvalidOperationException(msg);
         }

         return response;
      }

      public string MyName
      {
         get
         {
            if (string.IsNullOrEmpty(_myName))
            {
               var request = CreateRequest("session/", Method.GET);
               RestResponse response = Execute(request, 200);
               if (response != null)
               {
                  JObject jo = JObject.Parse(response.Content);
                  JObject session = (JObject)jo["session"];

                  JObject links = (JObject) session["links"];
                  JObject user = (JObject) session["user"] ?? (JObject) links["user"];
                  if (user == null) throw new ApplicationException("cannot find user object in response");
                  _myName = user.Value<string>("title");
               }

            }

            return _myName;
         }
      }

      public IEnumerable<Repository> GetRepositories()
      {
         var result = new List<Repository>();
         var request = CreateRequest("repositories/", Method.GET);
         RestResponse response = Execute(request, 200);
         if(response != null)
         {
            JObject jo = JObject.Parse(response.Content);
            JArray repos = jo["repositories"] as JArray;
            foreach(JObject r in repos)
            {
               result.Add(new Repository(
                  r.Value<string>("id"),
                  r.Value<string>("name"),
                  r.Value<string>("tool"),
                  r.Value<string>("path")));
            }
         }
         return result;
      }

      public IEnumerable<UserGroup> GetGroups()
      {
         var request = CreateRequest("groups/", Method.GET);
         var response = Execute(request, 200);
         if (response != null)
         {
            JObject jo = JObject.Parse(response.Content);
            var groups = jo["groups"] as JArray;
            if(groups != null)
            {
               var result = new List<UserGroup>();
               foreach(JObject g in groups)
               {
                  result.Add(new UserGroup(
                     g.Value<long>("id"),
                     g.Value<string>("name"),
                     g.Value<string>("display_name"),
                     g.Value<string>("uri"),
                     g.Value<bool>("visible"),
                     g.Value<bool>("invite_only"),
                     g.Value<string>("mailing_list")));
               }
               return result;
            }
         }

         return null;
      }

      public IEnumerable<User> GetUsers()
      {
         var request = CreateRequest("users/?max-results=1000", Method.GET);
         var response = Execute(request, 200);
         if(response != null)
         {
            JObject jo = JObject.Parse(response.Content);
            var users = jo["users"] as JArray;
            if(users != null)
            {
               var result = new List<User>();
               foreach(JObject u in users)
               {
                  result.Add(new User(
                     u.Value<long>("id"),
                     u.Value<string>("username"),
                     u.Value<string>("first_name"),
                     u.Value<string>("last_name"),
                     u.Value<string>("fullname"),
                     u.Value<string>("email"),
                     u.Value<string>("url")));
               }
               return result;
            }
         }
         return null;
      }

      private void ValidateCaches()
      {
         if(_userNameToUser == null)
         {
            IEnumerable<User> allUsers = GetUsers();
            if(allUsers != null)
            {
               _userNameToUser = new Dictionary<string, User>();
               var aul = allUsers.ToList();
               foreach(User u in aul)
               {
                  _userNameToUser[u.InternalName] = u;
               }
            }
         }

         if(_groupNameToGroup == null)
         {
            IEnumerable<UserGroup> allGroups = GetGroups();
            if(allGroups != null)
            {
               _groupNameToGroup = new Dictionary<string, UserGroup>();
               var agl = allGroups.ToList();
               foreach(UserGroup g in agl)
               {
                  _groupNameToGroup[g.InternalName] = g;
               }
            }
         }
      }

      private void SignRequest(RestRequest request, NetworkCredential creds)
      {
         if (creds == null || creds.UserName == null || creds.Password == null) throw new ArgumentNullException("creds");
         string auth = creds.UserName + ":" + creds.Password;
         auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(auth));
         request.AddHeader(HttpRequestHeader.Authorization.ToString(), auth);
      }

      private void PickUpCookie(IRestResponse response)
      {
         var cookie = response.Cookies.FirstOrDefault(c => c.Name == "rbsessionid");
         if (cookie == null) throw new AuthenticationException("invalid username or password");
         AuthCookie = cookie.Value;
      }

      public void Authenticate(NetworkCredential creds)
      {
         var request = CreateRequest("repositories/", Method.GET);
         SignRequest(request, creds);
         var response = _client.Execute(request);
         PickUpCookie(response);
      }

      private Uri ParseLink(JObject links, string linkName)
      {
         if(links != null && linkName != null)
         {
            JObject linkObj = links[linkName] as JObject;
            if(linkObj != null)
            {
               string href = linkObj.Value<string>("href");
               if(href != null) return new Uri(href);
            }
         }
         return null;
      }

      private void ParseReview(string response, Review review, string entityTag, bool parseData)
      {
         JObject jo = JObject.Parse(response);
         if (entityTag != null) jo = jo[entityTag] as JObject;
         review.Id = jo.Value<long>("id");
         if (parseData)
         {
            ValidateCaches();
            review.Subject = jo.Value<string>("summary");
            review.Description = jo.Value<string>("description");
            review.TestingDone = jo.Value<string>("testing_done");
            //review.BugsClosed = jo.Value<string>("bugs_closed"); //this is not a string but an array (TODO)
            review.LastUpdated = jo.Value<DateTime>("last_updated");
            review.Status = jo.Value<string>("status");
            review.Branch = jo.Value<string>("branch");

            //target groups
            JArray jgroups = jo["target_groups"] as JArray;
            if(jgroups != null)
            {
               foreach(JObject jg in jgroups)
               {
                  string title = jg.Value<string>("title");
                  if(title != null && _groupNameToGroup.ContainsKey(title))
                  {
                     review.TargetGroups.Add(_groupNameToGroup[title]);
                  }
               }
            }

            //target users
            JArray jusers = jo["target_people"] as JArray;
            if(jusers != null)
            {
               foreach (JObject juser in jusers)
               {
                  string title = juser.Value<string>("title");
                  if(title != null && _userNameToUser.ContainsKey(title))
                  {
                     review.TargetUsers.Add(_userNameToUser[title]);
                  }
               }
            }
         }

         var links = jo["links"] as JObject;
         if (links != null)
         {
            if (review.Links == null) review.Links = new ReviewLinks();
            review.Links.Diffs = ParseLink(links, "diffs");
            review.Links.Update = ParseLink(links, "update");
            review.Links.Draft = ParseLink(links, "draft");
            review.Links.Self = ParseLink(links, "self");

            var linkSubmitter = links["submitter"] as JObject;
            if(linkSubmitter != null)
            {
               string userName = linkSubmitter.Value<string>("title");
               if(userName != null)
               {
                  ValidateCaches();
                  if (_userNameToUser.ContainsKey(userName)) review.Submitter = _userNameToUser[userName];
               }
            }
         }
      }

      /// <summary>
      /// Posts review to RB server
      /// </summary>
      /// <param name="review">The review to be posted. Some fields get updated by values received from server.</param>
      public void Post(Review review)
      {
         var request = CreateRequest("review-requests/", Method.POST);
         request.AddParameter("repository", review.Repository.Path);
         var response = Execute(request, 201);
         ParseReview(response.Content, review, "review_request", false);

         //I couldn't post extra fields during initial review creation, they have to be sent as an update
         Update(review);
      }

      public void Update(Review review)
      {
         var request = CreateRequest(review.Links.Draft, Method.PUT);
         if (review.Subject != null) request.AddParameter("summary", review.Subject);
         if (review.Description != null) request.AddParameter("description", review.Description);
         if (review.TestingDone != null) request.AddParameter("testing_done", review.TestingDone);
         if (review.BugsClosed != null) request.AddParameter("bugs_closed", review.BugsClosed);
         if (review.Branch != null) request.AddParameter("branch", review.Branch);

         if (review.TargetUsers.Count > 0)
         {
            string s = string.Join(",", review.TargetUsers.Select(u => u.InternalName));
            request.AddParameter("target_people", s);
         }
         if (review.TargetGroups.Count > 0)
         {
            string s = string.Join(",", review.TargetGroups.Select(g => g.InternalName));
            request.AddParameter("target_groups", s);
         }
         Execute(request, 200);
      }

      public void MakePublic(Review review)
      {
         var request = CreateRequest(review.Links.Draft, Method.POST);
         request.AddFile("public", Encoding.UTF8.GetBytes("1"), null);  //strange
         Execute(request, 201);
      }

      public void Delete(long id)
      {
         var request = CreateRequest("review-requests/" + id, Method.DELETE);
         Execute(request, 204);
      }

      public void AttachDiff(Review review, string repoRoot, string diffText)
      {
         if (review == null) throw new ArgumentNullException("review");
         if (diffText == null) throw new ArgumentNullException("diffText");

         var request = CreateRequest(review.Links.Diffs, Method.POST);
         request.AddFile("basedir", Encoding.UTF8.GetBytes(repoRoot), null);
         request.AddFile("path", Encoding.UTF8.GetBytes(diffText), "diff");

         Execute(request, 201);
      }

      private IEnumerable<Review> ParseReviews(string json, out string nextResource)
      {
         nextResource = null;
         var reviews = new List<Review>();
         JObject jo = JObject.Parse(json);

         //parse requests
         var all = jo["review_requests"] as JArray;
         if (all != null)
         {
            foreach (JObject rr in all)
            {
               var r = new Review();
               ParseReview(rr.ToString(), r, null, true);
               reviews.Add(r);
            }
         }

         JObject links = jo["links"] as JObject;
         if(links != null)
         {
            //get next page
            Uri next = ParseLink(links, "next");
            if(next != null)
            {
               nextResource = next.ToString();
            }
         }

         return reviews;
      }

      public IEnumerable<Review> GetPersonalRequests()
      {
         string resource = "review-requests/?max-results=1000";
         var r = new List<Review>();
         while (resource != null)
         {
            var request = CreateRequest(resource, Method.GET);
            var response = Execute(request, 200);
            IEnumerable<Review> batch = ParseReviews(response.Content, out resource);
            if(batch != null) r.AddRange(batch.Where(rv => rv.Submitter != null && rv.Submitter.InternalName == MyName));
            resource = null;
         }
         return r;
      }

      public void PostComment(Review review, string comment)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<Review> GetRequestsToGroup(string groupName)
      {
         throw new NotImplementedException();
      }
   }
}
