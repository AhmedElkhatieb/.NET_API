using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }


        public async Task AddAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);
        

        public void DeleteAsync(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);

        public void UpdateAsync(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);

        public async Task<TEntity?> GetAsync(TKey id) => await _storeContext.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (trackChanges)
            {
                return await _storeContext.Set<TEntity>().ToListAsync();
            }
            else
            {
                return await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

    }
}
