using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class Service
    {
        public int Idservice { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ClientQuotesHrs { get; set; }
    }
}
