using Core.DTO;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithSpec(ISpecification<T> spec);
        Task<int> CreateAsinc(T entity);
        void DeleteAsinc(T entity);
        Task<int> UpdateAsinc(T entity);
        Task<IReadOnlyList<T>> GetAllAsinc();
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

    }
}
