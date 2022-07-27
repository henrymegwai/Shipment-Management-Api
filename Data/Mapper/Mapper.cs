using Data.Entities;
using Data.Helper;
using Data.Models;
using System.Globalization;

namespace Data.Mapper
{
    public static class Mapper
    {

        public static Shipment Map(this CreateShipmentRequest request)
        {
            return new Shipment
            {
                associated_barcode = request.associated_barcode,
                comments = request.comments,
                created_at = DateTime.UtcNow.Map(), 
                destination = request.destination,
                driverid = request.driverid,
                effective_date = request.effective_date.Map(),
                origin = request.origin,
                planned_date = request.planned_date.Map(),
                shipment_date = request.shipment_date.Map(),
                status = (int)request.status,
                updated_at = string.Empty,
                updated_by = string.Empty,
                created_by = "87c496e2d06847828ff235e14e9bf137"
            };
        }

        public static CreateShipmentResponse Map(this Shipment shipment)
        {
            return new CreateShipmentResponse
            {
                id = shipment.id,
                associated_barcode = shipment.associated_barcode,
                comments = shipment.comments,
                created_at = shipment.created_at,
                created_by = shipment.created_by,
                destination = shipment.destination,
                driverid = shipment.driverid,
                effective_date = shipment.effective_date,
                origin = shipment.origin,
                planned_date = shipment.planned_date,
                shipment_date = shipment.shipment_date,
                status = shipment.status.ToString().ToEnum<Status>(),
                driver_name = $"{shipment.driver.first_name} {shipment.driver.last_name}"
            };
        }

        public static UpdateShipmentRequest MapForUpdate(this Shipment shipment)
        {
            return new UpdateShipmentRequest
            {
                id = shipment.id,
                associated_barcode = shipment.associated_barcode,
                comments = shipment.comments, 
                destination = shipment.destination,
                driverid = shipment.driverid,
                effective_date = shipment.effective_date,
                origin = shipment.origin,
                planned_date = shipment.planned_date,
                shipment_date = shipment.shipment_date,
                status = shipment.status.ToString().ToEnum<Status>()
            };
        }

        public static Shipment Map(this UpdateShipmentRequest request)
        {
            return new Shipment
            {
                id = request.id,
                associated_barcode = request.associated_barcode,
                comments = request.comments, 
                destination = request.destination,
                driverid = request.driverid,
                effective_date = request.effective_date,
                origin = request.origin,
                planned_date = request.planned_date,
                shipment_date = request.shipment_date,
                status = (int)request.status, 
                updated_at = DateTime.UtcNow.Map(),
                updated_by = "f346ba682de14ff5a4e3abd2f3a23a33"
            };
        }

        
        public static Driver Map(this CreateDriverRequest request)
        {
            return new Driver
            {
                first_name = request.first_name,
                last_name = request.last_name,
                vehicle_plate = request.vehicle_plate,
                expiration_date = request.expiration_date.Map(),
                active = request.active, 
                start_date = request.start_date.Map(),
                created_by = "f346ba682de14ff5a4e3abd2f3a23a33", 
                updated_by = string.Empty
            };
        }

        public static CreateDriverResponse Map(this Driver driver)
        {
            return new CreateDriverResponse
            {
                id = driver.id,
                first_name = driver.first_name,
                last_name = driver.last_name,
                vehicle_plate = driver.vehicle_plate,
                expiration_date = driver.expiration_date,
                active = driver.active,
                start_date = driver.start_date,
                created_by = "f346ba682de14ff5a4e3abd2f3a23a33",
            };
        }
        public static UpdateDriverRequest MapForUpdate(this Driver driver)
        {
            return new UpdateDriverRequest
            {
                id = driver.id,
                first_name = driver.first_name,
                last_name = driver.last_name,
                vehicle_plate = driver.vehicle_plate,
                expiration_date = driver.expiration_date,
                active = driver.active,
                start_date = driver.start_date,
            };
        }

        public static Driver Map(this UpdateDriverRequest request)
        {
            return new Driver
            {
                id = request.id,
                first_name = request.first_name,
                last_name = request.last_name,
                vehicle_plate = request.vehicle_plate,
                expiration_date = request.expiration_date,
                active = request.active,
                start_date = request.start_date, 
                updated_by = "f346ba682de14ff5a4e3abd2f3a23a33"
            };
        }


        public static string Map(this DateTime dateTime) 
        {
            return dateTime.ToUniversalTime().ToString("s") + "Z";
        }
        
        public static DateTime Map(this string dateTime) 
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime parsedDate = DateTime.ParseExact(dateTime.Replace("Z", ""), "dd-MM-yyyy hh:mm:ss:tt", provider); 
            return parsedDate;
        }
    }
}
