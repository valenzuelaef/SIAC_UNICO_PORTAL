using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{


    [DataContract(Name = "LoanRentalPostPaid")]
    public class LoanRental
    {
        public LoanRental()
        {
            lstCabAccounLoanRental = new List<LoanRental>();
            lstDebAccounLoanRental = new List<LoanRental>();
        }

        [DataMember]
        public string RAZON_SOCIAL { get; set; }

        [DataMember]
        public string RUC { get; set; }

        [DataMember]
        public string DIRECCION { get; set; }
        [DataMember]
        public string IMEI { get; set; }

        [DataMember]
        public string MOTIVO_SAP { get; set; }

        [DataMember]
        public string MODALIDAD_SAP { get; set; }

        [DataMember]
        public string FECHAS { get; set; }

        [DataMember]
        public string NUMERO_CLARIFY { get; set; }

        [DataMember]
        public string NUMERO_PEDIDO { get; set; }

        [DataMember]
        public string DENOMINACION { get; set; }

        [DataMember]
        public string VALOR_NETO { get; set; }

        [DataMember]
        public string MODELO { get; set; }


        [DataMember]
        public List<LoanRental> lstCabAccounLoanRental { get; set; }

        [DataMember]
        public List<LoanRental> lstDebAccounLoanRental { get; set; }
    }
}
