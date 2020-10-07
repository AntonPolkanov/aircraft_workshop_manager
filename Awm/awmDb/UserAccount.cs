using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Shift = new HashSet<Shift>();
            Timesheet = new HashSet<Timesheet>();
        }

        public string EmailAddressId { get; set; }
        public int? AircraftId { get; set; }
        public int? ClientId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }

        public virtual Aircraft Aircraft { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Shift> Shift { get; set; }
        public virtual ICollection<Timesheet> Timesheet { get; set; }
    }
}
