using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataServiceTOBE.Response
{
    public class responseData
    {
        public responseData()
        {
            datosHFC = new List<datosHFC>();
        }
        public List<datosHFC> datosHFC { get; set; }
    }
}
