using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Domain.Models
{
    public class ShopModel
    {
        public Guid ShopId { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNo { get; set; }
        public PlaceDetails Location { get; set; }
    }
}
