using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.RBWebApi.Model;
using RestSharp;

namespace IronBoard.RBWebApi
{
   public class RBClient
   {
      private readonly Uri _webApiBaseUri;
      private RestClient _client;

      public RBClient(Uri webApiBaseUri)
      {
         if (webApiBaseUri == null) throw new ArgumentNullException("webApiBaseUri");
         _webApiBaseUri = webApiBaseUri;
         _client = new RestClient(webApiBaseUri.ToString());
      }

      public IEnumerable<Repository> GetRepositories()
      {
         //var request = new RestRequest()
      }
   }
}
