using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public class VillaRepository : IVillaRepository

    {
        private readonly ApplicationDbContext _db;
        
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public async Task CreateAsync(Villa entity)
        {
            await _db.Villas.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(Villa entity)
        {
           _db.Villas.Remove(entity);
            await SaveAsync();
        }

       

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        

        public async Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null, bool track = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if(!track)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            _db.Update(entity);
            await SaveAsync();
        }
    }
}
