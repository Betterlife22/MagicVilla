using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter= null);
        Task <Villa> GetVillaAsync(Expression<Func<Villa,bool>> filter= null , bool track=true);
        Task CreateAsync(Villa entity);
       
        Task DeleteAsync(Villa entity);
        Task UpdateAsync(Villa entity);
        Task SaveAsync();
    }
}
