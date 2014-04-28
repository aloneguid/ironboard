using System.Collections.Generic;
using System.IO;
using IronBoard.Core.Model;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   class PendingChangesPresenter
   {
      private readonly IPendingChangesView _view;
      private FileSystemWatcher _fsWatcher;

      public PendingChangesPresenter(IPendingChangesView view)
      {
         _view = view;
      }

      private void Initialise()
      {
         if (_fsWatcher == null && IbApplication.SolutionPath != null)
         {
            _fsWatcher = new FileSystemWatcher(IbApplication.SolutionPath);
            _fsWatcher.IncludeSubdirectories = true;
            _fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fsWatcher.Changed += _fsWatcher_Changed;
            _fsWatcher.EnableRaisingEvents = true;
         }
      }

      void _fsWatcher_Changed(object sender, FileSystemEventArgs e)
      {
         _view.RefreshView();
      }

      public IEnumerable<LocalWorkItem> GetPendingChanges()
      {
         Initialise();

         //going to remove pending changes anyways
         //return IbApplication.CodeRepository.GetLocalChanges();
         return null;
      }

      public void OpenInBrowser(Review r)
      {
          string url = string.Format("{0}/r/{1}", IbApplication.RbClient.ServerUri, r.Id);
          IbApplication.OpenBrowserWindow(url, false);
      }

      public string GetDetailsTitle()
      {
          return string.Format(Strings.ReviewDetails_NewTicket_PreCommit, IbApplication.CodeRepository.Branch);
      }
   }
}
