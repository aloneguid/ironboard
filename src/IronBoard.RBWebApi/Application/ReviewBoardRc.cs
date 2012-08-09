using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Application
{
   class ReviewBoardRc
   {
      private const string KeyReviewBoardUrl = "REVIEWBOARD_URL";
      private const string KeyRepository = "REPOSITORY";

      private readonly string _configPath;

      public ReviewBoardRc(string rootFolder)
      {
         if (rootFolder == null) throw new ArgumentNullException("rootFolder");
         if(!Directory.Exists(rootFolder)) throw new DirectoryNotFoundException("rootFolder");
         _configPath = Path.Combine(rootFolder, ".reviewboardrc");
         if (!File.Exists(_configPath)) throw new FileNotFoundException("config file not found", _configPath);
      }

      private void ReadConfiguration()
      {
         
      }

      public Uri Uri { get; private set; }

      public string Repository { get; private set; } 
   }
}
