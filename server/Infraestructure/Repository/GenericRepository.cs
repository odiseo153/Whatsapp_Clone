
using Whatsapp_Api.Core.Models;
using Whatsapp_Api.Infraestructure.Context;
using Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class GenericRepository<T>(SocialMediaContext context) : IGenericsMethod<T> where T : class
    {
        public async Task<T> Create(T Entity)
        {
           await  context.Set<T>().AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? page = null,
        int? pageSize = null,
        Guid? id = null
        )
        {
            try
            {
                var _dbSet = context.Set<T>();
                IQueryable<T> query = _dbSet;

                // Obtener por ID si se proporciona
                if (id != null)
                {
                    var entity = await _dbSet.FindAsync(id);
                    return entity != null ? new List<T> { entity } : new List<T>();
                }

                // Aplicar filtro
                if (filter != null)
                {
                    query = query.Where(filter);
                }


                // Aplicar ordenación
                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                // Aplicar paginación si está presente
                if (page.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;

            }
        }


        public bool Delete(string Id)
        {
            var entity = context.Set<T>().Find(Id);

            if (entity == null)
            {
                return false;
            }

            context.Remove(entity);
            context.SaveChanges();

            return true;
        }



        public async Task<T> Update(T entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException(nameof(entidad));
            }

            // Buscar la entidad existente en la base de datos
            var entidadExistente = await context.Set<T>().FindAsync(entidad.GetType().GetProperty("Id")?.GetValue(entidad));
            if (entidadExistente == null)
            {
                throw new InvalidOperationException("La entidad no fue encontrada en la base de datos.");
            }

            // Iterar sobre las propiedades de la entidad
            foreach (var propiedad in entidad.GetType().GetProperties())
            {
                // No actualizar propiedades de solo lectura o claves primarias
                if (!propiedad.CanWrite || propiedad.Name == "Id")
                {
                    continue;
                }

                var valorEntidad = propiedad.GetValue(entidad);
                var valorFinal = valorEntidad ?? propiedad.GetValue(entidadExistente);

                // Asignar el valor final a la entidad existente
                propiedad.SetValue(entidadExistente, valorFinal);
            }

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();

            return entidadExistente;
        }


    }
}
