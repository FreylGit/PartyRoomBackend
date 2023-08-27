namespace PartyRoom.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> Models { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
