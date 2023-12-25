using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RouletteDemo.Api.Interfaces;

namespace RouletteDemo.Api.Data
{
    public class EntityRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _dbSet.FindAsync(id, cancellationToken)
                    .ConfigureAwait(false);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken)
                    .ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

        }

        public IEnumerable<T> GetItems(Func<T, bool> filter)
        {
            return _dbSet.Where(filter);
        }

        public async Task<T> GetLastItem(CancellationToken cancellationToken)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _dbSet.OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
            #pragma warning restore CS8603 // Possible null reference return.
        }
    }
}