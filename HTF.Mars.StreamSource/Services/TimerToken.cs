﻿using System;
using System.Windows.Threading;

namespace HTF.Mars.StreamSource.Services
{
    public class TimerToken : ITimerToken
    {
        private readonly DispatcherTimer _timer;

        public TimerToken(TimeSpan interval, Action action)
        {
            _timer = new DispatcherTimer { Interval = interval };
            _timer.Tick += (sender, e) => { action(); };
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}