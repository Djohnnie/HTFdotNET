using System;

namespace HTF.Mars.StreamSource.Services
{
    public interface IRandomService
    {
        Int32 RandomInt(Int32 min, Int32 max);
        Decimal RandomDecimal(Int32 min, Int32 max);
    }
}