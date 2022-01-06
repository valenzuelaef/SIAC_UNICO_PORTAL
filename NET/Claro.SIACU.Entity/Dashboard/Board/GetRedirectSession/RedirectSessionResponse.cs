﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession
{
    [DataContract(Name = "RedirectSessionResponseDashboard")]
    public class RedirectSessionResponse
    {
        [DataMember]
        public List<Redirect> listRedirect { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public string CodeError { get; set; }
    }
}
