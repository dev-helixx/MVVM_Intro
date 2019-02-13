using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Intro.Model;

namespace MVVM_Intro.ViewModel
{
    public class CustomerViewModel : ViewModel
    {

    private CustomerModel Customer;

    public CustomerViewModel(CustomerModel customer)
    {
      Customer = customer;

      LoadValues();
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

    public CustomerModel SaveValues()
    {
      return new CustomerModel { FirstName = FirstName, LastName = LastName };
    }

  }
}
