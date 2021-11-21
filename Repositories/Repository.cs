using Dapper;
using Dapper.Contrib.Extensions;
using Imagegram.Functions.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Imagegram.Functions.Repositories
{
    public class Repository<TClass> : IRepository<TClass> where TClass : BaseEntity, new()
    {
        private readonly string _sqlConnection;

        public Repository()
        {
            _sqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
        }

        public async Task Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();

                await connection.DeleteAsync(new TClass { Id = id });
            }
        }

        public async Task<int> Insert(TClass obj)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                return await connection.InsertAsync(obj);
            }
        }

        public async Task<IEnumerable<TClass>> Query(string query, object param = null)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                return await connection.QueryAsync<TClass>(query, param);
            }
        }
    }
}
