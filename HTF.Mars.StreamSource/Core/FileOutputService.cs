using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using HTF.Mars.StreamSource.Contracts;
using Newtonsoft.Json;

namespace HTF.Mars.StreamSource.Core
{
    public class FileOutputService : IOutputService
    {
        public Boolean IsValid(String path)
        {
            return Directory.Exists(path);
        }

        public async Task WriteSample(String path, Sample sample)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}.json";
                    var serializedSample = SerializeSample(sample);
                    using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, fileName)))
                    {
                        await streamWriter.WriteAsync(serializedSample);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
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