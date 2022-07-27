using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CreateDriverRequest: Model
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string vehicle_plate { get; set; }
        public DateTime start_date { get; set; }
        public DateTime expiration_date { get; set; }
        public string created_by { get; set; } 
        public int active { get; set; }
    }  
    public class UpdateDriverRequest : Model
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string vehicle_plate { get; set; }
        public string start_date { get; set; }
        public string expiration_date { get; set; } 
        public int active { get; set; }
    }
}
