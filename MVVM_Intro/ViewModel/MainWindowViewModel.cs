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
using MVVM_Intro.Command;
using MVVM_Intro.Helpers;
using MVVM_Intro.Model;

namespace MVVM_Intro.ViewModel
{

  public class MainWindowViewModel : BaseViewModel
  {


    public CustomersViewModel CustomersViewModel { get; set; }
    public TextBoxViewModel TextBoxViewModel { get; set; }

    public ActionCommand LoadCommand { get; set; }
    public ActionCommand SaveCommand { get; set; }




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

      TextBoxViewModel = new TextBoxViewModel(mm.TextBoxContent);
      TextBoxViewModel.PropertyChanged += TextBoxViewModel_PropertyChanged;

     
      CanExecuteControl = true;

      /* Register whenever notifypropertychanged is called, RaiseCanExecuteChanged will be called on the all registered commands aswell*/
      RegisterCommand(LoadCommand = new ActionCommand(Load, CanLoad));
      RegisterCommand(SaveCommand = new ActionCommand(Save, CanSave));

    }

    private void CustomersViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {


      // Gets called twice since both fx firstname and fullname is changed.
      // First time, Dirty is set to true and the save button is reenabled
      // Second time, we can avoid another raise if Dirty already has been set to true
      if(!Dirty)
      {
        Dirty = true;
      }
      
    }

    private void TextBoxViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      Dirty = true;
    }

    public void LoadValues(MainModel mm)
    {
      CustomersViewModel.LoadValues(mm.Customers);
      TextBoxViewModel.LoadValues(mm.TextBoxContent);

      Dirty = false;
    }

    public void Load()
    {

      MainModel mainModel = new MainModel();

      LoadValues(mainModel);

      MessageBox.Show("Values loaded:" );

      CanExecuteControl = true;
      
   
    }

    public void Save()
    {
      /* Xml Serilizer to write data to an existing txt file */
      XmlSerializer x = new XmlSerializer(typeof(MainModel));
      if (!string.IsNullOrWhiteSpace(StaticResources.DBPath) && File.Exists(StaticResources.DBPath))
      {
        using (TextWriter tw = new StreamWriter(StaticResources.DBPath))
        {
          // Update main model object with new values from textboxes
          mm.Customers = CustomersViewModel.SaveValues();
          mm.TextBoxContent = TextBoxViewModel.TextBox; 

          x.Serialize(tw, mm);
        }
        MessageBox.Show("Values saved");
      }

      CanExecuteControl = true;
      Dirty = false;
      
     
    }
    public bool CanLoad()
    {
      return !CanExecuteControl;
    }

    public bool CanSave()
    {
      
      return Dirty;
    }

  }
}
