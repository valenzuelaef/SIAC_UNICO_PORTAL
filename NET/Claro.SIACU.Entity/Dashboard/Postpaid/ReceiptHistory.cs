using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ReceiptHistory")]
    public class ReceiptHistory
    {

        [Data.DbColumn("InvoiceNumber")]
        [DataMember]
        public string InvoiceNumber { get; set; }


        [Data.DbColumn("CCName")]
        [DataMember]
        public string CCName { get; set; }


        [Data.DbColumn("ContactClient")]
        [DataMember]
        public string ContactClient { get; set; }


        [Data.DbColumn("CCAddr1")]
        [DataMember]
        public string CCAddr1 { get; set; }


        [Data.DbColumn("CCAddr2")]
        [DataMember]
        public string CCAddr2 { get; set; }


        [Data.DbColumn("Distrito")]
        [DataMember]
        public string Distrito { get; set; }


        [Data.DbColumn("Provincia")]
        [DataMember]
        public string Provincia { get; set; }


        [Data.DbColumn("Departamento")]
        [DataMember]
        public string Departamento { get; set; }


        [Data.DbColumn("NroDoc")]
        [DataMember]
        public string NroDoc { get; set; }


        [Data.DbColumn("FechaInicio")]
        [DataMember]
        public string FechaInicio { get; set; }


        [Data.DbColumn("FechaFin")]
        [DataMember]
        public string FechaFin { get; set; }


        [Data.DbColumn("FechaEmision")]
        [DataMember]
        public string FechaEmision { get; set; }


        [Data.DbColumn("FechaVencimiento")]
        [DataMember]
        public string FechaVencimiento { get; set; }


        [Data.DbColumn("CodCliente")]
        [DataMember]
        public string CodCliente { get; set; }


        [Data.DbColumn("NroCiclo")]
        [DataMember]
        public string NroCiclo { get; set; }


        [Data.DbColumn("Periodo")]
        [DataMember]
        public string Periodo { get; set; }


        [Data.DbColumn("TotalPrevCharges")]
        [DataMember]
        public decimal TotalPrevCharges { get; set; }


        [Data.DbColumn("TotalPaymentsRcvd")]
        [DataMember]
        public decimal TotalPaymentsRcvd { get; set; }


        [Data.DbColumn("TotalPrevBalance")]
        [DataMember]
        public decimal TotalPrevBalance { get; set; }


        [Data.DbColumn("TotalCurrentCharges")]
        [DataMember]
        public decimal TotalCurrentCharges { get; set; }


        [Data.DbColumn("TotalTaxes")]
        [DataMember]
        public decimal TotalTaxes { get; set; }


        [Data.DbColumn("TotalAmountDue")]
        [DataMember]
        public decimal TotalAmountDue { get; set; }


        [Data.DbColumn("Mes")]
        [DataMember]
        public string Mes { get; set; }


        [Data.DbColumn("Anho")]
        [DataMember]
        public string Anho { get; set; }

    }
}
