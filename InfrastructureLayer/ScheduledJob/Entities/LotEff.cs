using System;

namespace ScheduledJob.Entities
{
    public class LotEff
    {
        public string Lot { get; set; }

        public DateTime LohTimestamp { get; set; }

        public string ProcessGroup { get; set; }

        public int WaferCount { get; set; }

        public string BasicType { get; set; }

        /// <summary>
        /// Bau/Product
        /// </summary>
        public string Bau { get; set; }

        public string SBA { get; set; }

        public string QMP { get; set; }
    }
}
