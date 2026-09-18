using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowService2.Models;

namespace WindowService2.Data
{
    public class Service1DbContext:DbContext
    {
        public Service1DbContext(DbContextOptions<Service1DbContext> option):base(option)
        {
                
        }

        public DbSet<Employee> Employee1DS { get; set; }
    }
}
