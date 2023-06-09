﻿using AppLookUp.Models;

namespace AppLookUp.Data.Repository.IRepository
{
    public interface IInformationRepository : IRepository<Information>
    {
        Task<Information> GetUpsert(int? id);
        Task<IEnumerable<Information>> GetTop10(string? keyword);
    }
}
