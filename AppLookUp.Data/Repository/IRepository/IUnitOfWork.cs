namespace AppLookUp.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITypeInfoRepository TypeInfo { get; }
        IInformationRepository Information { get; }
        IAppUserRepository AppUser { get; }
        Task<T> Delete<T>(T entity);
        Task<int> SaveAsync();
    }
}
