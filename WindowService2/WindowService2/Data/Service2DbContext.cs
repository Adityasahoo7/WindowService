using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowService2.Models;

namespace WindowService2.Data
{
    public class Service2DbContext:DbContext
    {
        public Service2DbContext(DbContextOptions<Service2DbContext> options):base(options)
        {
                
        }

        public DbSet<Employee>Employee2DS { get; set; }

        public DbSet<SyncControl> SyncControls { get; set; }
    }
}
