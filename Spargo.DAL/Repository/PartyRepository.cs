using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public class PartyRepository : IRepository<Party>
    {
        private readonly string connectionStr;
        public PartyRepository(string connStr)
        {
            connectionStr = connStr;
        }
        public async Task Create(Party entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Party(Goods_Id, Stor_Id, Quantity) VALUES (@goods_Id, @stor_Id, @quantity)";
                command.Connection = connection;
                SqlParameter nameParam = new SqlParameter("@goods_Id", entity.Goods_Id);
                command.Parameters.Add(nameParam);
                SqlParameter pharmParam = new SqlParameter("@stor_Id", entity.Stor_Id);
                command.Parameters.Add(pharmParam);
                SqlParameter qntParam = new SqlParameter("@quantity", entity.Quantity);
                command.Parameters.Add(qntParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Party WHERE id=@id";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Party>> GetAll()
        {
            List<Party> result = new List<Party>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Party";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int pId = reader.GetInt32(0);
                        int goods_Id = reader.GetInt32(1);
                        int stor_Id = reader.GetInt32(2);
                        int qnt = reader.GetInt32(3);

                        var party = new Party(pId, goods_Id, stor_Id, qnt);
                        result.Add(party);
                    }
                }

                await reader.CloseAsync();
            }
            return result.ToArray();
        }

        public async Task<Party> GetById(int id)
        {
            Party result = null;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Party WHERE id=@id";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int pId = reader.GetInt32(0);
                        int goods_Id = reader.GetInt32(1);
                        int stor_Id = reader.GetInt32(2);
                        int qnt = reader.GetInt32(3);

                        result = new Party(pId, goods_Id, stor_Id, qnt);
                    }
                }

                await reader.CloseAsync();
            }
            return result;
        }

        public void Update(Party entity)
        {
            throw new NotImplementedException();
        }
    }
}
