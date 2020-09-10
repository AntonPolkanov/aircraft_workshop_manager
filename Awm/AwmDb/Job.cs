using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class Job
    {
        public int JobId { get; set; }
        public string Description { get; set; }
        public int? TimeTakenhrs { get; set; }
        public int? ActualTimeTakenHrs { get; set; }
    }
}
