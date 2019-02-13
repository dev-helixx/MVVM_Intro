using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Intro.Command
{
  public class Command : ICommand
  {
    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, null);
    }

    public Command(Action execute, Func<bool> canExecute)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    private Action execute { get; set; }
    private Func<bool> canExecute { get; set; }

    public bool CanExecute(object parameter)
    {
      return canExecute();
    }

    public void Execute(object parameter)
    {
      execute();
    }
  }
}
