using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.RBWebApi.Application;
using IronBoard.RBWebApi.Model;
using RestSharp;

namespace IronBoard.RBWebApi
{
   public class RBClient
   {
      private readonly Uri _webApiBaseUri;
      private RestClient _client;
      private ReviewBoardRc _config;

      public RBClient(Uri webApiBaseUri, string projectRootFolder)
      {
         if (webApiBaseUri == null) throw new ArgumentNullException("webApiBaseUri");
         _webApiBaseUri = webApiBaseUri;
         _client = new RestClient(new Uri(webApiBaseUri, "api").ToString());
         _config = new ReviewBoardRc(projectRootFolder);
      }

      public IEnumerable<Repository> GetRepositories()
      {
         var request = new RestRequest("repositories");
         request.Method = Method.GET;

         return null;
      }
   }
}
