using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Models
{
    public class ProductionDetails
    {
        public string UserName { get; set; }
        public string Date { get; set; }
        public string ProductCount { get; set; }
        public string Status { get; set; }
        public bool IsUnread { get; set; } = false;
        public bool? IsOnline { get; set; } = false;
        public bool? IsNoOfNotification { get; set; } = false;
    }
}
