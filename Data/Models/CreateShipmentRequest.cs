using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public  class CreateShipmentRequest: Model
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public Status status { get; set; } = Status.init;
        public DateTime shipment_date { get; set; } 
        public string driverid { get; set; }
        public DateTime planned_date { get; set; }
        public DateTime effective_date { get; set; }
        public string comments { get; set; }
        public string associated_barcode { get; set; }  
    }

    public class UpdateShipmentRequest: Model
    {
        public string id { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public Status status { get; set; }
        public string shipment_date { get; set; }
        public string driverid { get; set; }
        public string planned_date { get; set; }
        public string effective_date { get; set; }
        public string comments { get; set; }
        public string associated_barcode { get; set; }  
    }
}
