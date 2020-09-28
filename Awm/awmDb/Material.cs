using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Material
    {
        public int MaterialId { get; set; }
        public int JobId { get; set; }
        public DateTime? IntakeDate { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public string Gnr { get; set; }

        public virtual Job Job { get; set; }
    }
}
