﻿using System;
using HTF.Mars.StreamSource.Contracts;

namespace HTF.Mars.StreamSource.Core
{
    public interface ICoreService
    {
        event EventHandler<Sample> SampleReceived;
        Boolean IsValid(String destination);
        void Start(String destination);
        void Stop();
    }
}