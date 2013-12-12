using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using IronBoard.Core.Application;
using IronBoard.Core.Model.IssueTracker;
using IronBoard.Daemon.Application;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Daemon
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         IIssueTracker tracker = new JiraIssueTracker("***",
            new NetworkCredential("***", "***"));

         IRbClient rb = RbFactory.CreateHttpClient("c:\\dev\\mse", null);
         rb.Authenticate(new NetworkCredential("***", "***"));

         Console.WriteLine("fetching review tickets...");
         IEnumerable<Review> reviews = rb.GetRequestsToGroup("si");

         Console.WriteLine("fetching Jira issues for this sprint...");
         IEnumerable<TrackerIssue> issues = tracker.GetIssues("MSE", "2.5.13");

         var workflow = new MimecastWorkflow(rb, tracker, new ConsoleNotifier());
         workflow.Execute("MSE", "2.5.13", issues, reviews);
      }
   }
}
