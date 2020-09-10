using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class Aircraft
    {
        public int AircraftId { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Engine { get; set; }
        public DateTime? LastServiceDate { get; set; }
    }
}
