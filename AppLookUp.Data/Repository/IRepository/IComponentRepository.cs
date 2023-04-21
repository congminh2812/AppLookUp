using AppLookUp.Models;

namespace AppLookUp.Data.Repository.IRepository
{
    public interface IComponentRepository : IRepository<Component>
    {
        Task<Component> GetUpsert(int? id);
        Task<IEnumerable<Component>> GetTop10(string? keyword);
    }
}
