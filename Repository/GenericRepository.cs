using Microsoft.EntityFrameworkCore;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StockContext _context;
        internal DbSet<T> _dbSet;
        public GenericRepository(StockContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }



        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> CreateNew(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return default(T); // or return null; or return a message if needed
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChangesAsync();

            return entity;
        }

    }
}
