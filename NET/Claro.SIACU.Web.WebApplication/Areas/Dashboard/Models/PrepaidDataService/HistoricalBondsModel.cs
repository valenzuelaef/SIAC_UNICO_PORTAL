using Claro.Helpers;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class HistoricalBondsModel : IExcel
    {
       [Header(Title = "Id Transaccion", Order = Claro.Constants.NumberZero)]
        public string IdTransaction { get; set; }
        [Header(Title = "Transaccion", Order = Claro.Constants.NumberOne)]
        public string Transaction { get; set; }
        [Header(Title = "Promocion", Order = Claro.Constants.NumberTwo)]
        public string Promotion { get; set; }
        [Header(Title = "Fecha de Reegistro", Order = Claro.Constants.NumberThree)]
        public string RegistrationDate { get; set; }
        [Header(Title = "Aplicativo", Order = Claro.Constants.NumberFour)]
        public string Applicative { get; set; }
        public int Accountant { get; set; }


        
    }
}