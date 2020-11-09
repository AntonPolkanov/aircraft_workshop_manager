using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Timesheet
    {
        public int TimesheetId { get; set; }
        public int? AircraftId { get; set; }
        public int? ShiftId { get; set; }
        public int? JobId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string ActualHours { get; set; }
        public string EmailAddressId { get; set; }

        public virtual Aircraft Aircraft { get; set; }
        public virtual UserAccount EmailAddress { get; set; }
        public virtual Job Job { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
