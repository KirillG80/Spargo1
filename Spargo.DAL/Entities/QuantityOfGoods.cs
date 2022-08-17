using System;
using System.Collections.Generic;
using System.Text;

namespace Spargo.DAL
{
    public class QuantityOfGoods
    {
        public int PharmID { set; get; }
        public string PharmName { get; set; }
        public string GoodsName { get; set; }
        public int Quantity { get; set; }
        public string EntityToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", PharmID, PharmName, GoodsName, Quantity);
        }
    }
}
