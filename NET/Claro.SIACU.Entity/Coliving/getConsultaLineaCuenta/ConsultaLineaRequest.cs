﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta
{
    [DataContract]
    public class ConsultaLineaRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
