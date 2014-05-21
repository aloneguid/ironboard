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
      private const string TestBranchName = "myticket";
      private ICodeRepository _git;

      [SetUp]
      public void SetUp()
      {
         string path = Path.GetFullPath("../../../../test/git-repo");

         _git = new GitRepository(path);
         _git.Branch = _git.MainBranchName;
      }

      [TearDown]
      public void TearDown()
      {
         _git.Branch = _git.MainBranchName;
      }

      [Test]
      public void BranchSwitching_ToTicketAndFromTicket_ReturnsToPrimary()
      {
         Assert.AreEqual(_git.MainBranchName, _git.Branch);

         _git.Branch = TestBranchName;
         Assert.AreEqual(TestBranchName, _git.Branch);

         _git.Branch = _git.MainBranchName;
         Assert.AreEqual(_git.MainBranchName, _git.Branch);
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
      public void GetHistory_LocalRepo_ReturnsValidEntries()
      {
         List<WorkItem> history = _git.GetHistory(10).ToList();

         Assert.Greater(history.Count, 0);
      }

      [Test]
      public void DiffRange_FirstCommit_GetsDiff()
      {
         List<WorkItem> history = _git.GetHistory(10).OrderBy(i => i.Time).ToList();
         string youngestId = history[0].ItemId;

         string diff =
            _git.GetDiff(new RevisionRange(youngestId, youngestId));

         Assert.NotNull(diff);
      }

      [Test]
      public void DiffRange_NotFirstCommit_GetsDiff()
      {
         List<WorkItem> history = _git.GetHistory(10).OrderBy(i => i.Time).ToList();

         string diff =
            _git.GetDiff(new RevisionRange(history[1].ItemId, history[history.Count - 1].ItemId));

         Assert.NotNull(diff);
      }
   }
}
