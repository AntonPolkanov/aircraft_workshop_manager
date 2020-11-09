using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Shift
    {
        public Shift()
        {
            Job = new HashSet<Job>();
            Timesheet = new HashSet<Timesheet>();
        }

        public int ShiftId { get; set; }
        public string EmailAddressId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? DurationHours { get; set; }

        public virtual UserAccount EmailAddress { get; set; }
        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<Timesheet> Timesheet { get; set; }
    }
}
