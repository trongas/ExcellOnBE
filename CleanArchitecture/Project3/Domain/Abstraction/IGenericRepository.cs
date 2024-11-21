namespace Domain.Abstraction
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<bool> Add(T entity);
        Task<T?> GetById(TKey id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<int> Count();
        //Task<IEnumerable<T>> GetByPage(int page, int pageSize);

    }
}
