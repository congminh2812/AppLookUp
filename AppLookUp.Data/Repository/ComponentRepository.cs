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

        public async Task<IEnumerable<Component>> GetTop10(string? keyword)
        {
            var data = _db.Components.AsQueryable();

            if (keyword is not null)
                data = data.Where(s => s.Name.ToLower().Contains(keyword.ToLower()));

            if (data.Count() > 10)
                data = data.Take(10);

            return await data.ToListAsync();
        }
    }
}
