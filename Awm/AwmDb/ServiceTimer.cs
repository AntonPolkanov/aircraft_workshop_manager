using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class ServiceTimer
    {
        public int ServiceTimerId { get; set; }
        public int AircraftId { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public byte? Status { get; set; }

        public virtual Aircraft Aircraft { get; set; }
    }
}
