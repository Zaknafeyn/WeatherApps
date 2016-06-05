using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using Prism.Commands;
using Prism.Mvvm;
using Services.DTO.WeatherInCity;
using Services.Interfaces;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private readonly IWeatherSevice _weatherSevice;
        private WaveFileWriter writer;
        private WaveIn waveIn;
        private WasapiLoopbackCapture wasapiLoopback;
        private string _title = "Hello World";
        private bool _isRecording = false;
        private CityWeatherStatus _weather;

        public ViewAViewModel(IWeatherSevice weatherSevice)
        {
            _weatherSevice = weatherSevice;

            LoadData();

            CaptureSoundCommand = new DelegateCommand(CaptureSoundCommandExecuted);
            SwitchDeviceCommand = new DelegateCommand(SwitchDeviceCommandExecuted);
            StopCapturingSoundCommand = new DelegateCommand(StopCapturingSoundCommandExecuted);

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

            var defaultRecordEndpoint = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
            var defaultPlaybackEndpoint = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
            {
                if (device.DataFlow == DataFlow.Capture)
                    ItemsRecord.Add(defaultRecordEndpoint.ID != device.ID ? $"{device.FriendlyName}" : $"{device.FriendlyName} - DEFAULT");
                else
                    ItemsPlayback.Add(defaultPlaybackEndpoint.ID != device.ID? $"{device.FriendlyName}" : $"{device.FriendlyName} - DEFAULT");
            }
        }

        private void SwitchDeviceCommandExecuted()
        {
            
        }

        public DelegateCommand CaptureSoundCommand { get; }
        public DelegateCommand StopCapturingSoundCommand { get; }
        public DelegateCommand SwitchDeviceCommand { get; }

        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<string> ItemsPlayback { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> ItemsRecord { get; } = new ObservableCollection<string>();

        public bool IsRecording
        {
            get { return _isRecording; }
            set { SetProperty( ref _isRecording, value); }
        }

        public CityWeatherStatus Weather
        {
            get { return _weather; }
            set { SetProperty( ref _weather, value); }
        }

        private async void LoadData()
        {
            Weather = await _weatherSevice.GetWeatherByCityNameAsync("London");
        }

        private void CaptureSoundCommandExecuted()
        {
            MMDevice device;
            // get devices
            var deviceEnumerator = new MMDeviceEnumerator();
            device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);

            //var sessions = device.AudioSessionManager.Sessions;

            //if (sessions == null)
            //    return;

            //var sessionsList = new List<AudioSessionControl>();
            //for (int i = 0; i < sessions.Count; i++)
            //{
            //    sessionsList.Add(sessions[i]);
            //}

            //----
            //wasapiLoopback = new WasapiLoopbackCapture(device);
            //wasapiLoopback.DataAvailable += DataAvailable;

            //writer = new WaveFileWriter(@"D:\dev\sandbox\text1.wav", wasapiLoopback.WaveFormat);

            //wasapiLoopback.StartRecording();

            // -----

            waveIn = new WaveIn();

            waveIn.DataAvailable += DataAvailable;

            writer = new WaveFileWriter(@"D:\dev\sandbox\text1.wav", waveIn.WaveFormat);

            waveIn.StartRecording();
            // -----

            IsRecording = true;
        }

        private void StopCapturingSoundCommandExecuted()
        {
            waveIn.StopRecording();
            waveIn.DataAvailable -= DataAvailable;
            waveIn.Dispose();

            //----
            //wasapiLoopback.StopRecording();
            //wasapiLoopback.DataAvailable -= DataAvailable;
            //wasapiLoopback.Dispose();

            IsRecording = false;

            writer.Dispose();
        }

        private async void DataAvailable(object sender, WaveInEventArgs e)
        {
            await writer.WriteAsync(e.Buffer, 0, e.BytesRecorded);
        }

    }
}
