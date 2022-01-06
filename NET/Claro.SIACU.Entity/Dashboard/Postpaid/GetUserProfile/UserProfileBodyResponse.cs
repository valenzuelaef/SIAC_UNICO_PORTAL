using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile
{
    public class UserProfileBodyResponse
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "errorCode")]
        public string errorCode { get; set; }

        [DataMember(Name = "errorMsg")]
        public string errorMsg { get; set; }

        [DataMember(Name = "cursorSeguridad")]
        public IList<CursorSeguridad> cursorSeguridad { get; set; }
    }

    public class CursorSeguridad
    {
        [DataMember(Name = "UsuacCod")]
        public string UsuacCod { get; set; }

        [DataMember(Name = "PerfcCod")]
        public string PerfcCod { get; set; }

        [DataMember(Name = "UsuacCodVenSap")]
        public string UsuacCodVenSap { get; set; }
    }
}
