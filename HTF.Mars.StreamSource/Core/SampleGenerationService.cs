using System.Collections.Generic;
using HTF.Mars.StreamSource.Contracts;
using HTF.Mars.StreamSource.Services;

namespace HTF.Mars.StreamSource.Core
{
    public class SampleGenerationService : ISampleGenerationService
    {
        private readonly IRandomService _randomService;

        public SampleGenerationService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public Sample GenerateSample()
        {
            return new Sample();
        }

        public Sample GenerateSample(IEnumerable<Sample> previousSamples)
        {
            return new Sample();
        }
    }
}