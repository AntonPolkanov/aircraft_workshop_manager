using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Aircraft
    {
        public Aircraft()
        {
            AircraftImage = new HashSet<AircraftImage>();
            ClientNotes = new HashSet<ClientNotes>();
            Flight = new HashSet<Flight>();
            Service = new HashSet<Service>();
            ServiceTimer = new HashSet<ServiceTimer>();
            Timesheet = new HashSet<Timesheet>();
            UserAccount = new HashSet<UserAccount>();
        }

        public int AircraftId { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Engine { get; set; }
        public DateTime? LastServiceDate { get; set; }

        public virtual ICollection<AircraftImage> AircraftImage { get; set; }
        public virtual ICollection<ClientNotes> ClientNotes { get; set; }
        public virtual ICollection<Flight> Flight { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<ServiceTimer> ServiceTimer { get; set; }
        public virtual ICollection<Timesheet> Timesheet { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
