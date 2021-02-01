using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class Role
    {
        public Role()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public int RId { get; set; }
        public string RRole { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
