using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using IronBoard.Core.Model;

namespace IronBoard.Core.Application
{
   public sealed class GitRepository : CommandLineRepository
   {
      private Lazy<Uri> _remoteRepositoryUri = new Lazy<Uri>(); 

      public GitRepository(string workingCopyPath) : base(workingCopyPath, "git")
      {
      }

      public override string ClientVersion
      {
         get { return Exec("--version"); }
      }

      public override string Branch
      {
         get
         {
            string branchInfo = Exec("branch");

            return ExtractCurrentBranch(branchInfo);
         }
         set
         {
            Exec("checkout {0}", value);
         }
      }

      public override string MainBranchName { get { return "master"; } }

      public override string RelativeRoot
      {
         get { return RemoteRepositoryUri.ToString(); }
      }

      private Uri RemoteRepositoryUri
      {
         get
         {
            string url = Exec("config --get remote.origin.url");

            return new Uri(url);
         }
      }

      public override string GetDiff(RevisionRange range)
      {
         range = (RevisionRange) range.Clone();

         //get previous log entry
         try
         {
            string log = Exec("log -n 1 \"{0}^1\"", range.From);
            IEnumerable<WorkItem> entries = ParseLog(log);

            WorkItem previous;
            if (entries == null || (previous = entries.FirstOrDefault()) == null) throw new ApplicationException("could not get previous entry");

            range.From = previous.ItemId;
         }
         catch (ArgumentException)
         {
            //that only means that the first entry doesn't have a parent, so let's diff from the master
            string diff = Exec("diff --full-index \"{0}\" master", range.To);
            return diff;
         }

         //execute the real diff
         return Exec("diff --full-index \"{0}\" \"{1}\"", range.From, range.To);

         //git diff range.to master         
      }

      public override IEnumerable<WorkItem> GetHistory(int maxEntries)
      {
         /*
          * --date=(relative|local|default|iso|rfc|short|raw)
          * Only takes effect for dates shown in human-readable format, such as when using "--pretty". log.date config variable sets a default value for log command’s --date option.
          * --date=relative shows dates relative to the current time, e.g. "2 hours ago".
          * --date=local shows timestamps in user’s local timezone.
          * --date=iso (or --date=iso8601) shows timestamps in ISO 8601 format.
          * --date=rfc (or --date=rfc2822) shows timestamps in RFC 2822 format, often found in E-mail messages.
          * --date=short shows only date but not time, in YYYY-MM-DD format.
          * --date=raw shows the date in the internal raw git format %s %z format.
          * --date=default shows timestamps in the original timezone (either committer’s or author’s).
          */

         string log = Exec("log -n {0} --date={1}",
            maxEntries,
            "rfc");  //need to enforce date format as it can vary

         return ParseLog(log);
      }

      public override void Dispose()
      {
      }

      private IEnumerable<WorkItem> ParseLog(string log)
      {
         var result = new List<WorkItem>();

         string itemId = null;
         string author = null;
         string comment = null;
         DateTime date = DateTime.UtcNow;

         foreach (string line in log.Split('\n'))
         {
            if (line.StartsWith("commit "))
            {
               if (itemId != null)
               {
                  var item = new WorkItem(itemId, author, comment, date);
                  result.Add(item);
                  author = comment = null;
                  date = DateTime.UtcNow;
               }

               itemId = line.Substring(7).Trim();
            }
            else if (line.StartsWith("author:", StringComparison.InvariantCultureIgnoreCase))
            {
               author = line.Substring(7).Trim();
            }
            else if (line.StartsWith("date:", StringComparison.InvariantCultureIgnoreCase))
            {
               //date example: Mon Apr 28 11:55:45 2014 +0100
               string sd = line.Substring(5).Trim();
               date = ParseRfc2822Date(sd);
            }
            else
            {
               string s = line.Trim();
               if (!string.IsNullOrEmpty(s))
               {
                  if (comment == null)
                  {
                     comment = s;
                  }
                  else
                  {
                     comment += Environment.NewLine;
                     comment += s;
                  }
               }
            }
         }

         if (itemId != null)
         {
            result.Add(new WorkItem(itemId, author, comment, date));
         }

         return result;
      }

      private string ExtractCurrentBranch(string branchInfo)
      {
         string current = Split(branchInfo).FirstOrDefault(b => b.StartsWith("* "));

         return current == null ? null : current.Substring(2).Trim();
      }

      private static DateTime ParseRfc2822Date(string date)
      {
         date = date.Replace("bst", "+0100");
         date = date.Replace("gmt", "-0000");
         date = date.Replace("edt", "-0400");
         date = date.Replace("est", "-0500");
         date = date.Replace("cdt", "-0500");
         date = date.Replace("cst", "-0600");
         date = date.Replace("mdt", "-0600");
         date = date.Replace("mst", "-0700");
         date = date.Replace("pdt", "-0700");
         date = date.Replace("pst", "-0800");

         DateTime parsedDateTime = DateTime.MinValue;

         var r =
            new Regex(
               @"(?:(?:Mon|Tue|Wed|Thu|Fri|Sat|Sun), )?(?<DateTime>\d{1,2} (?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) \d{4} \d{2}\:\d{2}(?:\:\d{2})?)(?: (?<TimeZone>[\+-]\d{4}))?",
               RegexOptions.IgnoreCase);
         Match m = r.Match(date);
         if (m.Success)
         {
            string dateTime = m.Groups["DateTime"].Value.TrimStart('0');
            parsedDateTime = DateTime.ParseExact(dateTime,
               new[] {"d MMM yyyy HH:mm", "d MMM yyyy HH:mm:ss", "d MMM yyyy hh:mm", "d MMM yyyy hh:mm:ss"},
               CultureInfo.InvariantCulture, DateTimeStyles.None);

            string timeZone = m.Groups["TimeZone"].Value;
            if (timeZone.Length == 5)
            {
               int hour = Int32.Parse(timeZone.Substring(0, 3));
               int minute = Int32.Parse(timeZone.Substring(3));
               var offset = new TimeSpan(hour, minute, 0);
               parsedDateTime = new DateTimeOffset(parsedDateTime, offset).UtcDateTime;
            }
         }
         return parsedDateTime;
      }

      private IEnumerable<string> Split(string output)
      {
         return output.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
      }
   
   }
}
