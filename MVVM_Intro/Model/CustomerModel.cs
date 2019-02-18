using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Intro.Model
{
  public class CustomerModel
  {

    #region Properties

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName
    {
      get
      {
        return FirstName + " " + LastName;
      }
    }


    #endregion

  } 
  
}
