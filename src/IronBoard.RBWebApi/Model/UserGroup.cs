using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Model
{
   public class UserGroup : Reviewer
   {
      public UserGroup() : base(null, null)
      {
         
      }

      public UserGroup(long id, string name, string displayName, string uri, bool isVisible, bool inviteOnly, string mailingList) : base(name, displayName)
      {
         this.Id = id;
         this.Uri = uri;
         this.IsVisible = isVisible;
         this.InviteOnly = inviteOnly;
         this.MailingList = mailingList;
      }

      public long Id { get; set; }

      public string Uri { get; set; }

      public bool IsVisible { get; set; }

      public bool InviteOnly { get; set; }

      public string MailingList { get; set; }
   }
}
