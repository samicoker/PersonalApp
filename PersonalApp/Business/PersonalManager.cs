using PersonalApp.Core;
using PersonalApp.Entities;
using PersonalApp.Extensions;
using PersonalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Business
{
    public class PersonalManager : EntityRepositoryBase<Personal>
    {

        public PersonalManager() : base(DbContext.personalList)
        {
        }
        public bool AnyPersonalInDepartment(int departmentId)
        {
            return _entityList.Any(e => e.DepartmentId == departmentId);
        }
        public static void Operation(List<Personal> personalList, List<Department> departmentList)
        {
           
        }
    }
}
