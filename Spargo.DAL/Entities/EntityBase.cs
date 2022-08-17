using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spargo.DAL
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public abstract string EntityToString();
    }
}
