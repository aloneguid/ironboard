using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using IronBoard.Core.Model.IssueTracker;
using IronBoard.Daemon.Model;
using IronBoard.Daemon.Resources;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Daemon
{
   class MimecastWorkflow
   {
      private readonly IRbClient _reviewBoard;
      private readonly IIssueTracker _tracker;
      private readonly INotifier _notifier;
      private static Random _rnd = new Random(DateTime.UtcNow.Millisecond);

      private const string ReviewLinkFieldName = "Review Link";
      private const string InProgressStatusName = "In Progress";
      private const string PendingReleaseStatusName = "Pending Release";
      private const string InReviewStatusname = "In Review";
      private const string InTestingStatusname = "In Testing";

      public MimecastWorkflow(IRbClient reviewBoard, IIssueTracker tracker, INotifier notifier)
      {
         if (reviewBoard == null) throw new ArgumentNullException("reviewBoard");
         if (tracker == null) throw new ArgumentNullException("tracker");
         if (notifier == null) throw new ArgumentNullException("notifier");

         _reviewBoard = reviewBoard;
         _tracker = tracker;
         _notifier = notifier;
      }

      public void Execute(string projectId, string releaseFixVersion,
         IEnumerable<TrackerIssue> trackerIssuesInput,
         IEnumerable<Review> reviewsInput)
      {
         var trackerIssues = new List<TrackerIssue>(trackerIssuesInput);
         var reviews = new List<Review>(reviewsInput);

         //ProcessJiraTickets(trackerIssues);
         //StampReviewLinks(reviews);
         CheckFixVersion(trackerIssues, projectId, releaseFixVersion);
         //TransitionIssuesToReview(trackerIssues);
         //ProcessReviewTicketsNotReviewedForTooLong(reviews);
         //ProcessReviewTicketsOpenForTooLong(reviews);
         //CheckFixVersion(trackerIssues, projectId, releaseFixVersion);
      }

      public void Release(string projectId, string releaseFixVersion)
      {
         
      }

      private string GetRandomResourceString(ResourceManager rm)
      {
         var allKeys = new List<string>();

         ResourceSet rs = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
         IDictionaryEnumerator ide = rs.GetEnumerator();
         while (ide.MoveNext())
         {
            string key = (string) ide.Key;
            allKeys.Add(key);
         }

         int idx = _rnd.Next(allKeys.Count);
         string s = rm.GetString(allKeys[idx]);
         return s;
      }

      private bool HasReviewLink(TrackerIssue issue)
      {
         return issue.CustomFields.ContainsKey(ReviewLinkFieldName) &&
                !string.IsNullOrEmpty(issue.CustomFields[ReviewLinkFieldName]) &&
                issue.CustomFields[ReviewLinkFieldName].StartsWith("http:");
      }

      private void AskForTrackerEstimates()
      {
         //project="Dev: Mimecast Synchronisation Engine" and originalEstimate is EMPTY and Sprint="2.5.13" and status in (Open, "In Progress", "In Review", "In Testing") and type in (Improvement, "New Feature", bug) and assignee != igavryliuk
      }

      private void CheckFixVersion(IEnumerable<TrackerIssue> issues, string projectId, string fixVersion)
      {
         foreach (TrackerIssue issue in issues)
         {
            if (issue.Status == PendingReleaseStatusName)
            {
               if (issue.FixVersions == null || !issue.FixVersions.Contains(fixVersion))
               {
                  _notifier.NotifyUser(NotificationLevel.Verbose, "updating fix version to {0} for {1}", fixVersion, issue);
                  _tracker.UpdateFixVersion(projectId, issue.Id, fixVersion);
               }
            }
         }
      }

      private void CheckHasFixSummary(IEnumerable<TrackerIssue> issues)
      {
         //todo: can't detect fix summary
      }

      private void ProcessReviewTicketsOpenForTooLong(IEnumerable<Review> reviews)
      {
         foreach (Review review in reviews)
         {
            bool openForTooLong = (DateTime.Now - review.Created) > TimeSpan.FromDays(5);

            if (openForTooLong)
            {
               _reviewBoard.PostComment(review, GetRandomResourceString(ReviewTicketOpenForTooLong.ResourceManager));
            }
         }
      }

      private void ProcessReviewTicketsNotReviewedForTooLong(IEnumerable<Review> reviews)
      {
         foreach (Review review in reviews)
         {
            bool hasExpired = (DateTime.Now - review.LastUpdated) > TimeSpan.FromHours(5);

            if (hasExpired)
            {
               _reviewBoard.PostComment(review, Strings.ReviewBugger_NotUpdated);
            }
         }
      }

      private void ProcessJiraTickets(IEnumerable<TrackerIssue> issues)
      {

         foreach (TrackerIssue issue in issues)
         {
            bool stuckInStatus = issue.Updated.HasValue && (DateTime.Now - issue.Updated) > TimeSpan.FromHours(8);

            if (stuckInStatus)
            {
               _notifier.NotifyUser(NotificationLevel.Warning, "no update on issue for 8 hours, {0}", issue);

               if (issue.Status == InProgressStatusName)
               {
                  _tracker.CommentOnIssue(issue.Id, Strings.JiraBugger_Old_InProgress);
               }
               else if (issue.Status == InReviewStatusname)
               {
                  _tracker.CommentOnIssue(issue.Id, GetRandomResourceString(TrackerReviewNotDoneYet.ResourceManager));
               }
               else if (issue.Status == "In Testing")
               {
                  _tracker.CommentOnIssue(issue.Id, Strings.JiraBugger_Old_InTesting);
               }
            }

            //

            bool requiresReviewLinkButHasNone =
               (issue.Status == InReviewStatusname || issue.Status == InTestingStatusname) && !HasReviewLink(issue);

            if (requiresReviewLinkButHasNone)
            {
               _tracker.CommentOnIssue(issue.Id, Strings.JiraBugger_AskToAddReviewLink);
            }
         }
      }

      private void TransitionIssuesToReview(IEnumerable<TrackerIssue> issues)
      {
         foreach (TrackerIssue issue in issues)
         {
            if (HasReviewLink(issue) && issue.Status == InProgressStatusName)
            {
               _notifier.NotifyUser(NotificationLevel.Verbose,
                  "transitioning issue {0} from {1} to {2} because it has a review link", issue.Id, issue.Status,
                  InReviewStatusname);

               _tracker.TransitionIssue(issue.Id, InReviewStatusname);
            }
         }
      }

      private void StampReviewLinks(IEnumerable<Review> reviews)
      {
         foreach (Review review in reviews)
         {
            if (String.IsNullOrEmpty(review.BugsClosed))
            {
               _notifier.NotifyUser(NotificationLevel.Warning, "ticket has no associated issue, asking user");
               _reviewBoard.PostComment(review, GetRandomResourceString(ReviewTicketHasNoJiraIssue.ResourceManager));
            }
            else
            {
               string[] keys = review.BugsClosed.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

               foreach (string key1 in keys)
               {
                  string key = key1.Trim();

                  TrackerIssue issue = null;

                  try
                  {
                     issue = _tracker.GetIssue(key);
                  }
                  catch (Exception)
                  {

                  }

                  if (issue == null)
                  {
                     _notifier.NotifyUser(NotificationLevel.Warning, "invalid issue {0}", key);
                     _reviewBoard.PostComment(review,
                        String.Format(Strings.ReviewBugger_InvalidJiraIssue, key));
                  }
                  else
                  {

                     if (!HasReviewLink(issue))
                     {
                        string url = String.Format("{0}r/{1}", _reviewBoard.ServerUri, review.Id);

                        _notifier.NotifyUser(NotificationLevel.Verbose, "associating {0} with {1}", issue.Id, url);

                        try
                        {
                           _tracker.UpdateCustomField(issue.Id, ReviewLinkFieldName, url);
                        }
                        catch (Exception ex)
                        {
                           _notifier.NotifyUser(NotificationLevel.Warning, "failed to update {0} ({1})", issue.Id, ex.Message);
                        }
                     }
                  }
               }

            }
         }
      }
   }
}
