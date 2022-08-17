using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public class GoodsRepository : IRepository<Goods>
    {
        private readonly string connectionStr;

        public GoodsRepository(string connectionString)
        {
            connectionStr = connectionString;
        }
        public async Task Create(Goods entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Goods(Name, Price) VALUES (@name, @price)";
                command.Connection = connection;
                SqlParameter nameParam = new SqlParameter("@name", entity.Name);
                command.Parameters.Add(nameParam);
                SqlParameter priceParam = new SqlParameter("@price", entity.Price);
                command.Parameters.Add(priceParam);

                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Goods WHERE id=@id";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<IEnumerable<Goods>> GetAll()
        {
            List<Goods> result = new List<Goods>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT Id, Name, Price FROM Goods";
                command.Connection = connection;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync()) 
                    {
                        int goodsId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);

                        var goods = new Goods(goodsId, name, price);
                        result.Add(goods);
                    }
                }

                await reader.CloseAsync();
            }
            return result;
        }

        public async Task<Goods> GetById(int id)
        {
            Goods result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "SELECT Id, Name, Price FROM Goods WHERE id=@id";
                    command.Connection = connection;
                    SqlParameter idParam = new SqlParameter("@id", id);
                    command.Parameters.Add(idParam);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            int goodsId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            decimal price = reader.GetDecimal(2);

                            result = new Goods(goodsId, name, price);
                        }
                    }

                    await reader.CloseAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public void Update(Goods entity)
        {
            throw new NotImplementedException();
        }

    }
}
