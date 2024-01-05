using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model
{
    interface IUser
    {
        int Id { get; }
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        int Phone { get; set; }
        DateTime Created { get; set; }
    }
}
