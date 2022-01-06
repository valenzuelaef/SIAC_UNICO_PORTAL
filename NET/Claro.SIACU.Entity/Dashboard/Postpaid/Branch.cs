using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
     [DataContract(Name = "BranchPostPaid")]
    public class Branch
    {
        [DataMember]
        public string codsolot { get; set; }

        [DataMember]
        public string tipotrabajo { get; set; }

        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public string sucursal { get; set; }

        [DataMember]
        public string codcliente { get; set; }

        [DataMember]
        public string contrato { get; set; }

        [DataMember]
        public string notas { get; set; }

        [DataMember]
        public string plano { get; set; }

        [DataMember]
        public string ubigeo { get; set; }

        [DataMember]
        public string departamento { get; set; }

        [DataMember]
        public string provincia { get; set; }

        [DataMember]
        public string distrito { get; set; }
    }
}
