using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

namespace CIS_598_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveIn recorder;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOut player;
        private int delayLength;
        private bool running = false;
        Controller c = new Controller();
        

        public string PlaceholderText { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            recorder = new WaveIn();
            
        }

        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }

        private void uxPresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void uxPowerToggle_Checked(object sender, RoutedEventArgs e)
        {
            running = true;
            uxPowerToggle.Content = "On";

            // set up the recorder
            recorder = new WaveIn();
            recorder.DataAvailable += RecorderOnDataAvailable;

            // set up our signal chain
            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            
            // set up playback
            player = new WaveOut();
            player.Init(bufferedWaveProvider);

            // begin playback & record
            StartPlayback();
        }

        private void uxPowerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            StopPlayback();
        }

        private void uxFrequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (running)
            {
                StopPlayback();
            }

        }

        private void uxDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (running)
            {
                StopPlayback();
            }
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
            running = false;
            uxPowerToggle.Content = "Off";
            uxPowerToggle.IsChecked = false;

            // stop recording
            recorder.StopRecording();
            // stop playback
            player.Stop();
            // dispose the recorder
            recorder.Dispose();
        }

        private void uxManagePresetsBtn_Click(object sender, RoutedEventArgs e)
        {
            PresetManager presetManager = new PresetManager();
            presetManager.Show();
        }

        private void uxSaveCurrentSettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c.CloseMainWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
