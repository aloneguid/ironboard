using System.Runtime.Remoting;
using NUnit.Framework;
using RestSharp;

namespace IronBoard.Test.Unit
{
   [TestFixture]
   public class RestSharpWorkaroundsTest
   {
      [Test]
      public void RestHttp_AddsAuthHeaderTwice_ComesOutAsOne()
      {
         var request = new RestRequest();
         request.AddHeader("h1", "v1");
         request.AddHeader("h1", "v1");
         Assert.AreEqual(2, request.Parameters.FindAll(p => p.Name == "h1").Count);

         int removed = request.Parameters.RemoveAll(p => p.Type == ParameterType.HttpHeader && p.Name == "h1");
         Assert.AreEqual(2, removed);
         Assert.AreEqual(0, request.Parameters.Count);
      }

   }
}
