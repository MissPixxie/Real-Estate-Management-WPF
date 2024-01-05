using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model
{
    public interface IEmployee
    {
        decimal Salary { get; set; }
        string Department { get; set; }
    }
}
