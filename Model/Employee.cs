using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model
{
    public abstract class Employee : User, IEmployee
    {
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public Employee() { }
    }
}
