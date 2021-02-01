using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class Group
    {
        public Group()
        {
            UserData = new HashSet<UserDatum>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int GId { get; set; }
        public string GGroup { get; set; }

        public virtual ICollection<UserDatum> UserData { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
