using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IronBoard.Core.Model;
using IronBoard.Core.Model.Exceptions;

namespace IronBoard.Core.Application
{
   public abstract class CommandLineRepository : ICodeRepository
   {
      private readonly string _execName;
      protected string WorkingCopyPath { get; private set; }

      protected CommandLineRepository(string workingCopyPath, string execName)
      {
         _execName = execName;
         if (workingCopyPath == null) throw new ArgumentNullException("workingCopyPath");
         if (!Directory.Exists(workingCopyPath)) throw new DirectoryNotFoundException("working copy does not exist at " + workingCopyPath);

         WorkingCopyPath = workingCopyPath;
      }

      public ScmCapabilities Capabilities { get; protected set; }

      public virtual void Dispose()
      {
         
      }

      public abstract string ClientVersion { get; }

      public abstract string Branch { get; set; }

      public abstract string MainBranchName { get; }

      public abstract string RelativeRoot { get; }

      public abstract string GetDiff(RevisionRange range);

      public abstract IEnumerable<WorkItem> GetHistory(int maxEntries);

      protected string Exec(string command, params object[] parameters)
      {
         if (command == null) return null;

         string tempFile = GetTempFilePath(".cmd");

         string formattedCommand = string.Format(command, parameters);
         formattedCommand = string.Format("/C {0} {1} >\"{2}\"", _execName, formattedCommand, tempFile);
         var start = new ProcessStartInfo("cmd", formattedCommand);
         start.WorkingDirectory = WorkingCopyPath;
         start.UseShellExecute = false;
         start.RedirectStandardOutput = false;
         start.RedirectStandardError = true;
         start.CreateNoWindow = true;

         try
         {
            Process p = Process.Start(start);
            p.WaitForExit();

            if (p.ExitCode != 0)
            {
               string error = p.StandardError.ReadToEnd();

               if (error.IndexOf("argument", StringComparison.InvariantCultureIgnoreCase) != -1) throw new ArgumentException(error);

               throw new VersionControlException("{0} (code: {1}, command: {2})", error, p.ExitCode, formattedCommand);
            }

            if (!File.Exists(tempFile)) return null;

            return File.ReadAllText(tempFile);
         }
         finally
         {
            if (File.Exists(tempFile))
            {
               File.Delete(tempFile);
            }
         }
      }

      protected string GetTempFilePath(string extension)
      {
         return Path.Combine(Path.GetTempPath(), string.Format("irb-{0}{1}", Guid.NewGuid(), extension));
      }
   }
}
