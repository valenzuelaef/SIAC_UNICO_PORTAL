using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountAnnotation;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{
    public class AccountAnnotationModel
    {

        public AccountAnnotationModel()
        {
            lstTypeAnnotation = new List<TypeAnnotation>();
            listAnnotationPast = new List<Annotation>();
        }

        public List<TypeAnnotation> lstTypeAnnotation { get; set; }
        public List<Annotation> listAnnotationPast { get; set; }
        public string Transaction { get; set; }
        public string Type { get; set; }

    }
}