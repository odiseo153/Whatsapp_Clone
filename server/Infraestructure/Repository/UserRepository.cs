using Whatsapp_Api.Core.Models;
 
using Infraestructure.Interfaces;
using System.Linq.Expressions;



namespace Whatsapp_Api.Infraestructure.Repository
{
    public class UserRepository() : IGenericsMethod<User>
    {
        public Task<User> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, int? page = null, int? pageSize = null, Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}



