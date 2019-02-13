using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Intro.ViewModel
{
  public class PeterViewModel : ViewModel
  {

  

    public PeterViewModel(string content)
    {
      LoadValues(content);
    }

    private string _peter;
    public string Peter
    {
      get { return _peter; }
      set
      {
        if (value != _peter)
        {
          _peter = value;
          OnPropertyChanged(nameof(Peter));
        }
      }
    }

    public void LoadValues(string content)
    {
      Peter = content;
    }

  }
}
