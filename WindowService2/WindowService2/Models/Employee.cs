using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowService2.Models
{
    public class Employee
    {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string? Department { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
