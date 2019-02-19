using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MVVM_Intro.ViewModel;

namespace MVVM_Intro.Model
{
  [Serializable]
  public class MainModel : BaseViewModel
  {

    public List<CustomerModel> Customers { get; set; }
    public string TextBoxContent { get; set; }


    public MainModel()
    {
      Customers = new List<CustomerModel>();
    }

    public MainModel(string DBPath)
    {
     
      // Fetch data from DB file
      MainModel mainModel = new MainModel();
      XmlSerializer x = new XmlSerializer(typeof(MainModel));
      using (TextReader tr = new StreamReader(DBPath))
      {
        mainModel = (MainModel)x.Deserialize(tr);
      }

      // Initialize properties with data fetched from DB
      Customers = mainModel.Customers;
      TextBoxContent = mainModel.TextBoxContent;
    }


  }
}
