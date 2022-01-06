using Claro.Helpers;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class LoanRentalModel : IExcel
    {
        [Header(Title = "DateIni")]
        public string DateIni { get; set; }
        [Header(Title = "DateFin")]
        public string DateFin { get; set; }
        [Header(Title = "Type")]
        public string Type { get; set; }
        [Header(Title = "Number")]
        public string Number { get; set; }

        public List<LoandRentalType> listLoanRentalType { get; set; }
        [Header(Title = "listLoanRental")]
        public List<LoanRental> listLoanRental { get; set; }
        [Header(Title = "listLoanRentalNoClarify")]
        public List<LoanRentalNoClarify> listLoanRentalNoClarify { get; set; }

        
    }
}