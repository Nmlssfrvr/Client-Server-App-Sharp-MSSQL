using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class UserDatum
    {
        public int UId { get; set; }
        public int? UInfo { get; set; }
        public string ULogin { get; set; }
        public string UPassHash { get; set; }
        public int? URole { get; set; }

        public virtual Client UInfoNavigation { get; set; }
        public virtual Group URoleNavigation { get; set; }
    }
}
