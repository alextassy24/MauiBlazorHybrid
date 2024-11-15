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

}
