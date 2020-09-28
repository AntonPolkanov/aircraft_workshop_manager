using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Shift
    {
        public int ShiftId { get; set; }
        public string EmailAddressId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? DurationHours { get; set; }
        public byte? ClockStatus { get; set; }

        public virtual UserAccount EmailAddress { get; set; }
    }
}
