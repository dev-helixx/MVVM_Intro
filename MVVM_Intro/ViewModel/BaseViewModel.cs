using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Intro.Command;

namespace MVVM_Intro.ViewModel
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    List<ActionCommand> commands = new List<ActionCommand>();


    /* */
    public void RegisterCommand(ActionCommand command)
    {
      commands.Add(command);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string property)
    {
      foreach(ActionCommand command in commands)
      {
        command.RaiseCanExecuteChanged();
      }

      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

  }
}
