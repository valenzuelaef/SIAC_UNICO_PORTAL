using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class ListGetSolicitude
    {
        private List<GetSolicitude> _GetSolicitude;

        [DataMember(Name = "obtenerSolicitudes")]
        public List<GetSolicitude> GetSolicitude
        {
            set
            {
                if (value == null)
                {
                    _GetSolicitude = new List<GetSolicitude>();
                }
                else
                {
                    _GetSolicitude = value;
                }
            }
            get { return _GetSolicitude; }
        }
    }
}
