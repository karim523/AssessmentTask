namespace Demo.DAL.presistance.Repostories._Generic
{
    public interface IGenericRepostory<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(bool AsNotracking = true);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAllQueryable();
        IEnumerable<T> GetAllEnumerable();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
