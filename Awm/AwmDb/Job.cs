using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Job
    {
        public Job()
        {
            Material = new HashSet<Material>();
            SparePart = new HashSet<SparePart>();
        }

        public int JobId { get; set; }
        public string EmailAddressId { get; set; }
        public string JobDescription { get; set; }
        public int? AllocatedHours { get; set; }
        public int? CumulativeHours { get; set; }
        public byte? Status { get; set; }

        public virtual UserAccount EmailAddress { get; set; }
        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<SparePart> SparePart { get; set; }
    }
}
