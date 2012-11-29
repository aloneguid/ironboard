using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
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
         return new[]
                   {
                      new UserGroup(1, "group1", "Group One", null, true, false, null), 
                      new UserGroup(2, "group2", "Group Two", null, true, false, null), 
                   };
      }

      public IEnumerable<User> GetUsers()
      {
         return new[]
                   {
                      new User(1, "ivan", "Ivan", "Gavryliuk", "asshole", "dsfadf@gmail.com", null),
                      new User(1, "ivan1", "Ivan1", "Gavryliuk1", "asshole1", "1dsfadf@gmail.com", null),
                      new User(1, "ivan2", "Ivan2", "Gavryliuk2", "asshole2", "2dsfadf@gmail.com", null),
                      new User(1, "ivan3", "Ivan3", "Gavryliuk3", "asshole3", "3dsfadf@gmail.com", null)
                   };
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

      public void MakePublic(Review review)
      {
         throw new NotImplementedException();
      }

      public void AttachDiff(Review review, string repoRoot, string diffText)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<Review> GetPersonalRequests()
      {
         Thread.Sleep(TimeSpan.FromSeconds(5));

         var r1 = new Review(1, "review 1", "no desc", "i hate tests", "MGK-76", DateTime.Now);
         return new[] { r1 };
      }
   }
}
