using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public class Goods : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Goods(int ID)
        {
            Id = ID;
        }
        public Goods(int ID, string name, decimal price)
        {
            Id = ID;
            Name = name;
            Price = price;
        }

        public override string EntityToString()
        {
            return string.Format("{0}\t{1}", Id, Name);
        }
    }
}
