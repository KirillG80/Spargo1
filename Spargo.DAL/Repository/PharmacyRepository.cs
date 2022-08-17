using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public class PharmacyRepository : IRepository<Pharmacy>
    {
        private readonly string connectionStr;
        public PharmacyRepository(string connStr)
        {
            connectionStr = connStr;
        }
        public async Task Create(Pharmacy entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Pharmacy(Name, Address, Phone) VALUES (@name, @addr, @phone)";
                command.Connection = connection;
                SqlParameter nameParam = new SqlParameter("@name", entity.Name);
                command.Parameters.Add(nameParam);
                SqlParameter addrParam = new SqlParameter("@addr", entity.Address);
                command.Parameters.Add(addrParam);
                SqlParameter phoneParam = new SqlParameter("@phone", entity.Phone);
                command.Parameters.Add(phoneParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Pharmacy WHERE id=@id";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Pharmacy>> GetAll()
        {
            List<Pharmacy> result = new List<Pharmacy>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Pharmacy";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int goodsId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string address = reader.GetString(2);
                        string phone = reader.GetString(3);

                        var goods = new Pharmacy(goodsId, name, address, phone);
                        result.Add(goods);
                    }
                }

                await reader.CloseAsync();
            }
            return result.ToArray();
        }

        public async Task<Pharmacy> GetById(int id)
        {
            Pharmacy result = null;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Pharmacy WHERE id=@id";
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
                        string address = reader.GetString(2);
                        string phone = reader.GetString(3);

                        result = new Pharmacy(goodsId, name, address, phone);
                    }
                }

                await reader.CloseAsync();
            }
            return result;
        }

        public void Update(Pharmacy entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<QuantityOfGoods>>GetQntInPharmById(int Id)
        {
            List<QuantityOfGoods> result = new List<QuantityOfGoods>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "select ph.Id, ph.Name, g.Name, sum(p.Quantity) " +
                                        "from Pharmacy ph " +
                                        "inner join Storage st on st.Pharm_Id = ph.Id " +
                                        "inner join Party p on p.Stor_Id = st.Id " +
                                        "inner join Goods g on g.Id = p.Goods_Id " +
                                        "where ph.Id = @id " +
                                        "group by ph.Id, ph.Name, g.Name";
                command.Connection = connection;
                SqlParameter idParam = new SqlParameter("@id", Id);
                command.Parameters.Add(idParam);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int pharmId = reader.GetInt32(0);
                        string pharmName = reader.GetString(1);
                        string goodsName = reader.GetString(2);
                        int qnt = reader.GetInt32(3);

                        result.Add(new QuantityOfGoods()
                        {
                            PharmID = pharmId,
                            PharmName = pharmName,
                            GoodsName = goodsName,
                            Quantity = qnt
                        });
                    }
                }

                await reader.CloseAsync();
            }
            return result;
        }
    }
}
