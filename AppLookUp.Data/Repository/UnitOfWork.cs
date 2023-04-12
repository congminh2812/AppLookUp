using AppLookUp.Data.Data;
using AppLookUp.Data.Repository.IRepository;
using AppLookUp.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppMVC.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ITypeInfoRepository TypeInfo { get; private set; }
        public IInformationRepository Information { get; private set; }
        public IAppUserRepository AppUser { get; private set; }

        public UnitOfWork(ApplicationDbContext db,
              UserManager<IdentityUser> userManager,
              RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            TypeInfo = new TypeInfoRepository(db);
            Information = new InformationRepository(db);
            AppUser = new AppUserRepository(db, userManager, roleManager);
        }

        public async Task<T> Delete<T>(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            if (typeof(IDeletable).IsAssignableFrom(typeof(T)))
            {
                ((IDeletable)entity).IsDeleted = true;
                _db.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
            }else 
                _db.Remove(entity);

            return entity;
        }

        public Task<int> SaveAsync()
        {
           return _db.SaveChangesAsync();
        }
    }
}
