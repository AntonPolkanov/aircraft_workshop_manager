using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class ClientNotes
    {
        public int ClientNotesId { get; set; }
        public int AircraftId { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }

        public virtual Aircraft Aircraft { get; set; }
        public virtual Client Client { get; set; }
    }
}
