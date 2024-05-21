using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetVillaAsync(Expression<Func<T, bool>>? filter = null, bool track = true);
        Task CreateAsync(T entity);

        Task DeleteAsync(T entity);
        
        Task SaveAsync();
    }
}
