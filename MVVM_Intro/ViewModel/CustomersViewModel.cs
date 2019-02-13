using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM_Intro.Model;

namespace MVVM_Intro.ViewModel
{
  public class CustomersViewModel : ViewModel
  {

    /* Constructor */

    public CustomersViewModel(List<CustomerModel> customers)
    {
      Customers = new ObservableCollection<CustomerViewModel>();
      LoadValues(customers); 
    }

  
    public ObservableCollection<CustomerViewModel> Customers { get; set; }


    public List<CustomerModel> SaveValues()
    {
      List<CustomerModel> result = new List<CustomerModel>();

      foreach (var customer in Customers)
      {
        result.Add(customer.SaveValues());
      }

      return result;
    } 

    public void LoadValues(List<CustomerModel> customers)
    {
      Customers.Clear();

      foreach (var customer in customers)
      {


        CustomerViewModel cvm = new CustomerViewModel(customer);
        // Attach P ropertyChanged event to each object in the collection and act accordingly to its changes
        cvm.PropertyChanged += Cvm_PropertyChanged;
        
        Customers.Add(cvm);

      }
    }

    private void Cvm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      //Whenever af change is made to an object in the collection, this event gets called
      OnPropertyChanged(nameof(Customers));
    }
  }
}
