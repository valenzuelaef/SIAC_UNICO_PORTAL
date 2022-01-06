
using Claro.Helpers;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.ExportExcelCallHelper
{
    public class ExportExcelCall
    {

        public ExportExcelCall() { }

        [Header(Title = "Voz Fid Saldo", Order = Claro.Constants.NumberTwelve)]
        public string VoiceFidelizarBalance { get; set; }

        [Header(Title = "Voz Fid Costo", Order = Claro.Constants.NumberThirteen)]
        public string VoiceFidelizarConsummation { get; set; }

        [Header(Title = "Voice 1 Saldo", Order = Claro.Constants.NumberFourteen)]
        public string Voice1Balance { get; set; }

        [Header(Title = "Voice 1 Costo", Order = Claro.Constants.NumberFifteen)]
        public string Voice1Consummation { get; set; }

        [Header(Title = "SMS Fid Saldo", Order = Claro.Constants.NumberSixteen)]
        public string SMSFidelizarBalance { get; set; }

        [Header(Title = "SMS Fid Costo", Order = Claro.Constants.NumberSeventeen)]
        public string SMSFidelizarConsummation { get; set; }

        [Header(Title = "MMS Fid Saldo", Order = Claro.Constants.NumberEighteen)]
        public string MMSBalance { get; set; }

        [Header(Title = "MMS Fid Costo", Order = Claro.Constants.NumberNineteen)]
        public string MMSConsummation { get; set; }

        [Header(Title = "GPRS Saldo", Order = Claro.Constants.NumberTwenty)]
        public string GPRSBalance { get; set; }

        [Header(Title = "GPRS Costo", Order = Claro.Constants.NumberTwentyOne)]
        public string GPRSConsummation { get; set; }

        [Header(Title = "Promo Saldo (S/.)", Order = Claro.Constants.NumberTwentyTwo)]
        public string PromotionBalance { get; set; }

        [Header(Title = "Promo Costo (S/.)", Order = Claro.Constants.NumberTwentyThree)]
        public string PromotionConsummation { get; set; }
        [Header(Title = "SMS Saldo", Order = Claro.Constants.NumberTwentyFour)]
        public string SMSBalance { get; set; }
        [Header(Title = "SMS Costo", Order = Claro.Constants.NumberTwentyFive)] //
        public string SMSConsummation { get; set; }
        [Header(Title = "Voice 2 Saldo", Order = Claro.Constants.NumberTwentySix)]
        public string Voice2Balance { get; set; }
        [Header(Title = "Voice 2 Costo", Order = Claro.Constants.NumberTwentySeven)]
        public string Voice2Consummation { get; set; }
        [Header(Title = "Promo 1 Saldo (S/.)", Order = Claro.Constants.NumberTwentyEight)]
        public string Promotion1Balance { get; set; }
        [Header(Title = "Promo 1 Costo (S/.)", Order = Claro.Constants.NumberTwentyNine)]
        public string Promotion1Consummation { get; set; }
        [Header(Title = "Promo 2 Saldo (S/.)", Order = Claro.Constants.NumberThirty)]
        public string Promotion2Balance { get; set; }
        [Header(Title = "Promo 2 Costo (S/.)", Order = Claro.Constants.NumberThirtyOne)]
        public string Promotion2Consummation { get; set; }
        [Header(Title = "Zona de Tarifa", Order = Claro.Constants.NumberThirtyThree)]
        public string ZoneTariff { get; set; }


        [Header(Title = "Fecha", Order = Claro.Constants.NumberZero)]
        public string Date { get; set; }
        [Header(Title = "Hora", Order = Claro.Constants.NumberOne)]
        public string Hour { get; set; }
        [Header(Title = "Telefono Destino", Order = Claro.Constants.NumberTwo)]
        public string PhoneDestination { get; set; }
        [Header(Title = "Consumo", Order = Claro.Constants.NumberThree)]
        public string Consummation { get; set; }
        [Header(Title = "Costo (S/.)", Order = Claro.Constants.NumberFour)]
        public string Cost { get; set; }
        [Header(Title = "Saldo (S/.)", Order = Claro.Constants.NumberFive)]
        public string Balance { get; set; }
        [Header(Title = "Plan", Order = Claro.Constants.NumberSix)]
        public string Plan { get; set; }
        [Header(Title = "Promocion", Order = Claro.Constants.NumberSeven)]
        public string Promotion { get; set; }
        [Header(Title = "Tipo", Order = Claro.Constants.NumberEight)]
        public string Type { get; set; }
        [Header(Title = "Destino", Order = Claro.Constants.NumberNine)]
        public string Destination { get; set; }
        [Header(Title = "Operador", Order = Claro.Constants.NumberTen)]
        public string Operator { get; set; }
        [Header(Title = "Horario", Order = Claro.Constants.NumberEleven)]
        public string Schedule { get; set; }

        [Header(Title = "Roaming", Order = Claro.Constants.NumberThirtyTwo)]
        public string Roaming { get; set; }
    }
}