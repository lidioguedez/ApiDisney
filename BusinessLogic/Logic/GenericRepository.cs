using BusinessLogic.Data;
using Core.DTO;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApiDisneyDbContext _context;

        public GenericRepository(ApiDisneyDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsinc(T entity) 
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public void DeleteAsinc(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsinc()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> UpdateAsinc(T entity)
        {
            //_context.Set<T>().Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

    }
}
