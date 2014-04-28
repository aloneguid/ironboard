using System;
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

      public abstract string GetLocalDiff();

      protected string Exec(string command, params object[] parameters)
      {
         var start = new ProcessStartInfo(_execName, string.Format(command, parameters));
         start.WorkingDirectory = WorkingCopyPath;
         start.UseShellExecute = false;
         start.RedirectStandardOutput = true;

         Process p = Process.Start(start);
         p.WaitForExit();

         if(p.ExitCode != 0) throw new ApplicationException("cannot execute");

         using (var s = p.StandardOutput)
         {
            string result = s.ReadToEnd();
            return result.Trim();
         }
      }
   }
}
