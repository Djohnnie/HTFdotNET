using System;
using System.Collections.Generic;
using HTF.Mars.StreamSource.Contracts;
using HTF.Mars.StreamSource.Services;

namespace HTF.Mars.StreamSource.Core
{
    public class CoreService : ICoreService
    {
        private readonly ITimerService _timerService;
        private readonly ISampleGenerationService _sampleGenerator;
        private readonly IOutputService _outputter;

        private readonly List<Sample> _previousSamples = new List<Sample>();
        private ITimerToken _timerToken;

        private String _destination;

        public event EventHandler<Sample> SampleReceived;

        public CoreService(ITimerService timerService, ISampleGenerationService sampleGenerator, IOutputService outputter)
        {
            _timerService = timerService;
            _sampleGenerator = sampleGenerator;
            _outputter = outputter;
        }

        public Boolean IsValid(String destination)
        {
            return _outputter.IsValid(destination);
        }

        public void Start(String destination)
        {
            _destination = destination;
            _timerToken = _timerService.Start(TimeSpan.FromMilliseconds(1000), Do);
        }

        public void Stop()
        {
            _timerToken?.Stop();
        }

        private void Do()
        {
            var sample = _sampleGenerator.GenerateSample(_previousSamples);
            _previousSamples.Add(sample);
            SampleReceived?.Invoke(this, sample);
            _outputter.WriteSample(_destination, sample);
        }
    }
}