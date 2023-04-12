using AppLookUp.Models;

namespace AppLookUp.Data.Repository.IRepository
{
    public interface IInformationRepository : IRepository<Information>
    {
        Task<Information> GetUpsert(int? id);
    }
}
