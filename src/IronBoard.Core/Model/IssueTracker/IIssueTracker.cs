using System.Collections.Generic;

namespace IronBoard.Core.Model.IssueTracker
{
   public interface IIssueTracker
   {
      IEnumerable<TrackerProject> Projects { get; }

      IEnumerable<TrackerIssue> GetIssues(string projectName, string sprintName);

      TrackerIssue GetIssue(string issueId);

      void CommentOnIssue(string issueId, string comment);

      void UpdateCustomField(string issueId, string fieldName, string fieldValue);
      void UpdateFixVersion(string projectId, string issueId, string fixVersion);
      void TransitionIssue(string issueId, string targetStatus);
   }
}
