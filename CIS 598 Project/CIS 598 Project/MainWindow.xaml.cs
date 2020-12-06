using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
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
        //private SavingWaveProvider savingWaveProvider;
        private OffsetSampleProvider offsetSampleProvider;
        private WaveOut player;

        public string PlaceholderText { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
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
            uxPowerToggle.Content = "On";

            // set up the recorder
            recorder = new WaveIn();
            recorder.DataAvailable += RecorderOnDataAvailable;

            // set up our signal chain
            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            //savingWaveProvider = new SavingWaveProvider(bufferedWaveProvider, "temp.wav");

            // set up playback
            player = new WaveOut();
            player.Init(bufferedWaveProvider);

            // begin playback & record
            player.Play();
            recorder.StartRecording();
        }

        private void uxPowerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            uxPowerToggle.Content = "Off";

            // stop recording
            recorder.StopRecording();
            // stop playback
            player.Stop();
            // finalize the WAV file
            //savingWaveProvider.Dispose();
        }

        private void uxFrequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void uxDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void uxVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
