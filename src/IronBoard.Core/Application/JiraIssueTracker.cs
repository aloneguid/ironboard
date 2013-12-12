using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Atlassian.Jira;
using Atlassian.Jira.Linq;
using Atlassian.Jira.Remote;
using IronBoard.Core.Model;
using IronBoard.Core.Model.IssueTracker;

namespace IronBoard.Core.Application
{
   public class JiraIssueTracker : IIssueTracker
   {
      private const string BindingFileName = "jira-tracker.binding";

      private readonly Jira _jira;
      private List<Project> _projects;
      private readonly Dictionary<string, List<ProjectVersion>> _projectKeyToProjectVersions = new Dictionary<string, List<ProjectVersion>>();
      private List<IssueStatus> _statuses; 

      public JiraIssueTracker(string serverLocation, NetworkCredential credential)
      {
         _jira = new Jira(serverLocation, credential.UserName, credential.Password);

         Initialise();

         string binding = _jira.GetAccessToken();  //this makes a Login network call
         if (binding != null)
         {
            SaveBinding(binding);
         }
      }

      public JiraIssueTracker(string serverLocation, string binding)
      {
         _jira = new Jira(serverLocation, binding);

         Initialise();
      }

      private void Initialise()
      {
         _jira.MaxIssuesPerRequest = 1000;
      }

      public static IIssueTracker CreateFromBindingOrCredentials(string serverLocation, NetworkCredential credential)
      {
         string binding = LoadBinding();
         if(binding == null) return new JiraIssueTracker(serverLocation, credential);
         return new JiraIssueTracker(serverLocation, binding);
      }

      private static void SaveBinding(string binding)
      {
         string fullName = Path.Combine(Environment.CurrentDirectory, BindingFileName);
         if (binding == null)
         {
            if (File.Exists(fullName))
            {
               File.Delete(fullName);
            }
         }
         else
         {
            File.WriteAllText(fullName, binding);
         }
      }

      public static string LoadBinding()
      {
         string fullName = Path.Combine(Environment.CurrentDirectory, BindingFileName);
         if (File.Exists(fullName)) return File.ReadAllText(fullName);
         return null;
      }

      #region [ Jira Helpers ]

      private List<Project> JiraProjects
      {
         get { return _projects ?? (_projects = new List<Project>(_jira.GetProjects())); }
      }

      private Project GetProject(string key)
      {
         return JiraProjects.FirstOrDefault(p => p.Key == key);
      }

      private Dictionary<string, string> CollectCustomFields(Issue issue)
      {
         if (issue.CustomFields == null || issue.CustomFields.Count == 0) return null;

         var result = new Dictionary<string, string>();
         foreach (CustomField field in issue.CustomFields)
         {
            string value = (field.Values == null || field.Values.Length == 0)
               ? null
               : string.Join(",", field.Values);
            string name;
            try
            {
               name = field.Name;
            }
            catch (InvalidOperationException)
            {
               name = field.Id;
            }

            result[name] = value;
         }
         return result;
      }

      private string[] GetFixVersions(Issue i)
      {
         if (i.FixVersions == null || i.FixVersions.Count == 0) return null;

         return i.FixVersions.Select(fv => fv.Name).ToArray();
      }

      private TrackerIssue Convert(Issue i)
      {
         return new TrackerIssue(i.Key.ToString())
         {
            Reporter = i.Reporter,
            Assignee = i.Assignee,
            Subject = i.Summary,
            Description = i.Description,
            Created = i.Created,
            Updated = i.Updated,
            Resolution = i.Resolution == null ? null : i.Resolution.ToString(),
            Status = i.Status == null ? null : i.Status.Name,
            CustomFields = CollectCustomFields(i),
            FixVersions = GetFixVersions(i)
         };
      }

      private IEnumerable<TrackerIssue> Convert(IEnumerable<Issue> issues)
      {
         if (issues == null) return null;

         return issues.Select(Convert).ToList();
      }

      private ProjectVersion GetProjectVersion(string projectId, string versionName)
      {
         if (!_projectKeyToProjectVersions.ContainsKey(projectId))
         {
            IEnumerable<ProjectVersion> versions = _jira.GetProjectVersions(projectId);
            _projectKeyToProjectVersions[projectId] = new List<ProjectVersion>(versions);
         }

         return _projectKeyToProjectVersions[projectId].FirstOrDefault(v => v.Name == versionName);
      }

      private IssueStatus GetStatus(string name)
      {
         if (_statuses == null)
         {
            _statuses = new List<IssueStatus>(_jira.GetIssueStatuses());
         }

         return _statuses.FirstOrDefault(s => s.Name == name);
      }

      #endregion

      public IEnumerable<TrackerProject> Projects
      {
         get
         {
            return JiraProjects.Select(jp => new TrackerProject(jp.Key, jp.Name)).ToList();
         }
      }

      public IEnumerable<TrackerIssue> GetIssues(string projectName, string sprintName)
      {
         string qry = new JqlBuilder()
            .Add("project", projectName)
            .Add("sprint", sprintName)
            .ToString();

         IEnumerable<Issue> issues = _jira.GetIssuesFromJql(qry);
         return Convert(issues);
      }

      public TrackerIssue GetIssue(string issueId)
      {
         Issue issue = _jira.GetIssue(issueId);

         return Convert(issue);
      }

      public void CommentOnIssue(string issueId, string comment)
      {
         Issue issue = _jira.GetIssue(issueId);

         issue.AddComment(comment);
      }

      public void UpdateCustomField(string issueId, string fieldName, string fieldValue)
      {
         Issue issue = _jira.GetIssue(issueId);
         issue.CustomFields.Add(fieldName, fieldValue);
         issue.SaveChanges();
      }

      public void UpdateFixVersion(string projectId, string issueId, string fixVersion)
      {
         Issue issue = _jira.GetIssue(issueId);
         ProjectVersion v = GetProjectVersion(projectId, fixVersion);
         if(v == null) throw new ArgumentException("version " + fixVersion + " does not exist", "fixVersion");

         issue.FixVersions.Add(v);
         issue.SaveChanges();
      }

      public void TransitionIssue(string issueId, string targetStatus)
      {
         Issue issue = _jira.GetIssue(issueId);
         issue.Status = GetStatus(targetStatus);
         issue.SaveChanges();
      }
   }
}
