using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Flight
    {
        public int FlightId { get; set; }
        public int AircraftId { get; set; }
        public DateTime? Date { get; set; }
        public string Hours { get; set; }

        public virtual Aircraft Aircraft { get; set; }
    }
}
