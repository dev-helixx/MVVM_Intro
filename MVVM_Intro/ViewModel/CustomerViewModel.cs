using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM_Intro.Model;

namespace MVVM_Intro.ViewModel
{
  public class CustomerViewModel : BaseViewModel
  {

    private CustomerModel Customer;


    public CustomerViewModel() { }

    public CustomerViewModel(CustomerModel customer)
    {
      Customer = customer;
      LoadValues();
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


    public void LoadValues()
    {

      FirstName = Customer.FirstName;
      LastName = Customer.LastName;

    }

    public void EditMode()
    {
      // Enable delete button visibiliy
      ButtonVisibility = !ButtonVisibility ? true : false;
    }

    public CustomerModel SaveValues()
    {
      return new CustomerModel { FirstName = FirstName, LastName = LastName };
    }

  }
}
