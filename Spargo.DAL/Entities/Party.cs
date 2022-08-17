using System;
using System.Collections.Generic;
using System.Text;

namespace Spargo.DAL
{
    public class Party: EntityBase
    {
        public int Goods_Id { get; set; }
        public int Stor_Id { get; set; }
        public int Quantity { get; set; }

        public Party(int ID, int goodsId, int storId, int quantity)
        {
            Id = Id;
            Goods_Id = goodsId;
            Stor_Id = storId;
            Quantity = quantity;
        }

        public override string EntityToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", Id, Goods_Id, Stor_Id, Quantity);
        }
    }
}
