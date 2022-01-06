using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DbTable("TEMPO")]
    [DataContract(Name = "CallPrePaid")]
    public class Call
    {
        [DbColumn("CallType")]
        [DataMember]
        public string CallType { get; set; }

        [DbColumn("CallDate")]
        [DataMember]
        public string CallDate { get; set; }

        [DbColumn("CallDateTime")]
        [DataMember]
        public string CallDateTime { get; set; }

        [DbColumn("NumberB")]
        [DataMember]
        public string NumberB { get; set; }

        [DbColumn("NumberB_Operator")]
        [DataMember]
        public string NumberBOperator { get; set; }

        [DbColumn("CallDuration")]
        [DataMember]
        public string CallDuration { get; set; }

        [DbColumn("AccessFrontEndId")]
        [DataMember]
        public string ChargeSoles { get; set; }

        [DbColumn("RefundErrorCause")]
        [DataMember]
        public string BalanceSoles { get; set; }

        [DbColumn("TariffPlanId_Call")]
        [DataMember]
        public string TariffPlanIdCall { get; set; }

        [DbColumn("PROMOCION")]
        [DataMember]
        public string Promocion { get; set; }

        [DbColumn("VLR_Number")]
        [DataMember]
        public string VlrNumber { get; set; }

        [DataMember]
        public string NumberaArea { get; set; }

        [DataMember]
        public string NumberbArea { get; set; }

        [DbColumn("Purpose")]
        [DataMember]
        public string Purpose { get; set; }

        [DbColumn("TRAFICOROAMING")]
        [DataMember]
        public string TraficoRoaming { get; set; }

        [DbColumn("ACC_CHARGE_VOICE1LOYALTY")]
        [DataMember]
        public string VozFidelizacionConsumo { get; set; }

        [DbColumn("ACC_BAL_VOICE1LOYALTY")]
        [DataMember]
        public string VozFidelizacionSaldo { get; set; }

        [DbColumn("ACC_CHARGE_VOICE1PROMOTIONAL")]
        [DataMember]
        public string Voice1Consumo { get; set; }

        [DbColumn("ACC_BAL_VOICE1PROMOTIONAL")]
        [DataMember]
        public string Voice1Saldo { get; set; }

        [DbColumn("ACC_CHARGE_SMSLOYALTY")]
        [DataMember]
        public string SMSFidelizacionConsumo { get; set; }

        [DbColumn("ACC_BAL_SMSLOYALTY")]
        [DataMember]
        public string SMSFidelizacionSaldo { get; set; }

        [DbColumn("ACC_CHARGE_GPRSPROMOTIONAL")]
        [DataMember]
        public string GPRSConsumo { get; set; }

        [DbColumn("ACC_BAL_GPRSPROMOTIONAL")]
        [DataMember]
        public string GPRSSaldo { get; set; }

        [DbColumn("ACC_CHARGE_MMSPROMOTIONAL")]
        [DataMember]
        public string MMSConsumo { get; set; }

        [DbColumn("ACC_BAL_MMSPROMOTIONAL")]
        [DataMember]
        public string MMSSaldo { get; set; }

        [DataMember]
        public string SolesConsumo { get; set; }

        [DataMember]
        public string SolesSaldo { get; set; }

        [DbColumn("ACC_CHARGE_SMSPROMOTIONAL")]
        [DataMember]
        public string SMSConsumo { get; set; }

        [DbColumn("ACC_BAL_SMSPROMOTIONAL")]
        [DataMember]
        public string SMSSaldo { get; set; }

        [DbColumn("ACC_CHARGE_VOICE2PROMOTIONAL")]
        [DataMember]
        public string Voice2Consumo { get; set; }

        [DbColumn("ACC_BAL_VOICE2PROMOTIONAL")]
        [DataMember]
        public string Voice2Saldo { get; set; }

        [DbColumn("ACC_CHARGE_GPRSLOYALTY")]
        [DataMember]
        public string Promo1Consumo { get; set; }

        [DbColumn("ACC_BAL_GPRSLOYALTY")]
        [DataMember]
        public string Promo1Saldo { get; set; }

        [DbColumn("ACC_CHARGE_MMSLOYALTY")]
        [DataMember]
        public string Promo2Consumo { get; set; }

        [DbColumn("ACC_BAL_MMSLOYALTY")]
        [DataMember]
        public string Promo2Saldo { get; set; }

        [DataMember]
        public string NumberA { get; set; }

        [DbColumn("OFFER")]
        [DataMember]
        public string Offer { get; set; }

        [DbColumn("ACC_CHARGE_BONUSPROMOTIONAL")]
        [DataMember]
        public string PromoConsumo { get; set; }

        [DbColumn("ACC_BAL_BONUSPROMOTIONAL")]
        [DataMember]
        public string PromoSaldo { get; set; }

        [DbColumn("ZONADETARIFA")]
        [DataMember]
        public string ZoneTariff { get; set; }

        [DataMember]
        public string CallTelephoneDestination { get; set; }

        [DataMember]
        public string CallTypeTraffic { get; set; }

        [DataMember]
        public string CallUptake { get; set; }

        [DataMember]
        public string CallBoughtPresented { get; set; }

        [DataMember]
        public string CallBalance { get; set; }

        [DataMember]
        public string CallBagId { get; set; }

        [DataMember]
        public string CallDescription { get; set; }

        [DataMember]
        public string CallPlan { get; set; }

        [DataMember]
        public string CallPromotion { get; set; }

        [DataMember]
        public string CallDestination { get; set; }

        [DataMember]
        public string CallOperator { get; set; }

        [DataMember]
        public string CallCobroGroup { get; set; }

        [DataMember]
        public string CallNetworkType { get; set; }

        [DataMember]
        public string CallImei { get; set; }

        [DataMember]
        public string CallRoaming { get; set; }

        [DataMember]
        public string CallTariffArea { get; set; }

        [DataMember]
        public string CallCost { get; set; }

        [DataMember]
        public string CallStart { get; set; }

        [DataMember]
        public string CallEnd { get; set; }

        [DataMember]
        public string CallBag { get; set; }

        [DataMember]
        public string CallEventType { get; set; }

    }
}
