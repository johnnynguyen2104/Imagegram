using Imagegram.Functions.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagegram.Functions.Repositories
{
    public interface IRepository<TClass> where TClass : BaseEntity
    {
        Task<int> Insert(TClass obj);

        Task Delete(int id);

        Task<IEnumerable<TClass>> Query(string query, object param = null);
    }
}
