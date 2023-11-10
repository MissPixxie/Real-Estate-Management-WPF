using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modern_Real_Estate.Model;
using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Utilities;

namespace Modern_Real_Estate.Model
{
   public class EstateManager : ListManager<Estate>
    {
        private static EstateManager instance;

        private EstateManager() { }

        public static EstateManager GetInstance()
        {
            if (instance == null)
            {
                instance = new EstateManager();
            }
            return instance;
        }
    }
}
