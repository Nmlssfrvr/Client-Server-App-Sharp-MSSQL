using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class Client
    {
        public Client()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int CId { get; set; }
        public decimal? CIndex { get; set; }
        public string CFn { get; set; }
        public short CPassportSeries { get; set; }
        public int CPassportNumber { get; set; }

        public virtual MailPost CIndexNavigation { get; set; }
        public virtual UserDatum UserDatum { get; set; }
        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
