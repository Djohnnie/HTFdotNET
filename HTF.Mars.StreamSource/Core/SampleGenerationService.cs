using System;
using System.Collections.Generic;
using System.Linq;
using HTF.Mars.StreamSource.Contracts;
using HTF.Mars.StreamSource.Services;

namespace HTF.Mars.StreamSource.Core
{
    public class SampleGenerationService : ISampleGenerationService
    {
        private readonly IRandomService _randomService;
        private DateTime? timeBasedRandomEvent;
        private Boolean upOrDown;

        public SampleGenerationService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public Sample GenerateSample()
        {
            return new Sample
            {
                Temperature = _randomService.RandomDecimal(140, 310),
                WindOrientation = new Wind(0),
                WindSpeed = _randomService.RandomDecimal(0, 50),
                ParticleSize = _randomService.RandomDecimal(1, 1000),
                TimeStamp = DateTime.UtcNow
            };
        }

        public Sample GenerateSample(IEnumerable<Sample> previousSamples)
        {
            var shouldGenerateBogusSample = _randomService.RandomInt(1, 10) == 1;
            var bogusParameterToGenerate = _randomService.RandomInt(1, 4);

            var temperature = shouldGenerateBogusSample && bogusParameterToGenerate == 1 ? BogusTemperature() : RandomTemperature(previousSamples);
            var wind = shouldGenerateBogusSample && bogusParameterToGenerate == 2 ? BogusWind() : RandomWind(previousSamples);
            var windSpeed = shouldGenerateBogusSample && bogusParameterToGenerate == 3 ? BogusWindSpeed() : RandomWindSpeed(previousSamples);
            var particleSize = shouldGenerateBogusSample && bogusParameterToGenerate == 4 ? BogusParticleSize() : RandomParticleSize(previousSamples);

            return new Sample
            {
                Temperature = temperature,
                WindOrientation = new Wind(wind),
                WindSpeed = windSpeed,
                ParticleSize = particleSize,
                IsBogus = shouldGenerateBogusSample,
                TimeStamp = DateTime.UtcNow
            };
        }

        private Decimal BogusTemperature()
        {
            return _randomService.RandomDecimal(140, 310);
        }

        private Decimal RandomTemperature(IEnumerable<Sample> previousSamples)
        {
            CheckTimeBasedRandomEvent();
            var previousTemperature = previousSamples.LastOrDefault(x => !x.IsBogus)?.Temperature;
            var minimum = Math.Max(140, (Int32)(previousTemperature - (upOrDown ? 2 : 4) ?? 215));
            var maximum = Math.Min(310, (Int32)(previousTemperature + (upOrDown ? 4 : 2) ?? 215));
            return _randomService.RandomDecimal(minimum, maximum);
        }

        private Int32 BogusWind()
        {
            return _randomService.RandomInt(0, 15) * 225;
        }

        private Int32 RandomWind(IEnumerable<Sample> previousSamples)
        {
            CheckTimeBasedRandomEvent();
            var previousWind = previousSamples.LastOrDefault(x => !x.IsBogus)?.WindOrientation?.Value;
            var minimum = Math.Max(0, (Int32)(previousWind / 225 - (upOrDown ? 0 : 1) ?? 0));
            var maximum = Math.Min(15, (Int32)(previousWind / 225 + (upOrDown ? 1 : 0) ?? 15));
            return _randomService.RandomInt(minimum, maximum) * 225;
        }

        private Decimal BogusWindSpeed()
        {
            return _randomService.RandomDecimal(0, 50);
        }

        private Decimal RandomWindSpeed(IEnumerable<Sample> previousSamples)
        {
            CheckTimeBasedRandomEvent();
            var previousWindSpeed = previousSamples.LastOrDefault(x => !x.IsBogus)?.WindSpeed;
            var minimum = Math.Max(0, (Int32)(previousWindSpeed - (upOrDown ? 1 : 3) ?? 5));
            var maximum = Math.Min(50, (Int32)(previousWindSpeed + (upOrDown ? 3 : 1) ?? 5));
            return _randomService.RandomDecimal(minimum, maximum);
        }

        private Decimal BogusParticleSize()
        {
            return _randomService.RandomDecimal(1, 1000);
        }

        private Decimal RandomParticleSize(IEnumerable<Sample> previousSamples)
        {
            CheckTimeBasedRandomEvent();
            var previousParticleSize = previousSamples.LastOrDefault(x => !x.IsBogus)?.ParticleSize;
            var minimum = Math.Max(1, (Int32)(previousParticleSize - (upOrDown ? 20 : 50) ?? 10));
            var maximum = Math.Min(1000, (Int32)(previousParticleSize + (upOrDown ? 50 : 20) ?? 10));
            return _randomService.RandomDecimal(minimum, maximum);
        }

        private void CheckTimeBasedRandomEvent()
        {
            if (timeBasedRandomEvent == null || DateTime.Now > timeBasedRandomEvent)
            {
                timeBasedRandomEvent = DateTime.Now.AddSeconds(_randomService.RandomInt(0, 59));
                upOrDown = !upOrDown;
            }
        }
    }
}