using Data.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataGenerator
    {
        public async static Task Initialize(IConfiguration configuration)
        {
            using (var context = new DataContext(configuration))
            {
                if (context.Shipments.Any())
                {
                    return;   // Data was already seeded
                }


                await context.Drivers.AddRangeAsync(
                        new Driver
                        {
                            id = "EU.EORI.BUDR0000003",
                            first_name = "John",
                            last_name = "Colins",
                            vehicle_plate = "65438",
                            start_date = "2022-06-09T00:00:00Z",
                            expiration_date = "2022-06-09T00:00:00Z",
                            created_by = "2022-06-09T11:02:55Z",
                            updated_by = "2022-06-09T11:02:55Z",
                            active = 1,
                        },
                         new Driver
                         {
                             id = "EU.EORI.BUDR0000004",
                             first_name = "Paul",
                             last_name = "young",
                             vehicle_plate = "58974",
                             start_date = "2022-06-09T00:00:00Z",
                             expiration_date = "2022-06-09T00:00:00Z",
                             created_by = "2022-06-09T11:02:55Z",
                             updated_by = "2022-06-09T11:02:55Z",
                             active = 1,
                         }
                    );
                await context.Shipments.AddRangeAsync(
                       new Shipment
                       {
                           id = "22bb783d8c94431997ec7deba3920f10",
                           origin = "EU.EORI.BUWH0000001",
                           destination = "EU.EORI.BUNU0000005",
                           status = 1,
                           shipment_date = "2022-06-09T00:00:00Z",
                           driverid = "EU.EORI.BUDR0000003",
                           planned_date = "2022-06-09T00:00:00Z",
                           effective_date = "2022-06-09T00:00:00Z",
                           comments = "Created by LastMile in Medexis",
                           associated_barcode = "",
                           created_at = "2022-04-29T10:12:20Z",
                           created_by = "87c496e2d06847828ff235e14e9bf137",
                           updated_at = "2022-06-09T11:11:07Z",
                           updated_by = "f346ba682de14ff5a4e3abd2f3a23a33"
                       },
                        new Shipment
                        {
                            id = "0b112f986c084c07a287d1bddb61cd46",
                            origin = "EU.EORI.BUWH0000001",
                            destination = "EU.EORI.BUNU0000005",
                            status = 1,
                            shipment_date = "2022-06-07T00:00:00Z",
                            driverid = "EU.EORI.BUDR0000003",
                            planned_date = "2022-06-07T00:00:00Z",
                            effective_date = "2022-06-07T00:00:00Z",
                            comments = "Created by LastMile in Medexis",
                            associated_barcode = "LM_Ship_f4600c34d7a24d86a836355790fc5c5c_58",
                            created_at = "2022-04-29T10:12:20Z",
                            created_by = "87c496e2d06847828ff235e14e9bf137",
                            updated_at = "2022-06-09T11:02:55Z",
                            updated_by = "f346ba682de14ff5a4e3abd2f3a23a33"
                        }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
