using Demo.DAL.presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Repostories._Generic
{
    public class GenericRepostory<T>:IGenericRepostory<T> where T : class
    {
        private protected readonly AppDbContext _context;

        public GenericRepostory(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool AsNotracking = true)
        {
            if (AsNotracking)
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        public IQueryable<T> GetAllQueryable()=> _context.Set<T>();
        
        public IEnumerable<T> GetAllEnumerable() =>  _context.Set<T>();
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public void Add(T entity)=> _context.Set<T>().Add(entity);
        public void Update(T entity)=> _context.Set<T>().Update(entity);
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}