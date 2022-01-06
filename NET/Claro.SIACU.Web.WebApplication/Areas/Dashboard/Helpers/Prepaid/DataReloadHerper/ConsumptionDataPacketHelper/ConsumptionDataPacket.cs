using Claro.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper.ConsumptionDataPacketHelper
{
    public class ConsumptionDataPacket
    {
        public string ConvergedCode { get; set; }

        public string PackageCode { get; set; }

        [Header(Title = "Paquete", Order = Claro.Constants.NumberZero)]
        public string DescriptionPackage { get; set; }

        [Header(Title = "Fecha de Activación", Order = Claro.Constants.NumberZero)]
        public string ActivationDate { get; set; }

        [Header(Title = "Fecha de Expiración", Order = Claro.Constants.NumberZero)]
        public string ExpirationDate { get; set; }

        [Header(Title = "MB Disponibles", Order = Claro.Constants.NumberZero)]
        public string MBAvailable { get; set; }

        [Header(Title = "MB Otorgados", Order = Claro.Constants.NumberZero)]
        public string MBGranted { get; set; }

        [Header(Title = "MB Consumidos", Order = Claro.Constants.NumberZero)]
        public string MBConsumed { get; set; }

        [Header(Title = "Estado", Order = Claro.Constants.NumberZero)]
        public string State { get; set; }

        public string Channel { get; set; }

        public string Price { get; set; }

        public string MBTotal { get; set; }

        public string RatingGroup { get; set; }

        public string Zone { get; set; }

        public string TypePurchase { get; set; }

        public string Msisdn { get; set; }

        public string LineTypeId { get; set; }

        public string FamilyPackage { get; set; }

    }
}