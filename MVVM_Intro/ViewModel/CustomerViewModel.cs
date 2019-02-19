using System;
using System.Collections.Generic;
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
  public class CustomerViewModel : BaseViewModel
  {

    #region Private Fields
    private CustomerModel Customer;
    private CustomersViewModel cvm;
    #endregion

    #region Public Property fields
    public ActionCommand DeleteCommand { get; set; }
    #endregion

    #region Constructors

    public CustomerViewModel() { }

    public CustomerViewModel(CustomerModel customer, CustomersViewModel cvm)
    {
      this.cvm = cvm;
      RegisterCommand(DeleteCommand = new ActionCommand(Delete, CanDelete));
      CanDeleteControl = true;
      Customer = customer;
      LoadValues();
    }
    #endregion


    #region Public Properties

    private bool _canDeleteControl;
    public bool CanDeleteControl
    {
      get { return _canDeleteControl; }
      set
      {
        if (value != _canDeleteControl)
        {
          _canDeleteControl = value;
          OnPropertyChanged(nameof(CanDeleteControl));
        }
      }
    }



    private bool _buttonVisibility;
    public bool ButtonVisibility
    {
      get { return _buttonVisibility; }
      set
      {
        if (value != _buttonVisibility)
        {
          _buttonVisibility = value;
          OnPropertyChanged(nameof(ButtonVisibility));
        }
      }
    }


    private string _firstName;
    public string FirstName
    {
      get { return _firstName; }
      set
      {
        if (value != _firstName)
        {
          _firstName = value;
          OnPropertyChanged(nameof(FirstName));
          OnPropertyChanged(nameof(FullName));
        }
      }
    }

    private string _lastName;
    public string LastName
    {
      get { return _lastName; }

      set
      {
        if (_lastName != value)
        {
          _lastName = value;

          OnPropertyChanged(nameof(LastName));
          OnPropertyChanged(nameof(FullName));
        }
      }
    }

    public string FullName
    {
      get
      {
        return FirstName + " " + LastName;
      }
    }

    #endregion

    #region Methods

    public void LoadValues()
    {

      FirstName = Customer.FirstName;
      LastName = Customer.LastName;

    }


    private bool CanDelete()
    {
      return CanDeleteControl;
    }

    private void Delete()
    {

      cvm.DeleteValues(this);

      //MessageBox.Show(Customer.FullName);

      //XmlSerializer x = new XmlSerializer(typeof(MainModel));
      //if (!string.IsNullOrWhiteSpace(StaticResources.DBPath) && File.Exists(StaticResources.DBPath))
      //{
      //  using (TextWriter tw = new StreamWriter(StaticResources.DBPath))
      //  {
      //    // Update main model object with new values from textboxes
         
      //    //mm.TextBoxContent = TextBoxViewModel.TextBox;

      //    x.Serialize(tw, mm);
      //  }
      //  MessageBox.Show("Deleted entry.");
      //}

    }


    public void EditMode(bool EditIsChecked)
    {
      // Enable delete button visibiliy
      ButtonVisibility = EditIsChecked ? true : false;
      //MessageBox.Show("EditIsChecked: " + EditIsChecked);
    }

    public CustomerModel SaveValues()
    {
      return new CustomerModel { FirstName = FirstName, LastName = LastName };
    }

    #endregion

  }
}
