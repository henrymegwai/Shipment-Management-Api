using Data.Entities;
using Data.Helper;
using Microsoft.EntityFrameworkCore; 

namespace Data.Repositories
{
    public class ShipmentRepository : IRepository<Shipment>
    {
        private readonly DataContext context;
        public ShipmentRepository(DataContext _context)
        {
            context = _context;
        }
        public async Task<Shipment> Add(Shipment item)
        {
            item.id = Guid.NewGuid().ToString().Replace("-", "");
            await context.AddAsync(item);
            int count = await context.SaveChangesAsync();
            if (count < 1)
            {
                throw new Exception("Shipment record was not created");
            }
            return await GetById(item.id);
        }

        public async Task<IEnumerable<Shipment>> GetAllReadOnly()
        {
            return await context.Set<Shipment>().Include(x => x.driver).AsNoTracking().ToListAsync();
        }

        public async Task<Shipment> GetById(string id)
        {
            DbSet<Shipment> set =  context.Set<Shipment>();
            var objT = await set.Include(x => x.driver).FirstOrDefaultAsync(x=>x.id == id);
            if (objT != null)
                return objT;
            throw new Exception("Shipment record not found"); 
        }

        public Task<IEnumerable<Object>> GetForDropDown()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                var shipmentObj = await GetById(id);
                if (shipmentObj == null)
                    throw new Exception("Shipment record not found");

                context.Set<Shipment>().Remove(shipmentObj);
                int count = await context.SaveChangesAsync();
                if (count < 1) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Shipment was not deleted successfully {ex.Message}");
            }
        }


        public async Task<int> Update(Shipment entity)
        {
            var shipmentObj = await GetById(entity.id);
            if (shipmentObj == null)
                throw new Exception("Shipment does not exist");
            shipmentObj.status = entity.status;
            shipmentObj.origin = entity.origin;
            shipmentObj.destination = entity.destination;
            shipmentObj.shipment_date = entity.shipment_date;
            shipmentObj.effective_date = entity.effective_date;
            shipmentObj.planned_date = entity.planned_date;
            shipmentObj.comments = entity.comments;
            shipmentObj.associated_barcode = entity.associated_barcode;
            context.Entry(shipmentObj).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatus(string id, Object status)
        {
            var shipmentObj = await GetById(id);
            if (shipmentObj == null)
                throw new Exception("Shipment does not exist");
            shipmentObj.status = (int)status;
            context.Entry(shipmentObj).State = EntityState.Modified;
            int count =  await context.SaveChangesAsync();
            if (count < 1) { return false; }
            return true;
        }

         
    }
}
