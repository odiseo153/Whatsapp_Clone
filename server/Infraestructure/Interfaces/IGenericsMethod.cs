using System.Linq.Expressions;

namespace Infraestructure.Interfaces
{
    public interface IGenericsMethod<T> where T : class
    {
        public Task<T> Create(T entity);
        
        public Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? page = null,
        int? pageSize = null,
        Guid? id = null
       );
        
        public Task<T> Update(T entity);
     
        public bool Delete(string id);
    }

}


