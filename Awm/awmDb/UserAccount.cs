using System;
using System.Collections.Generic;

namespace Awm.AwmDb
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Job = new HashSet<Job>();
            Shift = new HashSet<Shift>();
        }

        public string EmailAddressId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<Shift> Shift { get; set; }
    }
}
