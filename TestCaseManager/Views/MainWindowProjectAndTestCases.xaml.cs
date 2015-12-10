using System;
using System.Collections.Generic;
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
using TestCaseManager.Utilities;

namespace TestCaseManager.Pages
{
    /// <summary>
    /// Interaction logic for MainWindowProjectAndTestCases.xaml
    /// </summary>
    public partial class MainWindowProjectAndTestCases : UserControl
    {
        public MainWindowProjectAndTestCases()
        {
            InitializeComponent();   
        }
        
        private void tvEmps_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object current;

            current = tvEmps.SelectedItem;
            switch (current.GetType().Name.ToLower())
            {
                case "employee":
                    break;
                case "employeetype":
                    break;
                case "subemployee":
                    break;
                default:
                    break;
            }
        }
    }
}
