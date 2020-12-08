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

    public delegate void LoadMainWindow();
    public delegate void CloseMainWindow();
    public delegate void UpdatePresetList(BindingList<Preset> presets);

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
        public BindingList<Preset> Presets { get; set; }

        static Controller c = new Controller();
        
        //public delegate void CallBack(BindingList<Preset> bl);
        //public void GetPresetList(CallBack obj)
        //{

        //}
        public void Callback(BindingList<Preset> bl)
        {
            
            Presets = bl;
        }


        //public BindingList<Preset> Presets { get; set; }// = new BindingList<Preset> { new Preset("test1", 1, 3), new Preset("test2", 2, 4), new Preset("test3", 10, 6) };
        LoadMainWindow LoadMainWindow = c.LoadMainWindow;
        CloseMainWindow CloseMainWindow = c.CloseMainWindow;

        
        
        
        //BindingList<Preset> presets = new BindingList<Preset>();

        
        

        public string PlaceholderText { get; set; }

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
            Preset newPreset = new Preset();
            InputPresetName inputPresetName = new InputPresetName();
            inputPresetName.ShowDialog();
            if (inputPresetName.DialogResult == true)
            {
                c.AddPreset(inputPresetName.uxInputPresetName.Text, (int)uxDelaySlider.Value, (int)uxFrequencySlider.Value);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CloseMainWindow();
            c.CloseMainWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadMainWindow();
            //Presets = c.GetPresets();

        }


    }
}
