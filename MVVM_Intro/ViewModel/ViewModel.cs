using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Intro.ViewModel
{
  public class ViewModel : INotifyPropertyChanged
  {
    List<Command.Command> commands = new List<Command.Command>();


    /* */
    public void RegisterCommand(Command.Command command)
    {
      commands.Add(command);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string property)
    {
      foreach(Command.Command command in commands)
      {
        command.RaiseCanExecuteChanged();
      }

      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

  }
}
