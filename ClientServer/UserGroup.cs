using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class UserGroup
    {
        public int UgId { get; set; }
        public int? UgGroup { get; set; }
        public int? UgRole { get; set; }

        public virtual Group UgGroupNavigation { get; set; }
        public virtual Role UgRoleNavigation { get; set; }
    }
}
