using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "SharedBagPostPaid")]
    public class SharedBag
    {
        public SharedBag()
        {
            lstAccountSharedBag = new List<SharedBag>();
            lstAccountSharedBagLine = new List<SharedBag>();
        }
        [DataMember]
        public string GRUPO_BOLSA { get; set; }
        [DataMember]
        public string CUENTA { get; set; }

        [DataMember]
        public string BOLSA { get; set; }

        [DataMember]
        public string LINEA { get; set; }

        [DataMember]
        public string UNIDAD { get; set; }

        [DataMember]
        public string TOPE { get; set; }

        [DataMember]
        public string CONSUMO { get; set; }

        [DataMember]
        public string SALDO { get; set; }

        [DataMember]
        public string FECHAVIGENCIA { get; set; }

        [DataMember]
        public string OPCIONAL1 { get; set; }

        [DataMember]
        public string OPCIONAL2 { get; set; }

        [DataMember]
        public string FLAGQUERY { get; set; }

        [DataMember]
        public string MENSAJERROR { get; set; }

        [DataMember]
        public List<SharedBag> lstAccountSharedBag { get; set; }

        [DataMember]
        public List<SharedBag> lstAccountSharedBagLine { get; set; }
    }
}