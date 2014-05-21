using System;
using IronBoard.Core.Application;
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
      public void RelativeRoot_TestRepository_Matches()
      {
         Uri remote = _svn.RemoteRepositoryUri;
         string relative = _svn.RelativeRoot;
      }
   }
}
