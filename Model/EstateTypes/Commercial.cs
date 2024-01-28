using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public abstract class Commercial : Estate
    {
        private string _type;
        public string Type
        {
            get { return "Commercial"; }
        }
    }
}
