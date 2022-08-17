using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public class StorageRepository : IRepository<Storage>
    {
        private readonly string connectionStr;
        public StorageRepository(string connStr)
        {
            connectionStr = connStr;
        }
        public async Task Create(Storage entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Storage(Name, Pharm_Id) VALUES (@name, @pharm_Id)";
                command.Connection = connection;
                SqlParameter nameParam = new SqlParameter("@name", entity.Name);
                command.Parameters.Add(nameParam);
                SqlParameter pharmParam = new SqlParameter("@pharm_Id", entity.Pharm_Id);
                command.Parameters.Add(pharmParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Storage WHERE id=@id";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            List<Storage> result = new List<Storage>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Storage";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int storId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int pharm_Id = reader.GetInt32(2);

                        var stor = new Storage(storId, name, pharm_Id);
                        result.Add(stor);
                    }
                }

                await reader.CloseAsync();
            }
            return result.ToArray();
        }

        public async Task<Storage> GetById(int id)
        {
            Storage result = null;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Storage WHERE id=@id";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int storId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int pharm_Id = reader.GetInt32(2);

                        result = new Storage(storId, name, pharm_Id);
                    }
                }

                await reader.CloseAsync();
            }
            return result;
        }

        public void Update(Storage entity)
        {
            throw new NotImplementedException();
        }
    }
}
