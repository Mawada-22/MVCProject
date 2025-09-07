using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repostries._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries.DepartmentRepos
{
    public class DepartmentRepostiry : GenericRepostiry<Department>,IDepartmentRepostiry
    {
        public DepartmentRepostiry(APPDBContext context) : base(context)
        {
        }
    }
}
