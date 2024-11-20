namespace BlazorHybrid.Interfaces.Repos
{
    public interface IRepository<T>
    {
        T GetById(object id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
    }

     public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
    }
}
