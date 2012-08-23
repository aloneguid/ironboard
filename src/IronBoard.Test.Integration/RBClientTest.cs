using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Model;
using NUnit.Framework;

namespace IronBoard.Test.Integration
{
   [TestFixture]
   public class RBClientTest
   {
      private RBClient _client;

      [SetUp]
      public void SetUp()
      {
         _client = new RBClient("https://ironboard.svn.codeplex.com/svn/trunk", @"c:\dev\ironboard", null);
         _client.Authenticate(new NetworkCredential("igavryliuk", "M1m3c45t"));
      }

      [Test]
      public void GetRepositoriesTest()
      {
         IEnumerable<Repository> repos = _client.GetRepositories();
         Assert.Greater(repos.Count(), 0);
      }

      [Test]
      public void GetGroupsTest()
      {
         IEnumerable<UserGroup> groups = _client.GetGroups();
         Assert.Greater(groups.Count(), 0);
      }

      [Test]
      public void GetUsersTest()
      {
         IEnumerable<User> users = _client.GetUsers();
         Assert.Greater(users.Count(), 0);
      }

      [Test]
      public void PostReviewToGroupsAndPeopleTest()
      {
         var review = new Review();
         review.Repository = _client.GetRepositories().First();
         review.Subject = "integration test";
         review.TestingDone = "integration " + DateTime.Now.ToString();

         User u1 = _client.GetUsers().First(u => u.Username == "cfullerton");
         User u2 = _client.GetUsers().First(u => u.Username == "iknight");
         UserGroup g1 = _client.GetGroups().First(g => g.Name == "mso4-all");
         
         review.TargetUsers.Add(u1);
         review.TargetUsers.Add(u2);
         review.TargetGroups.Add(g1);

         _client.Post(review);
      }
   }
}
