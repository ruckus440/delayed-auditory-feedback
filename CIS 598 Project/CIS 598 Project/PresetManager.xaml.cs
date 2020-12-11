/*
 * PresetManager.xaml.cs
 * Author: Mike Ruckert
 * CIS 598 Senior Project - Kansas State University
 * 
 * PresetManager view of the DAF/AAF app.
 */

using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CIS_598_Project
{

    /// <summary>
    /// Interaction logic for PresetManager.xaml
    /// </summary>
    public partial class PresetManager : Window
    {
        public BindingList<Preset> Presets { get; set; }
        static Controller c = new Controller();

        /// <summary>
        /// Constructor
        /// </summary>
        public PresetManager()
        {
            InitializeComponent();
            c.GetPresets(Callback);
            DataContext = this;
        }

        /// <summary>
        /// Callback function for passing the list of Preset via delegate.
        /// </summary>
        /// <param name="bl"></param>
        public void Callback(BindingList<Preset> bl)
        {
            Presets = bl;
        }

        /// <summary>
        /// Event handler for clicking uxMoveUpBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event handler for clicking uxMoveDownBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event handler for clicking uxDeletePreset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxDeletePreset_Click(object sender, RoutedEventArgs e)
        {
            Presets.RemoveAt(uxPresetListBox.SelectedIndex);
        }

        /// <summary>
        /// Event handler for clicking uxRenamePresetBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxRenamePresetBtn_Click(object sender, RoutedEventArgs e)
        {
            int index;
            string ssstring;
            InputPresetName inputPresetName = new InputPresetName();
            index = uxPresetListBox.SelectedIndex;
            inputPresetName.ShowDialog();

            ssstring = inputPresetName.uxInputPresetName.Text;

            Presets.ElementAt(index).Name = inputPresetName.uxInputPresetName.Text;
            uxPresetListBox.Items.Refresh();



        }
    }
}
