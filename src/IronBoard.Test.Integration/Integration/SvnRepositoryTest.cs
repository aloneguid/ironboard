using System;
using System.Collections.Generic;
using System.Linq;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using NUnit.Framework;

namespace IronBoard.Test.Integration
{
   [Ignore]
   public class SvnRepositoryTest
   {
      private SvnRepository _svn;

      [SetUp]
      public void InitialiseRepository()
      {
         _svn = new SvnRepository(@"C:\dev\ironboard");
      }

      [Test]
      public void GetPendingChangesTest()
      {
         IEnumerable<LocalWorkItem> pending = _svn.GetLocalChanges();
         Assert.Greater(pending.Count(), 0);

         string diff = _svn.GetLocalDiff();
      }

      [Test]
      public void RelativeRoot_TestRepository_Matches()
      {
         Uri remote = _svn.RemoteRepositoryUri;
         string relative = _svn.RelativeRoot;
      }
   }
}
