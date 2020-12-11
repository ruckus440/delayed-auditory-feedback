/*
 * InputPresetName.xaml.cs
 * Author: Mike Ruckert
 * CIS 598 Senior Project - Kansas State University
 * 
 * View form for entering the name of a preset for the DAF/AAF app.
 */

using System.Windows;

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

        /// <summary>
        /// Event handler for clicking the uxOkPreset button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOkPreset_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }
    }
}
