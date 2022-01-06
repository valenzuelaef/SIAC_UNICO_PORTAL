using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class ModelListaBloqueo
    {
        public string ticklerNumber { get; set; }
        public string ticklerCode { get; set; }
        public string ticklerStatus { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string longDescription { get; set; }
        public string estado { get; set; }
        public string dsc { get; set; }
    }
}