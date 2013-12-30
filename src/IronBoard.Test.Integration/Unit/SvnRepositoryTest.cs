using IronBoard.Core.Application;
using NUnit.Framework;

namespace IronBoard.Test.Unit
{
   [TestFixture]
   public class SvnRepositoryTest
   {
      [TestCase("http://svn/repo/", "http://svn/repo")]
      [TestCase("http://svn/repo;", "http://svn/repo")]
      [TestCase("http://svn/repo", "http://svn/repo")]
      public void TrimVariousUrls(string input, string output)
      {
         Assert.AreEqual(output, SvnRepository.TrimRepositoryUrl(input));
      }
   }
}
