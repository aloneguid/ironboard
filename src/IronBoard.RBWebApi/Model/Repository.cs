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

      /// <summary>
      /// Internal ReviewBoard's repository ID
      /// </summary>
      public string Id { get; set; }

      /// <summary>
      /// Public name
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Repository tool, usually it's the name of version control system like Git or Svn
      /// </summary>
      public string Tool { get; set; }

      /// <summary>
      /// Repository path, it's version control system specific
      /// </summary>
      public string Path { get; set; }

      public override string ToString()
      {
         return string.Format("{0} ({1})", Name, Path);
      }
   }
}
