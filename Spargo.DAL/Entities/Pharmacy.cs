using System;
using System.Collections.Generic;
using System.Text;

namespace Spargo.DAL
{
    public class Pharmacy: EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Pharmacy(int ID)
        {
            Id = ID;
        }
        public Pharmacy(int ID, string name, string address, string phone)
        {
            Id = ID;
            Name = name;
            Address = address;
            Phone = phone;
        }
        public override string EntityToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", Id, Name, Address, Phone);
        }
    }
}
