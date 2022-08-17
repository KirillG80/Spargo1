using Spargo.DAL;
using Spargo.Enums;
using Spargo.UI_Command;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Spargo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SparConnection"].ConnectionString;
            var ic = new InputCommands(connectionString);

            Console.WriteLine("Введите команду:");
            Console.WriteLine("1 1 n - вывести товар с ID=n,");
            Console.WriteLine("1 2 n - удалить товар с ID=n,");
            Console.WriteLine("1 3 name price- добавить товар с Name=name и ценой=price,");
            Console.WriteLine("1 5 - вывести список всех товаров,");
            Console.WriteLine("2 1 n - вывести аптеку с ID=n,");
            Console.WriteLine("2 2 n - удалить аптеку с ID=n,");
            Console.WriteLine("2 3 address phone- добавить аптеку с адрес=address и телефон=price,");
            Console.WriteLine("2 5 - вывести список всех аптек,");
            Console.WriteLine("2 6 n - Вывести список товаров и его количество в выбранной аптеке (количество товара во всех складах аптеки),");
            Console.WriteLine("3 1 n - вывести склад с ID=n,");
            Console.WriteLine("3 2 n - удалить склад с ID=n,");
            Console.WriteLine("3 3 name pharmId - добавить склад с с Name=name и ID аптеки=pharmId,");
            Console.WriteLine("3 5 - вывести список всех складов,");
            Console.WriteLine("4 1 n - вывести партию с ID=n,");
            Console.WriteLine("4 2 n - удалить партию с ID=n,");
            Console.WriteLine("4 3 goodsId storId quantity - добавить партию с с ID товара=goodsId,  ID склада=storId, кол-во=quantity");
            Console.WriteLine("4 5 - вывести список всех партий,");

            while (true)
            {
                string str = Console.ReadLine();
                var inputs = str.Split('\\');

                if (inputs.Length >= 2)
                {
                    //EntityType entytyType = (EntityType)Enum.Parse(typeof(EntityType), inputs[0], true);
                    EntityType entytyType;
                    CommandType comType;
                    bool res1 = Enum.TryParse(inputs[0], out entytyType) && Enum.IsDefined(typeof(EntityType), entytyType);
                    bool res2 = Enum.TryParse(inputs[1], out comType) && Enum.IsDefined(typeof(CommandType), comType);
                    if (res1 && res2)
                    {
                        await ic.GetDataAsync(entytyType, comType, inputs);
                    }
                    else
                    {
                        Console.Write("Неверно задана команда!");
                    }
                }
                else
                {
                    Console.Write("Неверно задана команда! ");
                }
                
            }
        }
    }
}
