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
      private readonly RestClient _client;
      private readonly ReviewBoardRc _config;

      public RBClient(string projectRootFolder)
      {
         _config = new ReviewBoardRc(projectRootFolder);
         if(_config.Uri == null) throw new ArgumentException("config file doesn't contain ReviewBoard server url");
         _client = new RestClient(new Uri(_config.Uri, "api").ToString());
      }

      public IEnumerable<Repository> GetRepositories()
      {
         var request = new RestRequest("repositories");
         request.Method = Method.GET;
         RestResponse response = _client.Execute(request) as RestResponse;
         if(response != null)
         {
                        
         }
         return null;
      }
   }
}
