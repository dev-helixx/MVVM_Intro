using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVVM_Intro.Model
{
  public class MainModel : ViewModel.ViewModel
  {

    /* Model class to hold different properties */

    public List<CustomerModel> Customers { get; set; }
    public string PetersSaying { get; set; }

    public MainModel()
    {
      Customers = new List<CustomerModel>();
    }

    public MainModel(string filePath)
      :this()
    {
      MainModel mainModel = new MainModel();
      XmlSerializer x = new XmlSerializer(typeof(MainModel));
      using (TextReader tr = new StreamReader(filePath))
      {
        mainModel = (MainModel)x.Deserialize(tr);
      }
      Customers = mainModel.Customers;
      PetersSaying = mainModel.PetersSaying;
    }


  }
}
