using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHelper
{
    public class AccountRelationPlan : IExcel
    {
        [Header(Title = "IT", Order = Claro.Constants.NumberZero)]
        public int IT { get; set; }
        [Header(Title = "Teléfono", Order = Claro.Constants.NumberOne)]
        public string Telephone { get; set; }
        [Header(Title = "Sim Card", Order = Claro.Constants.NumberTwo)]
        public string SIMCard { get; set; }
        [Header(Title = "NichoID", Order = Claro.Constants.NumberThree)]
        public string NichoID { get; set; }
        [Header(Title = "Plan", Order = Claro.Constants.NumberFour)]
        public string DescriptionPlan { get; set; }
        [Header(Title = "Cargo Fijo", Order = Claro.Constants.NumberFive)]
        public string FixedCharge { get; set; }
        [Header(Title = "Fecha Activ.", Order = Claro.Constants.NumberSix)]
        public string ActivationDate { get; set; }
        [Header(Title = "LDI", Order = Claro.Constants.NumberSeven, Group = "Servicios")]
        public string LDI { get; set; }
        [Header(Title = "Roaming Int.", Order = Claro.Constants.NumberEight, Group = "Servicios")]
        public string InternationalRoaming { get; set; }
        [Header(Title = "ClaroData", Order = Claro.Constants.NumberNine, Group = "Servicios")]
        public string TimData { get; set; }
        [Header(Title = "ClaroFax", Order = Claro.Constants.NumberTen, Group = "Servicios")]
        public string TimFax { get; set; }
        [Header(Title = "Paquete SMS", Order = Claro.Constants.NumberEleven, Group = "Servicios")]
        public string SMSPackage { get; set; }
        [Header(Title = "Paquete Data", Order = Claro.Constants.NumberTwelve, Group = "Servicios")]
        public string DataPackage { get; set; }
        [Header(Title = "Niv.de Habilt.", Order = Claro.Constants.NumberThirteen, Group = "Servicios")]
        public string HabilitacionLevel { get; set; }
        [Header(Title = "Tarifa Preferencial RPCe", Order = Claro.Constants.NumberFourteen, Group = "Servicios")]
        public string PreferentialRateRPC { get; set; }
        [Header(Title = "Costo por RPCe", Order = Claro.Constants.NumberFifteen, Group = "Servicios")]
        public string CostXRPCe { get; set; }
        [Header(Title = "Costo RPCe Nacional", Order = Claro.Constants.NumberSixteen, Group = "Servicios")]
        public string CostRPCUnlimited { get; set; }
        [Header(Title = "Seguro", Order = Claro.Constants.NumberSeventeen, Group = "Servicios")]
        public string Insurance { get; set; }
        [Header(Title = "Alquiler/Prestamo", Order = Claro.Constants.NumberEighteen, Group = "Servicios")]
        public string Loan { get; set; }
        [Header(Title = "Claroconnection", Order = Claro.Constants.NumberNineteen, Group = "Servicios")]
        public string TimProConnection { get; set; }
        [Header(Title = "GPRS", Order = Claro.Constants.NumberTwenty, Group = "Servicios")]
        public string GPRS { get; set; }
        [Header(Title = "LBS", Order = Claro.Constants.NumberTwentyOne, Group = "Servicios")]
        public string LBS { get; set; }
        [Header(Title = "Soluciones SMS", Order = Claro.Constants.NumberTwentyTwo, Group = "Servicios")]
        public string SolSMS { get; set; }
        [Header(Title = "RPM", Order = Claro.Constants.NumberTwentyThree, Group = "Servicios")]
        public string RTP { get; set; }
        [Header(Title = "Habilitación", Order = Claro.Constants.NumberTwentyFour, Group = "Servicios")]
        public string Habilitation { get; set; }
        [Header(Title = "Sms Mail", Order = Claro.Constants.NumberTwentyFive, Group = "Servicios")]
        public string SMSMail { get; set; }
        [Header(Title = "RPCe ilimitado", Order = Claro.Constants.NumberTwentySix, Group = "Servicios")]
        public string RPCUnlimited { get; set; }
        [Header(Title = "Cuenta Personal Recarga", Order = Claro.Constants.NumberTwentySeven, Group = "Servicios")]
        public string RechargePersonalAccount { get; set; }
        [Header(Title = "SMS Send", Order = Claro.Constants.NumberTwentyEight, Group = "Servicios")]
        public string SMSSending { get; set; }
        [Header(Title = "GPRS MMS", Order = Claro.Constants.NumberTwentyNine, Group = "Servicios")]
        public string GPRSMMS { get; set; }
        [Header(Title = "Blackberry", Order = Claro.Constants.NumberThirty, Group = "Servicios")]
        public string BlackBerry { get; set; }
        [Header(Title = "Servicio SERC", Order = Claro.Constants.NumberThirtyOne, Group = "Servicios")]
        public string Reposicion { get; set; }
        [Header(Title = "Tope Soles Adicionales", Order = Claro.Constants.NumberThirtyTwo, Group = "Servicios")]
        public string AdditionalTopBag { get; set; }
        [Header(Title = "Tope Bolsa / Exacto", Order = Claro.Constants.NumberThirtyThree, Group = "Servicios")]
        public string ExactlyTopBag { get; set; }
        [Header(Title = "Factura Detallada", Order = Claro.Constants.NumberThirtyFour, Group = "Servicios")]
        public string DetailedBilling { get; set; }
        [Header(Title = "Fact. det. Mod A.", Order = Claro.Constants.NumberThirtyFive, Group = "Servicios")]
        public string DetailedBillingModA { get; set; }
        [Header(Title = "Claro Directo", Order = Claro.Constants.NumberThirtySix, Group = "Servicios")]
        public string ClaroDirect { get; set; }
        [Header(Title = "Cargo fact detallada", Order = Claro.Constants.NumberThirtySeven, Group = "Servicios")]
        public string DetailedBillingCharge { get; set; }
        [Header(Title = "Claro Fax", Order = Claro.Constants.NumberThirtyEight, Group = "Servicios")]
        public string ClaroFax { get; set; }
        [Header(Title = "Claro Data", Order = Claro.Constants.NumberThirtyNine, Group = "Servicios")]
        public string ClaroData { get; set; }
        [Header(Title = "Claro Banca", Order = Claro.Constants.NumberForty, Group = "Servicios")]
        public string Clarobanking { get; set; }
        [Header(Title = "Bolsa Servicios", Order = Claro.Constants.NumberFortyOne)]
        public string BagService { get; set; }
        [Header(Title = "Con o Sin Equipo", Order = Claro.Constants.NumberFortyTwo)]
        public string WithNoEquipment { get; set; }
        [Header(Title = "Consultor Business", Order = Claro.Constants.NumberFortyThree)]
        public string BusinessConsultant { get; set; }
        [Header(Title = "Código WF", Order = Claro.Constants.NumberFortyFour)]
        public string WFCode { get; set; }
        [Header(Title = "Estado", Order = Claro.Constants.NumberFortyFive)]
        public string State { get; set; }
        [Header(Title = "Motivo", Order = Claro.Constants.NumberFortySix)]
        public string Reason { get; set; }
        [Header(Title = "Consultor Renovación", Order = Claro.Constants.NumberFortySeven)]
        public string ConsultanRenovating { get; set; }
        [Header(Title = "Fecha Entrega Equipo Renovación", Order = Claro.Constants.NumberFortyEight)]
        public string DeliveryDateRenewalTeam { get; set; }
        [Header(Title = "Fecha Activación Renovación", Order = Claro.Constants.NumberFortyNine)]
        public string RenovatingActivationDate { get; set; }
        public int DynamicRows { get; set; }
        public string F_LDI { get; set; }
        public string F_InternationalRoaming { get; set; }
        public string F_TimData { get; set; }
        public string F_TimFax { get; set; }
        public string F_SMSPackage { get; set; }
        public string F_DataPackage { get; set; }
        public string F_LevelEnablement { get; set; }
        public string F_RPV { get; set; }
        public string F_CostRPV { get; set; }
        public string F_CostRPCUnlimited { get; set; }
        public string F_Insurance { get; set; }
        public string F_Loan { get; set; }
        public string F_TimProConnection { get; set; }
        public string F_GPRS { get; set; }
        public string F_LBS { get; set; }
        public string F_SolSMS { get; set; }
        public string F_RTP { get; set; }
        public string F_Habilitation { get; set; }
        public string F_WithNoEquipment { get; set; }
        public string F_Executive { get; set; }
        public string F_SMSMail { get; set; }
        public string F_RPCUnlimited { get; set; }
        public string F_DetailedBilling { get; set; }
        public string F_DetailedBillingModA { get; set; }
        public string F_ClaroDirect { get; set; }
        public string F_DetailedBillingCharge { get; set; }
        public string F_ClaroFax { get; set; }
        public string F_ClaroData { get; set; }
        public string F_AdditionalConsumptionControl { get; set; }
        public string F_RechargePersonalAccount { get; set; }
        public string F_SMS { get; set; }
        public string F_MMS { get; set; }
        public string F_Blackberry { get; set; }
        public string F_Reposicion { get; set; }
        public string F_Clarobanking { get; set; }
    }
}