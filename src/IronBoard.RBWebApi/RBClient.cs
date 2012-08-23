using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using IronBoard.RBWebApi.Application;
using IronBoard.RBWebApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace IronBoard.RBWebApi
{
   public class RBClient
   {
      private readonly RestClient _client;
      private readonly ReviewBoardRc _config;
      private readonly string _svnRepositoryPath;
      private string _authCookie;

      public event Action<NetworkCredential> AuthenticationRequired;
      public event Action<string> AuthCookieChanged;

      public RBClient(string svnRepositoryPath, string projectRootFolder, string authCookie)
      {
         if (svnRepositoryPath == null) throw new ArgumentNullException("svnRepositoryPath");
         if (projectRootFolder == null) throw new ArgumentNullException("projectRootFolder");

         _svnRepositoryPath = svnRepositoryPath;
         _config = new ReviewBoardRc(projectRootFolder);
         if(_config.Uri == null) throw new ArgumentException("config file doesn't contain ReviewBoard server url");
         _client = new RestClient(new Uri(_config.Uri, "api").ToString());
         AuthCookie = authCookie;
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
            throw new InvalidOperationException(response.StatusCode.ToString() + ": " + response.StatusDescription);

         return response;
      }

      public IEnumerable<Repository> GetRepositories()
      {
         var result = new List<Repository>();
         var request = new RestRequest("repositories/");
         request.Method = Method.GET;
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
         var response = Execute(CreateRequest("users/", Method.GET), 200);
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
         JObject review_request = jo[entityTag] as JObject;
         review.Id = review_request.Value<long>("id");
         if (parseData)
         {
            review.Subject = review_request.Value<string>("summary");
            review.Description = review_request.Value<string>("description");
            review.TestingDone = review_request.Value<string>("testing_done");
         }

         JObject links = review_request["links"] as JObject;
         if(review.Links == null) review.Links = new ReviewLinks();
         review.Links.Diffs = ParseLink(links, "diffs");
         review.Links.Update = ParseLink(links, "update");
         review.Links.Draft = ParseLink(links, "draft");
         review.Links.Self = ParseLink(links, "self");
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
         Execute(request, 200);
      }

      public void AttachDiff(Review review, string diffText)
      {
         if (review == null) throw new ArgumentNullException("review");
         if (diffText == null) throw new ArgumentNullException("diffText");

         string diffBaseDir = _svnRepositoryPath.Substring(review.Repository.Path.Length);
         if (!diffBaseDir.StartsWith("/")) diffBaseDir = "/" + diffBaseDir;
         if (diffBaseDir.EndsWith("/")) diffBaseDir = diffBaseDir.Substring(0, diffBaseDir.Length - 1);

         var request = CreateRequest(review.Links.Diffs, Method.POST);
         request.AddFile("basedir", Encoding.UTF8.GetBytes(diffBaseDir), null);
         request.AddFile("path", Encoding.UTF8.GetBytes(diffText), "diff");

         Execute(request, 201);
      }
   }
}
