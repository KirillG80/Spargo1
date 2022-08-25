using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}

