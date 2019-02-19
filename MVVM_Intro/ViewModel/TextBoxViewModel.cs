using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Intro.ViewModel
{
  public class TextBoxViewModel : BaseViewModel
  {

  

    public TextBoxViewModel(string content)
    {
      // Method for populating the textbox
      LoadValues(content);
    }

    private string _textBox;
    public string TextBox
    {
      get { return _textBox; }
      set
      {
        if (value != _textBox)
        {
          _textBox = value;
          OnPropertyChanged("TextBox Content Changed");
        }
      }
    }

    public void LoadValues(string content)
    {
      TextBox = content;
    }

  }
}
