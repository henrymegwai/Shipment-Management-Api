using Data.Entities;
using Data.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class DriverRepository : IRepository<Driver>
    {
        private readonly DataContext context;
        public DriverRepository(DataContext _context)
        {
            context = _context;
        }
        public async Task<Driver> Add(Driver item)
        {
            int count = await context.Drivers.CountAsync();
            item.id = $"EU.EORI.BUDR000000{count + 3}";
            await context.AddAsync(item);
            int record = await context.SaveChangesAsync();
            if (record < 1)
            {
               throw new Exception("Driver record was not created");
            }
            return item;
        }

        public async Task<IEnumerable<Driver>> GetAllReadOnly()
        {
            return await context.Set<Driver>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Object>> GetForDropDown()
        {
            return await context.Set<Driver>().Select( x => new DriverDropDownList { id= x.id, name = $"{x.first_name} {x.last_name}" }).ToListAsync();
        }

        public async Task<Driver> GetById(string id)
        {
            DbSet<Driver> set = context.Set<Driver>();
            var objT = await set.FirstOrDefaultAsync(x=>x.id == id);
            if (objT != null)
                return objT;
            throw new Exception("Driver record not found");
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                var driverObj = await GetById(id);
                if (driverObj == null)
                    throw new Exception("Driver record not found");

                context.Set<Driver>().Remove(driverObj);
                int count = await context.SaveChangesAsync();
                if (count < 1) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Driver was not deleted successfully {ex.Message}");
            }
        }


        public async Task<int> Update(Driver driver)
        {
            var driverObj = await GetById(driver.id);
            if (driverObj == null)
                throw new Exception("Driver record not found");

            driverObj.vehicle_plate = driver.vehicle_plate ?? driverObj.vehicle_plate;
            driverObj.active = driver.active;
            driverObj.updated_by = "f346ba682de14ff5a4e3abd2f3a23a33";
            driverObj.first_name = driver.first_name ?? driverObj.first_name;
            driverObj.last_name = driver.last_name ?? driverObj.last_name;  
            context.Entry(driverObj).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public Task<bool> UpdateStatus(string id, object status)
        {
            throw new NotImplementedException();
        }
    }
}
