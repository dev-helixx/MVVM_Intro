using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using MVVM_Intro.Model;
using MVVM_Intro.ViewModel;

namespace MVVM_Intro
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    /*****************************************************************************************************************************
     * NEW CHANGES:
     * Opret en filewatcher der kigger på DB filen. Når der opdages ændringer i filen, tjekkes det op imod det der allerede 
     * ligger lokalt i programmet og på den måde kan vi vurdere om Load knappen skal aktiveres eller deaktiveres.
     *
     * Når man først gang henter data op i modellen, skal Load være deaktiv. Dette gemmes lokalt i programmet. 
     * Hvis der sker ændringer i DB filen, fx slet eller nogle 
     * værdier ændres, skal filewatcheren tjekke det der allerede lægger lokalt ift de ændringer der er regisreret i filen.
     * Hvis de stadig matcher 100% skal knappen stadig være deaktiv.
     * Hvis det ikke matcher, skal Load knappen aktiveres, således at man kan hente de nye værdier ind. 
     *  
     *  
     *****************************************************************************************************************************/


    //Model
    public const string path = @"c:\tmp\model.txt"; //DB file
    private MainModel mainModel;


    //ViewModel
    public MainWindowViewModel MainWindowViewModel { get; set; }

    public MainWindow()
    {
      //Reading model

      mainModel = new MainModel(path);


      MainWindowViewModel = new MainWindowViewModel(mainModel);
      // Set datacontext to that of MainWindowViewModel which controls the other viewModels
      DataContext = MainWindowViewModel;

      InitializeComponent();
    }
  }
}
