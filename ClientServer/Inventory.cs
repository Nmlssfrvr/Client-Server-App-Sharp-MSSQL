using System;
using System.Collections.Generic;

#nullable disable

namespace ClientServer
{
    public partial class Inventory
    {
        public Inventory()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int IId { get; set; }
        public string IProductName { get; set; }
        public int IAmount { get; set; }
        public decimal IPrice { get; set; }
        public string IFragile { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
