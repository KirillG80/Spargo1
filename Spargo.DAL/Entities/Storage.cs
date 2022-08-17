using System;
using System.Collections.Generic;
using System.Text;

namespace Spargo.DAL
{
    public class Storage: EntityBase
    {
        public int Pharm_Id { get; set; }
        public string Name { get; set; }
        public Storage(int ID, string name, int pharmId)
        {
            Id = ID;
            Name = name;
            Pharm_Id = pharmId;
        }

        public override string EntityToString()
        {
            return string.Format("{0}\t{1}\t{2}", Id, Name, Pharm_Id);
        }
    }
}
