using System;

namespace HTF.Mars.StreamSource.Services
{
    public interface ITimerService : IDisposable
    {
        ITimerToken Start(TimeSpan interval, Action action);
    }
}