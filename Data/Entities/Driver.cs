using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Driver
    {
        [Key]
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string vehicle_plate { get; set; }
        public string start_date { get; set; }
        public string expiration_date { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public int active { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }

        public Driver()
        {
            Shipments = new HashSet<Shipment>();
        }
    }
}
