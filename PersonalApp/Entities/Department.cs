using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Entities
{
    public class Department: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
