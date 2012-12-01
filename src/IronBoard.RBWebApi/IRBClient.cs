using System;
using System.Collections.Generic;
using System.Net;
using IronBoard.RBWebApi.Model;

namespace IronBoard.RBWebApi
{
   public interface IRbClient
   {
      event Action<NetworkCredential> AuthenticationRequired;

      event Action<string> AuthCookieChanged;

      Uri ServerUri { get; }

      string AuthCookie { get; set; }

      IEnumerable<Repository> GetRepositories();

      IEnumerable<UserGroup> GetGroups();

      IEnumerable<User> GetUsers();

      void Authenticate(NetworkCredential creds);

      /// <summary>
      /// Posts review to RB server
      /// </summary>
      /// <param name="review">The review to be posted. Some fields get updated by values received from server.</param>
      void Post(Review review);

      void Update(Review review);

      void MakePublic(Review review);

      void Delete(long id);

      void AttachDiff(Review review, string repoRoot, string diffText);

      IEnumerable<Review> GetPersonalRequests();
   }
}