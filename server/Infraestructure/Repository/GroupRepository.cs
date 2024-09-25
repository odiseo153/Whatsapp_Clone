

using Whatsapp_Api.Core.Models;
using Infraestructure.Interfaces;
using System.Linq.Expressions;

namespace Whatsapp_Api.Infraestructure.Repository
{
    public class GroupRepository : IGenericsMethod<Group>
    {
        public Task<Group> Create(Group entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAllAsync(Expression<Func<Group, bool>> filter = null, Func<IQueryable<Group>, IOrderedQueryable<Group>> orderBy = null, int? page = null, int? pageSize = null, Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<Group> Update(Group entity)
        {
            throw new NotImplementedException();
        }
    }
}






