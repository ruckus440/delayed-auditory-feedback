/*
 * MainWindow.xaml.cs
 * Author: Mike Ruckert
 * CIS 598 Senior Project - Kansas State University
 * 
 * Main view form for the DAF/AAF app.
 */

using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CIS_598_Project
{

    public delegate void LoadMainWindow();
    public delegate void CloseMainWindow();
    public delegate void UpdatePresetList(BindingList<Preset> presets);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Controller c = new Controller();
        private WaveIn recorder;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOut player;
        private int delayLength;
        private double pitchShift = 1.0;
        private bool running = false;
        public BindingList<Preset> Presets { get; set; }
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            recorder = new WaveIn();
            c.ReadSavedPresets();
            c.GetPresets(Callback);
            DataContext = this;
        }


        /// <summary>
        /// Callback method for delegate from Controller to send the list of Presets.
        /// </summary>
        /// <param name="bl">The list of Presets</param>
        public void Callback(BindingList<Preset> bl)
        {
            Presets = bl;
        }


        /// <summary>
        /// Event handler to add audio samples to the BufferedWaveProvider buffer when samples are available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="waveInEventArgs"></param>
        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }

        /// <summary>
        /// Event handler that triggers when the uxPresetCombo selection is changed. Catches the exception in the event a Preset is deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StopPlayback();
                Preset p = new Preset();
                p = (Preset)uxPresetComboBox.SelectedItem;
                uxDelaySlider.Value = p.Delay;
                uxFrequencySlider.Value = p.Frequency;
                if (uxPresetComboBox.SelectedIndex != -1)
                {
                    uxSaveCurrentSettingsBtn.Content = "Update Current Settings";
                }
                else
                {
                    uxSaveCurrentSettingsBtn.Content = "Save Current Settings";
                }
            }
            catch (NullReferenceException)
            {
                uxPresetComboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Event handler for when the uxPowerToggle is checked.
        /// </summary>
        /// <param name="sender">uxPowerToggle</param>
        /// <param name="e"></param>
        private void uxPowerToggle_Checked(object sender, RoutedEventArgs e)
        {
            running = true;
            uxPowerToggle.Content = "On";

            recorder = new WaveIn();
            recorder.DataAvailable += RecorderOnDataAvailable;
            recorder.WaveFormat = new WaveFormat(88200, 1);

            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);

            SmbPitchShiftingSampleProvider smbPitchShiftingProvider = new SmbPitchShiftingSampleProvider(bufferedWaveProvider.ToSampleProvider())
            {
                PitchFactor = (float)pitchShift
            };

            player = new WaveOut();
            player.Init(smbPitchShiftingProvider);

            StartPlayback();
        }

        /// <summary>
        /// Event handler for when the uxPowerToggle is unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPowerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            StopPlayback();
        }

        /// <summary>
        /// Event handler for when the value of uxFrequencySlider is changed.
        /// </summary>
        /// <param name="sender">uxFrequencySlider</param>
        /// <param name="e"></param>
        private void uxFrequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StopPlayback();
            pitchShift = 1.0 + (uxFrequencySlider.Value / 10);
        }

        /// <summary>
        /// Event handler for when the value of uxDelaySlider is changed.
        /// </summary>
        /// <param name="sender">uxDelaySlider</param>
        /// <param name="e"></param>
        private void uxDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StopPlayback();
            delayLength = (int)uxDelaySlider.Value * 100;
        }

        /// <summary>
        /// Starts audio recording and playback. Uses the Delay function to delay the playback from the start of recording depending on delayLength.
        /// </summary>
        private async void StartPlayback()
        {
            recorder.StartRecording();
            await Task.Delay(delayLength);
            player.Play();
        }

        /// <summary>
        /// If running, stops the playback, resets the uxPowerToggle, and dispose of the recorder.
        /// </summary>
        private void StopPlayback()
        {
            if (running)
            {
                running = false;
                uxPowerToggle.Content = "Off";
                uxPowerToggle.IsChecked = false;
                recorder.StopRecording();
                player.Stop();
                recorder.Dispose();
            }
        }

        /// <summary>
        /// Event handler for clicking uxManagePresetsBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxManagePresetsBtn_Click(object sender, RoutedEventArgs e)
        {
            uxSaveCurrentSettingsBtn.Content = "Save Current Settings";
            StopPlayback();
            uxPresetComboBox.SelectedIndex = -1;
            PresetManager presetManager = new PresetManager();
            presetManager.Show();
        }

        /// <summary>
        /// Event handler for clicking uxSaveCurrentSettingsBtn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSaveCurrentSettingsBtn_Click(object sender, RoutedEventArgs e)
        {

            if (uxPresetComboBox.SelectedIndex != -1)
            {
                c.UpdatePresetSettings(uxPresetComboBox.SelectedIndex, (int)uxDelaySlider.Value, (int)uxFrequencySlider.Value);
            }
            else
            {
                Preset newPreset = new Preset();
                InputPresetName inputPresetName = new InputPresetName();
                inputPresetName.ShowDialog();
                if (inputPresetName.DialogResult == true)
                {
                    c.AddPreset(inputPresetName.uxInputPresetName.Text, (int)uxDelaySlider.Value, (int)uxFrequencySlider.Value);
                }
                uxPresetComboBox.SelectedIndex = Presets.Count - 1;
            }
        }

        /// <summary>
        /// Event handler for closing MainWindow. Calls the Controllers SerializeSavedPresets method to save the list of Presets to file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c.SerializeSavedPresets();
        }

        /// <summary>
        /// Event handler for opening the PresetComboBox drop down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPresetComboBox_DropDownOpened(object sender, EventArgs e)
        {
            uxPresetComboBox.Items.Refresh();
        }

        /// <summary>
        /// Event handler for loading the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}