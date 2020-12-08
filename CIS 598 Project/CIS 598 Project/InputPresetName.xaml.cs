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
using System.Windows.Shapes;

namespace CIS_598_Project
{
    /// <summary>
    /// Interaction logic for InputPresetName.xaml
    /// </summary>
    public partial class InputPresetName : Window
    {
        public InputPresetName()
        {
            InitializeComponent();
        }

        private void uxOkPreset_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
