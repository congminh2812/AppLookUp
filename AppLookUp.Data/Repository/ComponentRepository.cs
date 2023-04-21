using AppLookUp.Data.Data;
using AppLookUp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLookUp.Data.Repository.IRepository
{
    public class ComponentRepository : Repository<Component>, IComponentRepository
    {
        private readonly ApplicationDbContext _db;
        public ComponentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Component> GetUpsert(int? id)
        {
            var component = await _db.Components.FirstOrDefaultAsync(x => x.Id == id) ?? new Component();

            return component;
        }
    }
}
