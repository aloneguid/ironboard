using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using NUnit.Framework;

namespace IronBoard.Test.Integration
{
   [TestFixture, Ignore]
   public class SubversionTest
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
         IEnumerable<LocalWorkItem> pending = _svn.GetPendingChanges();
         Assert.Greater(pending.Count(), 0);
      }

      [Test]
      public void GetUncommittedDiffTest()
      {
         string diff = _svn.GetUncommittedDiff(null);
      }
   }
}
