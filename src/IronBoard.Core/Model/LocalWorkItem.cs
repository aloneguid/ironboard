using System;
using System.IO;
using SharpSvn;

namespace IronBoard.Core.Model
{
   public class LocalWorkItem
   {
      internal LocalWorkItem(SvnStatusEventArgs svn)
      {
         this.Path = svn.FullPath;
         this.Status = ToStatus(svn.LocalNodeStatus);
      }

      private LocalItemStatus ToStatus(SvnStatus svn)
      {
         LocalItemStatus status;
         if (!Enum.TryParse(svn.ToString(), true, out status)) status = LocalItemStatus.Unknown;
         return status;
      }

      public string Path { get; private set; }

      public LocalItemStatus Status { get; private set; }

      public FileInfo File { get { return new FileInfo(Path); } }

      public override string ToString()
      {
         return string.Format("{0} ({1})", Path, Status);
      }
   }
}
