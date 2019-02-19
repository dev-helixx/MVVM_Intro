using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM_Intro.Command;

namespace MVVM_Intro.ViewModel
{
  [Serializable]
  public class BaseViewModel : INotifyPropertyChanged
  {
    private readonly BaseViewModel Parent;


    public BaseViewModel(BaseViewModel parent)
    {
      Parent = parent;
    }

    public BaseViewModel()
    {
    }

    // List containing various commands (button handlers)
    List<ActionCommand> commands = new List<ActionCommand>();


    /* Method for adding commands to a list */
    public void RegisterCommand(ActionCommand command)
    {
      commands.Add(command);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged(string property)
    {

      foreach (ActionCommand command in commands)
      {
        command.RaiseCanExecuteChanged();
      }

      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

  }

  #region Publisher/Subscriber implementation

  //public delegate void PubSubEventHandler<T>(object sender, PubSubEventArgs<T> args);

  //public class PubSubEventArgs<T> : EventArgs
  //{
  //  public T Item { get; set; }

  //  public PubSubEventArgs(T item)
  //  {
  //    Item = item;
  //  }
  //}

  //public static class PubSub<T>
  //{
  //  private static Dictionary<string, PubSubEventHandler<T>> events =
  //          new Dictionary<string, PubSubEventHandler<T>>();

  //  public static void AddEvent(string name, PubSubEventHandler<T> handler)
  //  {
  //    if (!events.ContainsKey(name))
  //      events.Add(name, handler);
  //  }
  //  public void RaiseEvent(string name, object sender, PubSubEventArgs<T> args)
  //  {
  //    if (events.ContainsKey(name) && events[name] != null)
  //      events[name](sender, args);

  //    Parent?.RaiseEvent(...)
  //  }
  //  public static void RegisterEvent(string name, PubSubEventHandler<T> handler)
  //  {
  //    if (events.ContainsKey(name))
  //      events[name] += handler;
  //  }
  //}
  #endregion
}
