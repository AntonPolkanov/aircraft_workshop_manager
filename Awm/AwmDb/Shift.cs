using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class Shift
    {
        public int ShiftId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? DurationHours { get; set; }
    }
}
