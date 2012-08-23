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
   }
}
