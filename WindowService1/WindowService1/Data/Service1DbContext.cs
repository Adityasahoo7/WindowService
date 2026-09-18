using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowService1.Models;

namespace WindowService1.Data
{
    public class Service1DbContext:DbContext
    {
        public Service1DbContext(DbContextOptions<Service1DbContext> options):base(options)
        {
                
        }

        public DbSet<Employee> employeeDS { get; set; }
    }
}
