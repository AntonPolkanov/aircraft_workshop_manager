using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Service
    {
        public Service()
        {
            Job = new HashSet<Job>();
        }

        public int ServiceId { get; set; }
        public int AircraftId { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ClientQuotesHrs { get; set; }

        public virtual Aircraft Aircraft { get; set; }
        public virtual ICollection<Job> Job { get; set; }
    }
}
