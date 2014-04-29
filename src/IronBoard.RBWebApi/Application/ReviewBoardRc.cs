using System;
using System.IO;

namespace IronBoard.RBWebApi.Application
{
   public class ReviewBoardRc
   {
      private const string KeyReviewBoardUrl = "REVIEWBOARD_URL";
      private const string KeyRepository = "REPOSITORY";

      private const string CustomKeyJiraPrefix = "IRONBOARD_JIRA_PREFIX";

      private readonly string _configPath;

      public ReviewBoardRc(string rootFolder)
      {
         if (rootFolder == null) throw new ArgumentNullException("rootFolder");
         if(!Directory.Exists(rootFolder)) throw new DirectoryNotFoundException("rootFolder");
         _configPath = Path.Combine(rootFolder, ".reviewboardrc");
         if (!File.Exists(_configPath)) throw new FileNotFoundException("config file not found at " + _configPath, _configPath);
         ReadConfiguration();
      }

      private void ReadConfiguration()
      {
         using(var reader = new StreamReader(File.OpenRead(_configPath)))
         {
            string s;
            while((s = reader.ReadLine()) != null)
            {
               int idx = s.IndexOf('=');
               if(idx != -1)
               {
                  string key = s.Substring(0, idx).Trim();
                  string val = s.Substring(idx + 1).Trim().Trim('\'').Trim('\"');
                  switch (key)
                  {
                     case KeyReviewBoardUrl:
                        Uri = new Uri(val);
                        break;
                     case KeyRepository:
                        Repository = val;
                        break;
                     case CustomKeyJiraPrefix:
                        JiraPrefix = val;
                        break;
                  }
               }
            }
         }
      }

      public Uri Uri { get; private set; }

      public string Repository { get; private set; } 

      public string JiraPrefix { get; private set; }
   }
}
