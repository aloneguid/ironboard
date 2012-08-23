using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Model
{
   public class User
   {
      public User()
      {
         
      }

      public User(long id, string userName, string firstName, string lastName, string fullName, string email, string url)
      {
         this.Id = id;
         this.Username = userName;
         this.FirstName = firstName;
         this.LastName = lastName;
         this.FullName = fullName;
         this.Email = email;
         this.Url = url;
      }

      public long Id { get; set; }

      public string Username { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string FullName { get; set; }

      public string Email { get; set; }

      public string Url { get; set; }
   }
}
