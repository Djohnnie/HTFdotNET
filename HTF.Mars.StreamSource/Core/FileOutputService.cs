using System;
using System.IO;
using System.Threading.Tasks;
using HTF.Mars.StreamSource.Contracts;
using Newtonsoft.Json;

namespace HTF.Mars.StreamSource.Core
{
    public class FileOutputService : IOutputService
    {
        public Boolean IsValid(String destination)
        {
            return true;
        }

        public async Task WriteSample(String path, Sample sample)
        {
            if (Directory.Exists(path))
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var serializedSample = SerializeSample(sample);
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, fileName)))
                {
                    await streamWriter.WriteAsync(serializedSample);
                }
            }
            else
            {
                throw new DirectoryNotFoundException("The specified path is not a valid directory");
            }
        }

        private String SerializeSample(Sample sample)
        {
            return JsonConvert.SerializeObject(sample, Formatting.Indented);
        }
    }
}