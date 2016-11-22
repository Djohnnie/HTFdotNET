using System;
using HTF.Mars.StreamSource.Services;

namespace HTF.Mars.StreamSource
{

    public class CyrillicString : ObservableBase
    {
        #region [ Private Members ]

        private readonly ITimerService _timerService;
        private readonly IRandomService _randomService;
        private string _value;

        #endregion

        #region [ Public Properties ]

        public String Cyrillic { get; set; }
        public String Latin { get; set; }

        public String Value
        {
            get { return _value; }
            set
            {
                _value = value;
                this.NotifyPropertyChanged(x => x.Value);
            }
        }

        #endregion

        #region [ Construction ]

        public CyrillicString(ITimerService timerService, IRandomService randomService)
        {
            _timerService = timerService;
            _randomService = randomService;
            var milliseconds = _randomService.RandomInt(500, 1000);
            _timerService.Start(TimeSpan.FromMilliseconds(milliseconds), RefreshString);
        }

        #endregion

        #region [ Helper Methods ]

        private void RefreshString()
        {
            Value = _randomService.RandomInt(0, 2) == 0 ? Cyrillic : Latin;
        }

        #endregion

    }

}