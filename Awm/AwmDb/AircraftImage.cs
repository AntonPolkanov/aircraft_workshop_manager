using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class AircraftImage
    {
        public int ImageId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Comment { get; set; }
        public string S3Path { get; set; }
    }
}
