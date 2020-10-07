using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class Client
    {
        public Client()
        {
            ClientNotes = new HashSet<ClientNotes>();
            UserAccount = new HashSet<UserAccount>();
        }

        public int ClientId { get; set; }
        public string Address { get; set; }
        public string ContactNmuber { get; set; }

        public virtual ICollection<ClientNotes> ClientNotes { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
