using System;

namespace HTF.Mars.StreamSource.Contracts
{
    public class Wind
    {
        public Int16 Value { get; set; }
        public Orientation Orientation { get; set; }
    }

    public enum Orientation
    {
        N = 0,
        NNO = 225,
        NO = 450,
        ONO = 675,
        O = 900,
        OZO = 1125,
        ZO = 1350,
        ZZO = 1575,
        Z = 1800,
        ZZW = 2025,
        ZW = 2250,
        WZW = 2475,
        W = 2700,
        WNW = 2925,
        NW = 3150,
        NNW = 3375
    }
}