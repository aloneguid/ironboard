using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
         string path = Path.GetFullPath("../../");

         _git = new GitRepository(path);
      }

      [Test]
      public void GetBranch_OnMyTicket_ReturnsBranch()
      {
         string branchName = _git.Branch;

         Assert.AreEqual("myticket", branchName);
      }
   }
}
