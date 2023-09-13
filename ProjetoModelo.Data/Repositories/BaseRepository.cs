using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Data.Context;
using Microsoft.EntityFrameworkCore;
namespace ProjetoModelo.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly HPEContext _context;

        public BaseRepository(HPEContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task Add(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> AddEntity(TEntity obj)
        {
            var entity = _context.Set<TEntity>().Add(obj).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task AddRange(IEnumerable<TEntity> objs)
        {
            _context.Set<TEntity>().AddRange(objs);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<TEntity> objs)
        {
            _context.Set<TEntity>().UpdateRange(objs);
            await _context.SaveChangesAsync();
        }
    }
}
