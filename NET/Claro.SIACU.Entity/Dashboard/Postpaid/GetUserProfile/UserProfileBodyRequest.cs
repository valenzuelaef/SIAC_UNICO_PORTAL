using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile
{
    public class UserProfileBodyRequest
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "ipAplicacion")]
        public string ipAplicacion { get; set; }

        [DataMember(Name = "aplicacion")]
        public string aplicacion { get; set; }

        [DataMember(Name = "usuarioLogin")]
        public string usuarioLogin { get; set; }

        [DataMember(Name = "appCod")]
        public string appCod { get; set; }
    }
}
