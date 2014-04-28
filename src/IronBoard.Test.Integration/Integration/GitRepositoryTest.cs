using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using NUnit.Framework;

namespace IronBoard.Test.Integration
{
   [TestFixture]
   public class GitRepositoryTest
   {
      private ICodeRepository _git;

      [SetUp]
      public void SetUp()
      {
         string path = Path.GetFullPath("../../../../test/git-repo");

         _git = new GitRepository(path);
      }

      [Test]
      public void GetBranch_OnMyTicket_ReturnsBranch()
      {
         string branchName = _git.Branch;

         Assert.AreEqual("myticket", branchName);
      }

      [Test]
      public void GetVersion_Current_ReturnsSomeVersion()
      {
         string v = _git.ClientVersion;

         Assert.IsNotNull(v);
      }

      [Test]
      public void GetLocalDiff_CurrentState_NonNullDiff()
      {
         string diff = _git.GetLocalDiff();

         Assert.IsNotNull(diff);
      }

      [Test]
      public void GetHistory_LocalRepo_ReturnsValidEntries()
      {
         List<WorkItem> history = _git.GetHistory(10).ToList();

         Assert.Greater(history.Count, 0);
      }
   }
}
