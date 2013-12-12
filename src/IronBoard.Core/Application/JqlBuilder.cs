using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IronBoard.Core.Application
{
   class JqlBuilder
   {
      private readonly StringBuilder _qry = new StringBuilder();

      public JqlBuilder()
      {
         
      }

      public JqlBuilder Add(string parameter, string value)
      {
         if (value != null)
         {
            if (_qry.Length > 0) _qry.Append(" AND ");

            _qry.Append(parameter);
            _qry.Append(" = \"");
            _qry.Append(value);
            _qry.Append("\"");
         }

         return this;
      }

      public override string ToString()
      {
         return _qry.ToString();
      }
   }
}
