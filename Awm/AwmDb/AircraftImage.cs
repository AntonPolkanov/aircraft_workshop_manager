using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class AircraftImage
    {
        public int ImageId { get; set; }
        public int AircraftId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Comment { get; set; }
        public string S3Path { get; set; }

        public virtual Aircraft Aircraft { get; set; }
    }
}
