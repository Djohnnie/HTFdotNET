using System;
using System.Threading.Tasks;
using HTF.Mars.StreamSource.Contracts;

namespace HTF.Mars.StreamSource.Core
{
    public interface IOutputService
    {
        Boolean IsValid(String destination);
        Task WriteSample(String destination, Sample sample);
    }
}