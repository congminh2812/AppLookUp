using AppLookUp.Models;
using AppLookUp.Models.RequestModels;
using AppLookUp.Models.ViewModels;

namespace AppLookUp.Data.Repository.IRepository
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<IEnumerable<UserVM>> GetList();
        Task<UserReqModel> GetUpsert(string? id);
        AppUser CreateUser();
    }
}
