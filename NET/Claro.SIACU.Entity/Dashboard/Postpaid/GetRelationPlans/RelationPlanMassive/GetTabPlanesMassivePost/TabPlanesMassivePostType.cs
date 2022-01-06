using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost
{
    [DataContract]
    public class TabPlanesMassivePostType
    {
        [DataMember(Name = "acceso")]
        public string acceso { get; set; }
        [DataMember(Name = "blackberry")]
        public string blackberry { get; set; }
        [DataMember(Name = "bolsaServicios")]
        public string bolsaServicios { get; set; }
        [DataMember(Name = "campana")]
        public string campana { get; set; }
        [DataMember(Name = "cargoFactDet")]
        public string cargoFactDet { get; set; }
        [DataMember(Name = "claroData")]
        public string claroData { get; set; }
        [DataMember(Name = "claroDirecto")]
        public string claroDirecto { get; set; }
        [DataMember(Name = "claroFax")]
        public string claroFax { get; set; }
        [DataMember(Name = "clarobanca")]
        public string clarobanca { get; set; }
        [DataMember(Name = "coId")]
        public string coId { get; set; }
        [DataMember(Name = "codigoWf")]
        public string codigoWf { get; set; }
        [DataMember(Name = "consultor")]
        public string consultor { get; set; }
        [DataMember(Name = "consultorRenovacion")]
        public string consultorRenovacion { get; set; }
        [DataMember(Name = "costoRpv")]
        public string costoRpv { get; set; }
        [DataMember(Name = "costoRpvNac")]
        public string costoRpvNac { get; set; }
        [DataMember(Name = "ctaPersRecarga")]
        public string ctaPersRecarga { get; set; }
        [DataMember(Name = "custcode")]
        public string custcode { get; set; }
        [DataMember(Name = "estado")]
        public string estado { get; set; }
        [DataMember(Name = "fBlackberry")]
        public string fBlackberry { get; set; }
        [DataMember(Name = "fCampana")]
        public string fCampana { get; set; }
        [DataMember(Name = "fCargoFactDet")]
        public string fCargoFactDet { get; set; }
        [DataMember(Name = "fClaroData")]
        public string fClaroData { get; set; }
        [DataMember(Name = "fClaroDirecto")]
        public string fClaroDirecto { get; set; }
        [DataMember(Name = "fClaroFax")]
        public string fClaroFax { get; set; }
        [DataMember(Name = "fClarobanca")]
        public string fClarobanca { get; set; }
        [DataMember(Name = "fControlConsumoAdic")]
        public string fControlConsumoAdic { get; set; }
        [DataMember(Name = "fCostoRpv")]
        public string fCostoRpv { get; set; }
        [DataMember(Name = "fCostoRpvNac")]
        public string fCostoRpvNac { get; set; }
        [DataMember(Name = "fCtaPersRecarga")]
        public string fCtaPersRecarga { get; set; }
        [DataMember(Name = "fCustomerId")]
        public string fCustomerId { get; set; }
        [DataMember(Name = "fEjecutivo")]
        public string fEjecutivo { get; set; }
        [DataMember(Name = "fFactDetModA")]
        public string fFactDetModA { get; set; }
        [DataMember(Name = "fFactDetallada")]
        public string fFactDetallada { get; set; }
        [DataMember(Name = "fGprs")]
        public string fGprs { get; set; }
        [DataMember(Name = "fHabilitacion")]
        public string fHabilitacion { get; set; }
        [DataMember(Name = "fLbs")]
        public string fLbs { get; set; }
        [DataMember(Name = "fLdi")]
        public string fLdi { get; set; }
        [DataMember(Name = "fMms")]
        public string fMms { get; set; }
        [DataMember(Name = "fNivDeHab")]
        public string fNivDeHab { get; set; }
        [DataMember(Name = "fPaqueteData")]
        public string fPaqueteData { get; set; }
        [DataMember(Name = "fPaqueteSms")]
        public string fPaqueteSms { get; set; }
        [DataMember(Name = "fPrestamo")]
        public string fPrestamo { get; set; }
        [DataMember(Name = "fReposicion")]
        public string fReposicion { get; set; }
        [DataMember(Name = "fRoamInt")]
        public string fRoamInt { get; set; }
        [DataMember(Name = "fRpceIlimitado")]
        public string fRpceIlimitado { get; set; }
        [DataMember(Name = "fRpv")]
        public string fRpv { get; set; }
        [DataMember(Name = "fRtp")]
        public string fRtp { get; set; }
        [DataMember(Name = "fSeguro")]
        public string fSeguro { get; set; }
        [DataMember(Name = "fSms")]
        public string fSms { get; set; }
        [DataMember(Name = "fSmsMail")]
        public string fSmsMail { get; set; }
        [DataMember(Name = "fSolsms")]
        public string fSolsms { get; set; }
        [DataMember(Name = "fTimData")]
        public string fTimData { get; set; }
        [DataMember(Name = "fTimFax")]
        public string fTimFax { get; set; }
        [DataMember(Name = "fTimProConnexion")]
        public string fTimProConnexion { get; set; }
        [DataMember(Name = "factDetModA")]
        public string factDetModA { get; set; }
        [DataMember(Name = "factDetallada")]
        public string factDetallada { get; set; }
        [DataMember(Name = "fechaActiv")]
        public string fechaActiv { get; set; }
        [DataMember(Name = "fechaActivRenov")]
        public string fechaActivRenov { get; set; }
        [DataMember(Name = "fechaEquipoRenov")]
        public string fechaEquipoRenov { get; set; }
        [DataMember(Name = "gprs")]
        public string gprs { get; set; }
        [DataMember(Name = "habilitacion")]
        public string habilitacion { get; set; }
        [DataMember(Name = "lbs")]
        public string lbs { get; set; }
        [DataMember(Name = "ldi")]
        public string ldi { get; set; }
        [DataMember(Name = "mms")]
        public string mms { get; set; }
        [DataMember(Name = "motivo")]
        public string motivo { get; set; }
        [DataMember(Name = "nichoId")]
        public string nichoId { get; set; }
        [DataMember(Name = "nivDeHab")]
        public string nivDeHab { get; set; }
        [DataMember(Name = "paqueteData")]
        public string paqueteData { get; set; }
        [DataMember(Name = "paqueteSms")]
        public string paqueteSms { get; set; }
        [DataMember(Name = "plan")]
        public string plan { get; set; }
        [DataMember(Name = "prestamo")]
        public string prestamo { get; set; }
        [DataMember(Name = "reposicion")]
        public string reposicion { get; set; }
        [DataMember(Name = "roamInt")]
        public string roamInt { get; set; }
        [DataMember(Name = "rpceIlimitado")]
        public string rpceIlimitado { get; set; }
        [DataMember(Name = "rpv")]
        public string rpv { get; set; }
        [DataMember(Name = "rtp")]
        public string rtp { get; set; }
        [DataMember(Name = "seguro")]
        public string seguro { get; set; }
        [DataMember(Name = "simCard")]
        public string simCard { get; set; }
        [DataMember(Name = "sms")]
        public string sms { get; set; }
        [DataMember(Name = "smsMail")]
        public string smsMail { get; set; }
        [DataMember(Name = "solsms")]
        public string solsms { get; set; }
        [DataMember(Name = "telefono")]
        public string telefono { get; set; }
        [DataMember(Name = "timData")]
        public string timData { get; set; }
        [DataMember(Name = "timFax")]
        public string timFax { get; set; }
        [DataMember(Name = "timProConnexion")]
        public string timProConnexion { get; set; }
        [DataMember(Name = "topeBolsaExacto")]
        public string topeBolsaExacto { get; set; }
        [DataMember(Name = "topeSolesAdic")]
        public string topeSolesAdic { get; set; }
    }
}
