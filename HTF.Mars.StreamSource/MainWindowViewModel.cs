using System;
using HTF.Mars.StreamSource.Contracts;
using HTF.Mars.StreamSource.Core;
using HTF.Mars.StreamSource.Services;
using Microsoft.Practices.Unity;

namespace HTF.Mars.StreamSource
{
    public class MainWindowViewModel : ObservableBase
    {
        #region [ Private Members ]

        private String[] _bsLeft;
        private String[] _bsRight;
        private CyrillicString _online;
        private CyrillicString _offline;
        private CyrillicString _title;
        private CyrillicString _fileTitle;
        private CyrillicString _fileStatus;
        private CyrillicString _webTitle;
        private CyrillicString _webStatus;
        private CyrillicString _footer1;
        private CyrillicString _footer2;
        private CyrillicString _outputPath;
        private String _pathOutput;
        private CyrillicString _outputHttp;
        private String _httpOutput;
        private CyrillicString _fileSamplesReceived;
        private CyrillicString _webSamplesReceived;

        private readonly ICoreService _coreFileService;
        private readonly ICoreService _coreHttpService;
        private readonly ITimerService _timerService;
        private readonly Random _randomGenerator = new Random();

        #endregion

        #region [ Public Properties ]

        public String[] BSLeft
        {
            get { return _bsLeft; }
            set
            {
                _bsLeft = value;
                this.NotifyPropertyChanged(x => x.BSLeft);
            }
        }

        public String[] BSRight
        {
            get { return _bsRight; }
            set
            {
                _bsRight = value;
                this.NotifyPropertyChanged(x => x.BSRight);
            }
        }

        public CyrillicString Title
        {
            get { return _title; }
            set
            {
                _title = value;
                this.NotifyPropertyChanged(x => x.Title);
            }
        }

        public CyrillicString FileTitle
        {
            get { return _fileTitle; }
            set
            {
                _fileTitle = value;
                this.NotifyPropertyChanged(x => x.FileTitle);
            }
        }

        public CyrillicString FileStatus
        {
            get { return _fileStatus; }
            set
            {
                _fileStatus = value;
                this.NotifyPropertyChanged(x => x.FileStatus);
            }
        }

        public CyrillicString WebTitle
        {
            get { return _webTitle; }
            set
            {
                _webTitle = value;
                this.NotifyPropertyChanged(x => x.WebTitle);
            }
        }

        public CyrillicString WebStatus
        {
            get { return _webStatus; }
            set
            {
                _webStatus = value;
                this.NotifyPropertyChanged(x => x.WebStatus);
            }
        }

        public CyrillicString Footer1
        {
            get { return _footer1; }
            set
            {
                _footer1 = value;
                this.NotifyPropertyChanged(x => x.Footer1);
            }
        }

        public CyrillicString Footer2
        {
            get { return _footer2; }
            set
            {
                _footer2 = value;
                this.NotifyPropertyChanged(x => x.Footer2);
            }
        }

        public CyrillicString OutputPath
        {
            get { return _outputPath; }
            set
            {
                _outputPath = value;
                this.NotifyPropertyChanged(x => x.OutputPath);
            }
        }

        public String PathOutput
        {
            get { return _pathOutput; }
            set
            {
                _pathOutput = value;
                if (!String.IsNullOrEmpty(value))
                {
                    HttpOutput = String.Empty;
                }
                this.NotifyPropertyChanged(x => x.PathOutput);
                RefreshOutput();
            }
        }

        public CyrillicString OutputHttp
        {
            get { return _outputHttp; }
            set
            {
                _outputHttp = value;
                this.NotifyPropertyChanged(x => x.OutputHttp);
            }
        }

        public String HttpOutput
        {
            get { return _httpOutput; }
            set
            {
                _httpOutput = value;
                if (!String.IsNullOrEmpty(value))
                {
                    PathOutput = String.Empty;
                }
                this.NotifyPropertyChanged(x => x.HttpOutput);
                RefreshOutput();
            }
        }

        public CyrillicString FileSamplesReceived
        {
            get { return _fileSamplesReceived; }
            set
            {
                _fileSamplesReceived = value;
                this.NotifyPropertyChanged(x => x.FileSamplesReceived);
            }
        }

        public CyrillicString WebSamplesReceived
        {
            get { return _webSamplesReceived; }
            set
            {
                _webSamplesReceived = value;
                this.NotifyPropertyChanged(x => x.WebSamplesReceived);
            }
        }

        #endregion

        #region [ Construction ]

        public MainWindowViewModel()
        {
            var diContainer = new UnityContainer();
            diContainer.RegisterType<ITimerService, TimerService>(new ContainerControlledLifetimeManager());
            diContainer.RegisterType<IRandomService, RandomService>(new ContainerControlledLifetimeManager());
            diContainer.RegisterType<ICoreService, CoreService>();
            diContainer.RegisterType<ISampleGenerationService, SampleGenerationService>();
            diContainer.RegisterType<IOutputService, FileOutputService>();
            //diContainer.RegisterType<IOutputService, HttpOutputService>("Http");
            _offline = diContainer.Resolve<CyrillicString>();
            _offline.Latin = "OFFLINE";
            _offline.Cyrillic = "ОФФЛИНЕ";
            _online = diContainer.Resolve<CyrillicString>();
            _online.Latin = "ONLINE";
            _online.Cyrillic = "ОНЛИНЕ";
            _title = diContainer.Resolve<CyrillicString>();
            _title.Latin = "Dusty Drones Sensor StreamService Configuration Dashboard";
            _title.Cyrillic = "Дусты Дронес Сенсор СтреамСервице Цонфигуратион Дашбоард";
            _fileTitle = diContainer.Resolve<CyrillicString>();
            _fileTitle.Latin = "Test Interface For File/Binary Based Listeners";
            _fileTitle.Cyrillic = "Тест Интерфаце Фор Филе/Бинары Басед Листенерс";
            _fileStatus = _offline;
            _webTitle = diContainer.Resolve<CyrillicString>();
            _webTitle.Latin = "Test Interface For Web/HTTP Based Listeners";
            _webTitle.Cyrillic = "Тест Интерфаце Фор Үеб/HТТП Басед Листенерс";
            _webStatus = _offline;
            _footer1 = diContainer.Resolve<CyrillicString>();
            _footer1.Latin = "DDSSCD v0.13.267.9782964387";
            _footer1.Cyrillic = "ДДССЦД в0.13.267.9782964387";
            _footer2 = diContainer.Resolve<CyrillicString>();
            _footer2.Latin = "DDSSCD is connected to the service backend";
            _footer2.Cyrillic = "ДДССЦД ис цоннецтед то тhе сервице бацкенд";
            _outputPath = diContainer.Resolve<CyrillicString>();
            _outputPath.Latin = "Output Path";
            _outputPath.Cyrillic = "Оутпут Патh";
            _outputHttp = diContainer.Resolve<CyrillicString>();
            _outputHttp.Latin = "Output HTTP";
            _outputHttp.Cyrillic = "Оутпут HТТП";
            _fileSamplesReceived = diContainer.Resolve<CyrillicString>();
            _fileSamplesReceived.Latin = "No samples received";
            _fileSamplesReceived.Cyrillic = "No samples received";
            _webSamplesReceived = diContainer.Resolve<CyrillicString>();
            _webSamplesReceived.Latin = "No samples received";
            _webSamplesReceived.Cyrillic = "No samples received";
            _timerService = diContainer.Resolve<ITimerService>();
            _timerService.Start(TimeSpan.FromMilliseconds(50), RefreshBullShit);
            _coreFileService = diContainer.Resolve<ICoreService>();
            _coreFileService.SampleReceived += coreFileService_SampleReceived;
            //_coreHttpService = diContainer.Resolve<ICoreService>();
        }

        private void coreFileService_SampleReceived(object sender, Sample e)
        {
            FileSamplesReceived.Latin = $"{_coreFileService.SamplesGenerated} samples received";
            FileSamplesReceived.Cyrillic = $"{_coreFileService.SamplesGenerated} samples received";
        }

        #endregion

        #region [ Helper Methods ]

        private void RefreshBullShit()
        {
            var bsLeft = new String[50];
            var bsRight = new String[50];
            for (var i = 0; i < 50; i++)
            {
                bsLeft[i] = $"{_randomGenerator.Next(0, 100):D2}";
                bsRight[i] = $"{_randomGenerator.Next(0, 100):D2}";
            }
            BSLeft = bsLeft;
            BSRight = bsRight;
        }

        private void RefreshOutput()
        {
            _coreFileService.Stop();
            FileStatus = _offline;
            //_coreHttpService.Stop();
            WebStatus = _offline;
            if (!String.IsNullOrWhiteSpace(PathOutput) && String.IsNullOrWhiteSpace(HttpOutput))
            {
                if (_coreFileService.IsValid(PathOutput))
                {
                    FileStatus = _online;
                    _coreFileService.Start(PathOutput);
                }
            }
            if (!String.IsNullOrWhiteSpace(HttpOutput) && String.IsNullOrWhiteSpace(PathOutput))
            {
                if (_coreFileService.IsValid(HttpOutput))
                {
                    WebStatus = _online;
                    _coreHttpService.Start(HttpOutput);
                }
            }
        }

        #endregion
    }
}