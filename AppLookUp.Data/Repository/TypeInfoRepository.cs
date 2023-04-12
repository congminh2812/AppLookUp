using AppLookUp.Data.Data;
using AppLookUp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLookUp.Data.Repository.IRepository
{
    public class TypeInfoRepository : Repository<TypeInfo>, ITypeInfoRepository
    {
        private readonly ApplicationDbContext _db;
        public TypeInfoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<TypeInfo> GetUpsert(int? id)
        {
            var typeInfo = await _db.TypeInfos.FirstOrDefaultAsync(x => x.Id == id) ?? new TypeInfo();

            return typeInfo;
        }
    }
}
