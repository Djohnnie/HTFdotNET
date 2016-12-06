using System;
using System.Threading.Tasks;
using HTF.Mars.StreamSource.Contracts;

namespace HTF.Mars.StreamSource.Core
{
    public interface ICoreService
    {
        event EventHandler<Sample> SampleReceived;
        Int32 SamplesGenerated { get; set; }
        Task<Boolean> IsValid(String destination);
        void Start(String destination);
        void Stop();
    }
}