using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MVVM_Intro.Helpers;
using MVVM_Intro.ViewModel;

namespace MVVM_Intro.FileHandler
{
  public class Filewatcher
  {
    /* Private Member Fields */
    private string DBFilepath = StaticResources.DBPath;
    private FileSystemWatcher watcher;
    private MainWindowViewModel mvvm;

    public Filewatcher(MainWindowViewModel mvvm)
    {
      this.mvvm = mvvm;
    }

    public void Init()
    {

      // Create a new FileSystemWatcher and set its properties.
      watcher = new FileSystemWatcher();


      watcher.Path = Path.GetDirectoryName(DBFilepath);
      // Watch for changes in LastAccess and LastWrite times, and
      // the renaming of files or directories.
      watcher.NotifyFilter = NotifyFilters.LastAccess
                           | NotifyFilters.LastWrite
                           | NotifyFilters.FileName
                           | NotifyFilters.DirectoryName;

      // Only watch single file.
      watcher.Filter = Path.GetFileName(DBFilepath);

      // Add event handlers.
      watcher.Changed += OnChanged;
      watcher.Deleted += OnChanged;

      // Begin watching.
      watcher.EnableRaisingEvents = true;



    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
      if(e.ChangeType == WatcherChangeTypes.Changed)
      {

        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => mvvm.CanExecuteControl = true));
        
      }
      else if(e.ChangeType == WatcherChangeTypes.Deleted)
      {
        //MessageBox.Show("File has been deleted");
      }

    }


  }
}
