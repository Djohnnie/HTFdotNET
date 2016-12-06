using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HTF.Mars.StreamSoure.Web.Api.Models
{
    public class Wind
    {
        public Wind(Int32 value)
        {
            if (!Enum.IsDefined(typeof(Orientation), value)) throw new ArgumentException("value", nameof(value));
            Value = value;
            Orientation = (Orientation)value;
        }

        public Int32 Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
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