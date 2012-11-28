namespace IronBoard.RBWebApi.Model
{
   public class Reviewer
   {
      protected Reviewer(string internalName, string name)
      {
         InternalName = internalName;
         Name = name;
      }

      public string InternalName { get; set; }

      public string Name { get; set; }

      public override string ToString()
      {
         return string.IsNullOrWhiteSpace(Name) ? InternalName : Name;
      }
   }
}
