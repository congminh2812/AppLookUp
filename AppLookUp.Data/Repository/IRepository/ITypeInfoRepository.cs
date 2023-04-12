using AppLookUp.Models;

namespace AppLookUp.Data.Repository.IRepository
{
    public interface ITypeInfoRepository : IRepository<TypeInfo>
    {
        Task<TypeInfo> GetUpsert(int? id);
    }
}
