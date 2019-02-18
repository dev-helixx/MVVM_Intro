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
  public class BaseViewModel : INotifyPropertyChanged
  {



    public BaseViewModel()
    {

    }


    List<ActionCommand> commands = new List<ActionCommand>();


    /* */
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


  /************************* Publisher/Subscriber implementation *************************/

  public delegate void PubSubEventHandler<T>(object sender, PubSubEventArgs<T> args);

  public class PubSubEventArgs<T> : EventArgs
  {
    public T Item { get; set; }

    public PubSubEventArgs(T item)
    {
      Item = item;
    }
  }

  public static class PubSub<T>
  {
    private static Dictionary<string, PubSubEventHandler<T>> events =
            new Dictionary<string, PubSubEventHandler<T>>();

    public static void AddEvent(string name, PubSubEventHandler<T> handler)
    {
      if (!events.ContainsKey(name))
        events.Add(name, handler);
    }
    public static void RaiseEvent(string name, object sender, PubSubEventArgs<T> args)
    {
      if (events.ContainsKey(name) && events[name] != null)
        events[name](sender, args);
    }
    public static void RegisterEvent(string name, PubSubEventHandler<T> handler)
    {
      if (events.ContainsKey(name))
        events[name] += handler;
    }
  }
  /**************************************************************************************/

}
