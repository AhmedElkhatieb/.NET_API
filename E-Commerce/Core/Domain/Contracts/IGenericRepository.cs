using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        #region Specification
        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(Specifications<TEntity> specifications);
        Task<TEntity?> GetByIdWithSpecificationsAsync(Specifications<TEntity> specifications);
        #endregion
        Task<TEntity>? GetAsync(TKey id);
        Task AddAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        // For Count
        Task<int> CountAsync(Specifications<TEntity> specifications);
    }
}
