using System.Linq.Expressions;

namespace FuelManagerApi.Repository.Interfaces
{
    public interface IBaseRepository<T> 
    {
        public T Add(T entity);
        public T Update(T entity);
        public void Delete(T entity);
        T? Get(Expression<Func<T, bool>> predicate);

        public IEnumerable<T> GetAll();

    }
}
