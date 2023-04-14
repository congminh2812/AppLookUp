using AppLookUp.Data.Data;
using AppLookUp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLookUp.Data.Repository.IRepository
{
    public class InformationRepository : Repository<Information>, IInformationRepository
    {
        private readonly ApplicationDbContext _db;

        public InformationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Information> GetUpsert(int? id)
        {
            var information = await _db.Informations.FirstOrDefaultAsync(x => x.Id == id) ?? new Information();

            return information;
        }

        public async Task<IEnumerable<Information>> GetTop10()
        {
            var data = await _db.Informations.Where(s => !s.IsDeleted).Take(10).ToListAsync();

            return data;
        }
    }
}
