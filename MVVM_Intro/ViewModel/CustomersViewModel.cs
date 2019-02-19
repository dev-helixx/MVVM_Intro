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
  public class CustomersViewModel : BaseViewModel
  {

    private List<CustomerModel> customers;

    public ObservableCollection<CustomerViewModel> Customers { get; set; }


    public CustomersViewModel() { }
    /* Constructor */
    public CustomersViewModel(List<CustomerModel> customers)
    {
      this.customers = customers;
      Customers = new ObservableCollection<CustomerViewModel>();
      Customers.CollectionChanged += Customers_CollectionChanged;
      LoadValues(customers); 
    }

 
    public List<CustomerModel> SaveValues()
    {

      List<CustomerModel> result = new List<CustomerModel>();

      foreach (var customer in Customers)
      {
        // Overrides existing content in the list
        result.Add(customer.SaveValues());
      }

      return result;
    } 

    public void LoadValues(List<CustomerModel> customers)
    {
      Customers.Clear();

      foreach (var customer in customers)
      {

        CustomerViewModel cvm = new CustomerViewModel(customer, this);
        // Attach PropertyChanged event to each object in the collection
        cvm.PropertyChanged += Cvm_PropertyChanged;
        Customers.Add(cvm);

      }
    }

    private void Customers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      // Register changes to the collection itself, not just object's attributes
      OnPropertyChanged(nameof(Customers_CollectionChanged));
    }


    private void Cvm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      //Whenever af change is made to an >>object's attribute<< in the collection, this event gets called
      OnPropertyChanged("Customers Object Property Changed");
    }

    public void EditMode(bool EditIsChecked)
    {
      // For each child (CustomerModel) in the list, make the delete button visible
      foreach (var customer in Customers)
      {
        customer.EditMode(EditIsChecked);
      }
    }

    public void DeleteValues(CustomerViewModel vm)
    {
      // CustomerViewModel gives videre og vi kan dermed slette objektet direkte fremfor at løbe en liste igennem
      Customers.Remove(vm);
      

    }

  }
}
