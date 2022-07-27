using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public  class CreateShipmentResponse
    {
        public string id { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public Status status { get; set; }
        public string shipment_date { get; set; } 
        public string driverid { get; set; }
        public string driver_name { get; set; }
        public string planned_date { get; set; }
        public string effective_date { get; set; }
        public string comments { get; set; }
        public string associated_barcode { get; set; }
        public string created_at { get; set; }
        public string created_by { get; set; }
        public string updated_at { get; set; }
        public string updated_by { get; set; }
    }
    public  class UpdateShipmentResponse
    {
        public string id { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public int status { get; set; }
        public string shipment_date { get; set; } 
        public string driverid { get; set; }
        public string planned_date { get; set; }
        public string effective_date { get; set; }
        public string comments { get; set; }
        public string associated_barcode { get; set; }
        public string updated_at { get; set; }
        public string updated_by { get; set; }
    }
}
