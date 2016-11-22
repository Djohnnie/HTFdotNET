using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTF.Mars.StreamSource.Services
{
    public interface IRandomService
    {
        Int32 RandomInt(Int32 min, Int32 max);
        Decimal RandomDecimal(Int32 min, Int32 max);
    }
}