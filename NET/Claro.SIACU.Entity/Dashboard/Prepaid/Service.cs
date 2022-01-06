using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "ServicePrePaid")]
    public class Service
    {
        [DataMember]
        public string NroCelular { get; set; }

        [DataMember]
        public string StatusLinea { get; set; }

        [DataMember]
        public string ProviderID { get; set; }

        [DataMember]
        public string PlanTarifario { get; set; }

        [DataMember]
        public string FecExpLinea { get; set; }

        [DataMember]
        public string SaldoPrincipal { get; set; }

        [DataMember]
        public string FechaExpiracionSaldo { get; set; }

        [DataMember]
        public string CambiosTriosGratis { get; set; }

        [DataMember]
        public string CambiosTarifaGratis { get; set; }

        [DataMember]
        public string NroFamAmigos { get; set; }

        [DataMember]
        public string StatusIMSI { get; set; }

        [DataMember]
        public string SaldoMinutosSelect { get; set; }

        [DataMember]
        public string FechaExpSelect { get; set; }

        [DataMember]
        public string IsSelect { get; set; }

        [DataMember]
        public bool EsTFI { get; set; }

        [DataMember]
        public string FecActivacion { get; set; }

        [DataMember]
        public string SubscriberStatus { get; set; }

        [DataMember]
        public string CNTNumber { get; set; }

        [DataMember]
        public string IsCNTPossible { get; set; }

        [DataMember]
        public string NroIMSI { get; set; }

        [DataMember]
        public string FecDol { get; set; }

        [DataMember]
        public string Pago { get; set; }

        [DataMember]
        public string ICCID { get; set; }

        [DataMember]
        public string BancaMovil { get; set; }

        [DataMember]
        public string EstadoLinea { get; set; }

        [DataMember]
        public string MotivoBloqueo { get; set; }

        [DataMember]
        public string AlertaBloqueo { get; set; }

        [DataMember]
        public string Segmento { get; set; }

        [DataMember]
        public string TipoTriacion { get; set; }

        [DataMember]
        public string DescriptionTypeTriation { get; set; }

        [DataMember]
        public string DescripcionPlan { get; set; }

        [DataMember]
        public string EstadoDias { get; set; }

        [DataMember]
        public string EstadoSubscriber { get; set; }

        [DataMember]
        public string DiasEstado { get; set; }

        [DataMember]
        public string Tecnologia4G { get; set; }

        [DataMember]
        public string TechnologyVoLTE { get; set; }

        [DataMember]
        public string TechnologyVoWifi { get; set; }
        
        [DataMember]
        public string Respuesta { get; set; }

        [DataMember]
        public double Saldo { get; set; }

        [DataMember]
        public string SaldoPendiente { get; set; }

        [DataMember]
        public double FactorDivision { get; set; }

        [DataMember]
        public List<Account> listAccount { get; set; }

        [DataMember]
        public List<Account> listAccounts { get; set; }

        [DataMember]
        public List<Trio> listTrio { get; set; }

        [DataMember]
        public List<Portability> listPortability { get; set; }

        [DataMember]
        public string ResponsePortability { get; set; }

        [DataMember]
        public Portability oPortability { get; set; }

        [DataMember]
        public string TipoContacto { get; set; }

        [DataMember]
        public string BondRFA { get; set; }

        [DataMember]
        public string BondAmountRFA { get; set; }
        [DataMember]
        public string Transaction { get; set; }

        [DataMember]
        public List<Balance> listBalance { get; set; }

    }
}