using System.Collections.Generic;
using HTF.Mars.StreamSource.Contracts;

namespace HTF.Mars.StreamSource.Core
{
    public interface ISampleGenerationService
    {
        Sample GenerateSample();
        Sample GenerateSample(IEnumerable<Sample> previousSamples);
    }
}