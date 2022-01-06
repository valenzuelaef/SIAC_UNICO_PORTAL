using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class ResponseDataGetSolicitude
    {
        [DataMember(Name = "resultado")]
        public string Result { get; set; }
        [DataMember(Name = "mensaje")]
        public string Message { get; set; }

        private ListGetSolicitude _listGetSolicitude;

        [DataMember(Name = "listaObtenerSolicitudes")]
        public ListGetSolicitude ListGetSolicitude
        {
            set
            {
                if (value == null)
                {
                    _listGetSolicitude = new ListGetSolicitude();
                }
                else
                {
                    _listGetSolicitude = value;
                }
            }
            get { return _listGetSolicitude; }
        }
    }
}
