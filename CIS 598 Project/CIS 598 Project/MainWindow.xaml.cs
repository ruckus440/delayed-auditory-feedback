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

        public void Callback(BindingList<Preset> bl)
        {
            Presets = bl;
        }

        public MainWindow()
        {
            InitializeComponent();
            recorder = new WaveIn();
            c.ReadSavedPresets();
            c.GetPresets(Callback);
            DataContext = this;
        }

        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }

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

        private void uxPowerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            StopPlayback();
        }

        private void uxFrequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StopPlayback();
            pitchShift = 1.0 + (uxFrequencySlider.Value / 10);
        }

        private void uxDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StopPlayback();
            delayLength = (int)uxDelaySlider.Value * 100;
        }

        private async void StartPlayback()
        {
            recorder.StartRecording();
            await Task.Delay(delayLength);
            player.Play();
        }

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

        private void uxManagePresetsBtn_Click(object sender, RoutedEventArgs e)
        {
            uxSaveCurrentSettingsBtn.Content = "Save Current Settings";
            StopPlayback();
            uxPresetComboBox.SelectedIndex = -1;
            PresetManager presetManager = new PresetManager();
            presetManager.Show();
        }

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c.SerializeSavedPresets();
        }

        private void uxPresetComboBox_DropDownOpened(object sender, EventArgs e)
        {
            uxPresetComboBox.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}