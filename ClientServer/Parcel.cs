using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class Parcel
    {
        public int PId { get; set; }
        public decimal PTax { get; set; }
        public int? PReciever { get; set; }
        public int? PInventory { get; set; }
        public int? POffice { get; set; }
        public string PTariff { get; set; }

        public virtual Inventory PInventoryNavigation { get; set; }
        public virtual MailPost POfficeNavigation { get; set; }
        public virtual Client PRecieverNavigation { get; set; }
    }
}
