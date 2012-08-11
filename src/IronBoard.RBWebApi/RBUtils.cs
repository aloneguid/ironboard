using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi
{
   public static class RBUtils
   {
      internal const string ConfigFileName = ".reviewboardrc";

      public static string FindConfigFolder(string startFolder)
      {
         if (startFolder == null) throw new ArgumentNullException("startFolder");

         var di = new DirectoryInfo(startFolder);

         while(di != null)
         {
            string fileName = Path.Combine(di.FullName, ConfigFileName);
            if (File.Exists(fileName)) return di.FullName;
            di = di.Parent;
         }

         return null;
      }
   }
}
