using System;
using System.Reactive.Linq;
using HTF.Mars.StreamSource.Contracts;
using HTF.Mars.StreamSource.Core;
using HTF.Mars.StreamSource.Services;
using Microsoft.Practices.Unity;

namespace HTF.Mars.StreamSource
{
    public class MainWindowViewModel : ObservableBase
    {
        #region -_ Private Events _-

        private event EventHandler PathOutputChanged;

        private void RaisePathOutputChanged()
        {
            PathOutputChanged?.Invoke(this, EventArgs.Empty);
        }

        private event EventHandler HttpOutputChanged;

        private void RaiseHttpOutputChanged()
        {
            HttpOutputChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

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
                    _httpOutput = String.Empty;
                    this.NotifyPropertyChanged(x => x.HttpOutput);
                }
                this.NotifyPropertyChanged(x => x.PathOutput);
                RaisePathOutputChanged();
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
                    _pathOutput = String.Empty;
                    this.NotifyPropertyChanged(x => x.PathOutput);
                }
                this.NotifyPropertyChanged(x => x.HttpOutput);
                RaiseHttpOutputChanged();
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

            _offline = CreateCyrillicString(diContainer, "OFFLINE", "ОФФЛИНЕ");
            _online = CreateCyrillicString(diContainer, "ONLINE", "ОНЛИНЕ");
            _title = CreateCyrillicString(diContainer, "Dusty Drones Sensor StreamService Configuration Dashboard", "Дусты Дронес Сенсор СтреамСервице Цонфигуратион Дашбоард");
            _fileTitle = CreateCyrillicString(diContainer, "Test Interface For File/Binary Based Listeners", "Тест Интерфаце Фор Филе/Бинары Басед Листенерс");
            _fileStatus = _offline;
            _webTitle = CreateCyrillicString(diContainer, "Test Interface For Web/HTTP Based Listeners", "Тест Интерфаце Фор Үеб/HТТП Басед Листенерс");
            _webStatus = _offline;
            _footer1 = CreateCyrillicString(diContainer, "DDSSCD v0.13.267.9782964387", "ДДССЦД в0.13.267.9782964387");
            _footer2 = CreateCyrillicString(diContainer, "DDSSCD is connected to the service backend", "ДДССЦД ис цоннецтед то тhе сервице бацкенд");
            _outputPath = CreateCyrillicString(diContainer, "Output Path", "Оутпут Патh");
            _outputHttp = CreateCyrillicString(diContainer, "Output HTTP", "Оутпут HТТП");
            _fileSamplesReceived = CreateCyrillicString(diContainer, "No samples received", "Но самплес рецеивед");
            _webSamplesReceived = CreateCyrillicString(diContainer, "No samples received", "Но самплес рецеивед");

            _timerService = diContainer.Resolve<ITimerService>();
            _timerService.Start(TimeSpan.FromMilliseconds(50), RefreshBullShit);

            _coreFileService = diContainer.Resolve<ICoreService>(new DependencyOverride<IOutputService>(new FileOutputService()));
            _coreFileService.SampleReceived += coreFileService_SampleReceived;
            _coreHttpService = diContainer.Resolve<ICoreService>(new DependencyOverride<IOutputService>(new HttpOutputService()));
            _coreHttpService.SampleReceived += coreHttpService_SampleReceived;

            Observable.FromEventPattern(x => this.PathOutputChanged += x, x => this.PathOutputChanged -= x)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .ObserveOnDispatcher()
                .Do(x => RefreshOutput())
                .Subscribe();
            Observable.FromEventPattern(x => this.HttpOutputChanged += x, x => this.HttpOutputChanged -= x)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .ObserveOnDispatcher()
                .Do(x => RefreshOutput())
                .Subscribe();
        }

        private void coreFileService_SampleReceived(object sender, Sample e)
        {
            var fileSamplesGeneratedLatin = _coreFileService.SamplesGenerated == 0 ? "No" : $"{_coreFileService.SamplesGenerated}";
            var fileSamplesGeneratedCyrillic = _coreFileService.SamplesGenerated == 0 ? "Но" : $"{_coreFileService.SamplesGenerated}";
            FileSamplesReceived.Latin = $"{fileSamplesGeneratedLatin} samples received";
            FileSamplesReceived.Cyrillic = $"{fileSamplesGeneratedCyrillic} самплес рецеивед";
        }

        private void coreHttpService_SampleReceived(object sender, Sample e)
        {
            var webSamplesGeneratedLatin = _coreHttpService.SamplesGenerated == 0 ? "No" : $"{_coreHttpService.SamplesGenerated}";
            var webSamplesGeneratedCyrillic = _coreHttpService.SamplesGenerated == 0 ? "Но" : $"{_coreHttpService.SamplesGenerated}";
            WebSamplesReceived.Latin = $"{webSamplesGeneratedLatin} samples received";
            WebSamplesReceived.Cyrillic = $"{webSamplesGeneratedCyrillic} самплес рецеивед";
        }

        #endregion

        #region [ Helper Methods ]

        private CyrillicString CreateCyrillicString(UnityContainer diContainer, String latin, String cyrillic)
        {
            var cyrillicString = diContainer.Resolve<CyrillicString>();
            cyrillicString.Latin = latin;
            cyrillicString.Cyrillic = cyrillic;
            return cyrillicString;
        }

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

        private async void RefreshOutput()
        {
            _coreFileService.Stop();
            FileStatus = _offline;
            _coreHttpService.Stop();
            WebStatus = _offline;
            if (!String.IsNullOrWhiteSpace(PathOutput) && String.IsNullOrWhiteSpace(HttpOutput))
            {
                if (await _coreFileService.IsValid(PathOutput))
                {
                    FileStatus = _online;
                    _coreFileService.Start(PathOutput);
                }
            }
            if (!String.IsNullOrWhiteSpace(HttpOutput) && String.IsNullOrWhiteSpace(PathOutput))
            {
                if (await _coreHttpService.IsValid(HttpOutput))
                {
                    WebStatus = _online;
                    _coreHttpService.Start(HttpOutput);
                }
            }
        }

        #endregion
    }
}