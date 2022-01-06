using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response
{
    public class responseData
    {
        public List<ListaPeticiones> listaPeticiones { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
