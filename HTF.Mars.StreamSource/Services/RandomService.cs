using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTF.Mars.StreamSource.Services
{
    public class RandomService : IRandomService
    {
        private readonly Random _randomGenerator = new Random();

        public Int32 RandomInt(Int32 min, Int32 max)
        {
            return _randomGenerator.Next(min, max + 1);
        }

        public Decimal RandomDecimal(Int32 min, Int32 max)
        {
            return _randomGenerator.Next(min, max + 1);
        }
    }
}