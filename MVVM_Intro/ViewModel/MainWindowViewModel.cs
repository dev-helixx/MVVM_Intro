using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using MVVM_Intro.Model;

namespace MVVM_Intro.ViewModel
{

  public class MainWindowViewModel : ViewModel
  {


    public const string path = @"c:\tmp\model.txt";
    public CustomersViewModel CustomersViewModel { get; set; }
    public PeterViewModel PeterViewModel { get; set; }

    public Command.Command LoadCommand { get; set; }
    public Command.Command SaveCommand { get; set; }




    private bool _canExecuteControl;
    public bool CanExecuteControl
    {
      get { return _canExecuteControl; }
      set
      {
        if (value != _canExecuteControl)
        {
          _canExecuteControl = value;
          OnPropertyChanged(nameof(CanExecuteControl));
        }
      }
    }

    /* If any changes have been made to any of the UI fields */
    private bool _dirty;
    public bool Dirty
    {
      get { return _dirty; }
      set
      {
        if(value != _dirty)
        {
          _dirty = value;
          OnPropertyChanged(nameof(Dirty));
        }
      }
    }

    private MainModel mm;
    public MainWindowViewModel(MainModel mm)
    {

      this.mm = mm;

      CustomersViewModel = new CustomersViewModel(mm.Customers);
      CustomersViewModel.PropertyChanged += CustomersViewModel_PropertyChanged;

      PeterViewModel = new PeterViewModel(mm.PetersSaying);
      PeterViewModel.PropertyChanged += PeterViewModel_PropertyChanged;

      CanExecuteControl = true;

      /* Register whenever notifypropertychanged is called, RaiseCanExecuteChanged will be called on the all registered commands aswell*/
      RegisterCommand(LoadCommand = new Command.Command(Load, CanLoad));
      RegisterCommand(SaveCommand = new Command.Command(Save, CanSave));

      /* Lav det sådan, at der kommer et raisedpropertychanged event på det indre i custeomersviewmodel, da der er flere elemener (Firstname, Lastname etc). Dvs at hvert element skal have
       en raisedpropertychanged på sig, før at den kan tjekke for alle elementer. */
    }

    private void CustomersViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {


      // Gets called twice since both fx firstname and fullname is changed (assumed)
      // First time, Dirty is set to true and the save button is reenabled
      // Second time, we can avoid another raise if Dirty already has been set to true
      if(!Dirty)
      {
        //MessageBox.Show(e.PropertyName);
        Dirty = true;
      }
      
    }

    private void PeterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      //MessageBox.Show(e.PropertyName);
      Dirty = true;
    }

    public void LoadValues(MainModel mm)
    {
      CustomersViewModel.LoadValues(mm.Customers);
      PeterViewModel.LoadValues(mm.PetersSaying);

      Dirty = false;
    }

    public void Load()
    {

      MainModel mainModel = new MainModel(MainWindow.path);

      LoadValues(mainModel);

      MessageBox.Show("Values loaded:" );

      CanExecuteControl = true;
      
   
    }

    public void Save()
    {
      /* Xml Serilizer to write data to an existing txt file */
      XmlSerializer x = new XmlSerializer(typeof(MainModel));
      if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
      {
        using (TextWriter tw = new StreamWriter(path))
        {
          // Update main model object with new values from textboxes
          mm.Customers = CustomersViewModel.SaveValues();
          mm.PetersSaying = PeterViewModel.Peter; 

          x.Serialize(tw, mm);
        }
        MessageBox.Show("Values saved");
      }

      CanExecuteControl = true;
      Dirty = false;
      
     
    }
    public bool CanLoad()
    {
      return CanExecuteControl;
    }

    public bool CanSave()
    {
      
      return Dirty;
    }

  }
}
