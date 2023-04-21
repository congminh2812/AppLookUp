namespace AppLookUp.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITypeInfoRepository TypeInfo { get; }
        IInformationRepository Information { get; }
        IAppUserRepository AppUser { get; }
        IComponentRepository Component { get; }
        Task<T> Delete<T>(T entity);
        Task<int> SaveAsync();
    }
}
