using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Model
{
   public class Repository
   {
      public Repository()
      {
         
      }

      public Repository(string id, string name, string tool, string path)
      {
         Id = id;
         Name = name;
         Tool = tool;
         Path = path;
      }

      public string Id { get; set; }

      public string Name { get; set; }

      public string Tool { get; set; }

      public string Path { get; set; }
   }
}
