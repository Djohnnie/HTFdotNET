using System;
using System.Threading.Tasks;
using HTF.Mars.StreamSource.Contracts;
using Newtonsoft.Json;

namespace HTF.Mars.StreamSource.Core
{
    public class HttpOutputService : IOutputService
    {
        public Boolean IsValid(String destination)
        {
            return true;
        }

        public async Task WriteSample(String path, Sample sample)
        {
            await Task.Delay(1000);
        }

        private String SerializeSample(Sample sample)
        {
            return JsonConvert.SerializeObject(sample, Formatting.Indented);
        }
    }
}