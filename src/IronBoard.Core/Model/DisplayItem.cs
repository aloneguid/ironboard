using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.Core.Model
{
   class DisplayItem<T>
   {
      public DisplayItem(string name, T data, int index = -1)
      {
         this.Name = name;
         this.Data = data;
         this.Index = index;
      }

      public string Name { get; set; }

      public int Index { get; set; }

      public T Data { get; set; }

      public override string ToString()
      {
         return Name;
      }
   }
}
