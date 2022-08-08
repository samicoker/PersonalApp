using PersonalApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Models
{
    public class DbContext
    {

        public static List<Personal> personalList = new List<Personal>
            {
                new Personal{Id = 0,Name="Sami",Surname="Çoker",DayOffStart = new DateTime(2022,08,30), DayOffEnd=new DateTime(2022,09,01),DepartmentId=0},

                new Personal{Id = 1,Name="Sefa",Surname="Öztürk",DayOffStart = new DateTime(2022,08,12), DayOffEnd=new DateTime(2022,08,20),DepartmentId=1},

                new Personal{Id = 2,Name="Serhat",Surname="Kobulan",DayOffStart = new DateTime(2022,08,12), DayOffEnd=new DateTime(2022,08,20),DepartmentId=0}
            };

        public static List<Department> departmentList = new List<Department>
            {
                new Department{Id=0,Name="BackEnd"},
                new Department{Id=1,Name="FrontEnd"},
                new Department{Id=2,Name="Yönetim"}
            };
    }
}
