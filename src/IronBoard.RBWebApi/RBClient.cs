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
      private string _cookie;

      public RBClient(string projectRootFolder)
      {
         _config = new ReviewBoardRc(projectRootFolder);
         if(_config.Uri == null) throw new ArgumentException("config file doesn't contain ReviewBoard server url");
         _client = new RestClient(new Uri(_config.Uri, "api").ToString());
      }

      private RestRequest CreateRequest(string resource, Method method)
      {
         var request = new RestRequest(resource) {Method = method};
         if(_cookie != null) request.AddCookie("rbsessionid", _cookie);
         return request;
      }

      public IEnumerable<Repository> GetRepositories()
      {
         var result = new List<Repository>();
         var request = new RestRequest("repositories/");
         request.Method = Method.GET;
         RestResponse response = _client.Execute(request) as RestResponse;
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

      public void Authenticate(NetworkCredential creds)
      {
         var request = CreateRequest("repositories/", Method.GET);
         string auth = creds.UserName + ":" + creds.Password;
         auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(auth));
         request.AddHeader(HttpRequestHeader.Authorization.ToString(), auth);
         var response = _client.Execute(request);
         var cookie = response.Cookies.FirstOrDefault(c => c.Name == "rbsessionid");
         if(cookie == null) throw new AuthenticationException("invalid username or password");
         _cookie = cookie.Value;
      }

      /// <summary>
      /// Posts review to RB server
      /// </summary>
      /// <param name="review">The review to be posted. Some fields get updated by values received from server.</param>
      public void Post(Review review)
      {
         var request = CreateRequest("review-requests/", Method.POST);
         request.AddParameter("repository", review.Repository.Path);
         var response = _client.Execute(request) as RestResponse;

         //I couldn't post extra fields during initial review creation, they have to be sent as an update
         
      }
   }
}
