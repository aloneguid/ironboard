using System;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using NUnit.Framework;

namespace IronBoard.Test.Unit.Presenters
{
   [TestFixture]
   public class WorkItemRangeSelectorPresenterTest
   {
      private WorkItemRangeSelectorPresenter _presenter;

      [SetUp]
      public void SetUp()
      {
          _presenter = new WorkItemRangeSelectorPresenter(null);
      }

      [Test]
      public void ExtractJiraBugsTest()
      {
         var items = new[]
            {
               new WorkItem("1", "arsehole",
                            "i have fixed ABCD-1234 which is a regression of TRD-12 which i have fixed before",
                            DateTime.Now),
               new WorkItem("1", "arsehole",
                            "refixed FGT-777 because I'm a useless arsehole (ABCD-1234)",
                            DateTime.Now)
            };

         string[] bugs = _presenter.ExtractBugsClosed(items);
         Assert.AreEqual(3, bugs.Length);
         Assert.AreEqual("ABCD-1234", bugs[0]);
         Assert.AreEqual("TRD-12", bugs[1]);
         Assert.AreEqual("FGT-777", bugs[2]);
      }

      [Test]
      public void ExtractTestingTest()
      {
         var wi = new WorkItem("1", "God", "fake", DateTime.Now);
         wi.ChangedFilePaths.Add("/dsfdsf/sdfdsf/SuperCode.cs");
         wi.ChangedFilePaths.Add("/dsfdsf/dfsfd/UnexpectedUnitTest.cs");
         wi.ChangedFilePaths.Add("/dsfdsf/dfsfd/ExpectedUnitTest.cs");

         string bugsClosed = _presenter.ExtractTestingDone(new[] {wi});
         Assert.AreEqual("unit testing (see UnexpectedUnitTest.cs, ExpectedUnitTest.cs)", bugsClosed);
      }
   }
}
