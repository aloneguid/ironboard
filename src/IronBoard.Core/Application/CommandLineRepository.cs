using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IronBoard.Core.Model;

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

      public virtual void Dispose()
      {
         
      }

      public abstract string ClientVersion { get; }

      public abstract string Branch { get; }

      public abstract string RelativeRoot { get; }

      public abstract Uri RemoteRepositoryUri { get;}

      public abstract string RelativeRepositoryUri { get; }

      public abstract string GetLocalDiff();

      public abstract string GetDiff(string @from, string to);

      public abstract IEnumerable<WorkItem> GetHistory(int maxEntries);

      protected string Exec(string command, params object[] parameters)
      {
         var start = new ProcessStartInfo(_execName, string.Format(command, parameters));
         start.WorkingDirectory = WorkingCopyPath;
         start.UseShellExecute = false;
         start.RedirectStandardOutput = true;
         start.RedirectStandardError = true;

         Process p = Process.Start(start);
         p.WaitForExit();

         if (p.ExitCode != 0)
         {
            string error = p.StandardError.ReadToEnd();
            throw new ApplicationException(
               string.Format("cannot execute, exit code: {0}, message: [{1}]",
                  p.ExitCode,
                  error));
         }

         return p.StandardOutput.ReadToEnd();
      }
   }
}
