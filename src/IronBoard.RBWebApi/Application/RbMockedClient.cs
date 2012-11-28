using System;
using System.Collections.Generic;
using System.Net;
using IronBoard.RBWebApi.Model;

namespace IronBoard.RBWebApi.Application
{
   class RbMockedClient : IRbClient
   {
      public event Action<NetworkCredential> AuthenticationRequired;

      public event Action<string> AuthCookieChanged;

      public Uri ServerUri
      {
         get { return new Uri("http://microsoft.com"); }
      }

      public string AuthCookie
      {
         get { throw new NotImplementedException(); }
         set { throw new NotImplementedException(); }
      }

      public IEnumerable<Repository> GetRepositories()
      {
         throw new NotImplementedException();
      }

      public IEnumerable<UserGroup> GetGroups()
      {
         throw new NotImplementedException();
      }

      public IEnumerable<User> GetUsers()
      {
         throw new NotImplementedException();
      }

      public void Authenticate(NetworkCredential creds)
      {
         throw new NotImplementedException();
      }

      public void Post(Review review)
      {
         throw new NotImplementedException();
      }

      public void Update(Review review)
      {
         throw new NotImplementedException();
      }

      public void AttachDiff(Review review, string repoRoot, string diffText)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<Review> GetPersonalRequests()
      {
         var r1 = new Review(1, "review 1", "no desc", "i hate tests", "MGK-76", DateTime.Now);
         return new[] { r1 };
      }
   }
}
