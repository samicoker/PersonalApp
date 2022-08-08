using PersonalApp.Core;
using PersonalApp.Entities;
using PersonalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Business
{
    public class DepartmentManager : EntityRepositoryBase<Department>
    {
        public DepartmentManager():base(DbContext.departmentList)
        {

        }
       public List<Personal> PersonalListWithDepartmentId(int departmentId)
        {
            return DbContext.personalList.Where(pers => pers.DepartmentId == departmentId).ToList();
        }

    }
}
