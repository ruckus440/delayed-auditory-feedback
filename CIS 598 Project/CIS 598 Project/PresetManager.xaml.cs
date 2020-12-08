using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PresetManager.xaml
    /// </summary>
    public partial class PresetManager : Window
    {
        public BindingList<Preset> Presets { get; set; }
        static Controller c = new Controller();


        public PresetManager()
        {
            InitializeComponent();
            c.GetPresets(Callback);
            DataContext = this;
        }

        public void Callback(BindingList<Preset> bl)
        {

            Presets = bl;
        }


        private void uxMoveUpBtn_Click(object sender, RoutedEventArgs e)
        {   
            int index = uxPresetListBox.SelectedIndex;
            if (index > 0)
            {
                Preset item = (Preset)uxPresetListBox.SelectedItem;
                Presets.RemoveAt(uxPresetListBox.SelectedIndex);
                Presets.Insert(index - 1, item);
                uxPresetListBox.SelectedIndex = index - 1;
            }
        }

        private void uxMoveDownBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = uxPresetListBox.SelectedIndex;
            if (index < Presets.Count - 1)
            {
                Preset item = (Preset)uxPresetListBox.SelectedItem;
                Presets.RemoveAt(uxPresetListBox.SelectedIndex);
                Presets.Insert(index + 1, item);
                uxPresetListBox.SelectedIndex = index + 1;
            }

        }

        private void uxDeletePreset_Click(object sender, RoutedEventArgs e)
        {
            
            Presets.RemoveAt(uxPresetListBox.SelectedIndex);
        }

        private void uxAddPresetBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
