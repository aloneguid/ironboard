using System.IO;
using IronBoard.RBWebApi;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;

namespace IronBoard.Vsix.Package
{
   public partial class Package
   {
      private const string SettingsRoot = "IronBoard\\Common";
      private const string SettingsKey = "CoreSettings";
      private static WritableSettingsStore _settingsStore;

      private DirectoryInfo GetPropAsDir(__VSPROPID prop)
      {
         IVsSolution solution = GetService(typeof(SVsSolution)) as IVsSolution;

         if (solution != null)
         {
            object objSolutionDir;

            solution.GetProperty((int)prop, out objSolutionDir);

            if (objSolutionDir != null)
            {
               string solutionDir = objSolutionDir.ToString();

               return new DirectoryInfo(solutionDir);
            }
         }

         return null;
      }

      private DirectoryInfo SolutionDirectory
      {
         get { return GetPropAsDir(__VSPROPID.VSPROPID_SolutionDirectory); }
      }

      private WritableSettingsStore GetWritableSettingsStore(string settingsRoot)
      {
         SettingsManager settingsManager = new ShellSettingsManager(this);
         WritableSettingsStore userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
         if (!userSettingsStore.CollectionExists(settingsRoot))
         {
            userSettingsStore.CreateCollection(settingsRoot);
         }
         return userSettingsStore;
      }

      public void SaveOption(string key, string value)
      {
         _settingsStore.SetString(SettingsRoot, key, value);
      }

      public string ReadOption(string key)
      {
         if (_settingsStore.PropertyExists(SettingsRoot, key))
            return _settingsStore.GetString(SettingsRoot, key);

         return null;
      }

      public bool ConfigExists
      {
         get { return RBUtils.FindConfigFolder(SolutionDirectory.FullName) != null; }
      }
   }
}
