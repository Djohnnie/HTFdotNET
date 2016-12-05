using System;

namespace HTF.Mars.StreamSource.Contracts
{
    public class Sample
    {
        /// <summary>
        /// Gets or sets the temperature in Kelvin.
        /// </summary>
        /// <value>The temperature in Kelvin.</value>
        /// <remarks>
        /// Temperature on Mars ranges from
        /// 140K to 310K and averages 215K.
        /// </remarks>
        public Decimal Temperature { get; set; }

        /// <summary>
        /// Gets or sets the wind speed in m/s.
        /// </summary>
        /// <value>The wind speed in m/s.</value>
        public Decimal WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the wind orientation.
        /// </summary>
        /// <value>The wind orientation.</value>
        public Wind WindOrientation { get; set; }

        /// <summary>
        /// Gets or sets the size of the dust particles in µm.
        /// </summary>
        /// <value>The size of the particle in µm.</value>
        /// <remarks>
        /// The atmospheric dust particle size
        /// on Mars ranges from 0,010µm to 10µm.
        /// </remarks>
        public Decimal ParticleSize { get; set; }

        public Boolean IsBogus { get; set; }
    }
}