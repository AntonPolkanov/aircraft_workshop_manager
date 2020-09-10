using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class ServiceTimer
    {
        public int ServiceTimerId { get; set; }
        public string NextServiceDate { get; set; }
        public byte? Status { get; set; }
    }
}
