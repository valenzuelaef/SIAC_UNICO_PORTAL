using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.GetHistoricalPackage
{
    public class HistoricalPackageHelper
    {

        public List<PackageODCSHelper> lstPackageODCS { get; set; }

        public string TotalMBCicle { get; set; }

        public string TotalRows { get; set; }

        public string strMensaje { get; set; }

        public bool IsVisibleMensaje { get; set; }

        public string strMensajeAlert { get; set; }

        public string FlagOne { get; set; }

        
    }
}