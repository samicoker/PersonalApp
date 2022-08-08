using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Entities
{
    public class Personal: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? DayOffStart { get; set; }
        public DateTime? DayOffEnd { get; set; }
    }
}
