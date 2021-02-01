using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class MailPost
    {
        public MailPost()
        {
            Clients = new HashSet<Client>();
            Parcels = new HashSet<Parcel>();
        }

        public int MId { get; set; }
        public decimal MIndex { get; set; }
        public string MAddress { get; set; }
        public decimal MPhone { get; set; }
        public byte MPostamatCount { get; set; }
        public DateTime? MLastUpdateTime { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
