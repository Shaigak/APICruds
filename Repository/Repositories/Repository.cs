using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AppDbContext _context;
        private readonly DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null) { throw new NullReferenceException(nameof(entity)); }
           await entities.AddAsync(entity);
            await _context.SaveChangesAsync();  
            
        }

        public async Task  DeleteAsync (T entity)
        {
            if (entity == null) { throw new NullReferenceException(nameof(entity)); }

            entities.Remove(entity);

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            T entity = await entities.FindAsync(id);

           if(entity ==null) { throw new NullReferenceException(nameof(entity)); }

            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }






    }
}
