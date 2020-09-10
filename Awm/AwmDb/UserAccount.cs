using System;
using System.Collections.Generic;

namespace Awm.awmDb
{
    public partial class UserAccount
    {
        public int EmailAddressId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Type { get; set; }
    }
}
