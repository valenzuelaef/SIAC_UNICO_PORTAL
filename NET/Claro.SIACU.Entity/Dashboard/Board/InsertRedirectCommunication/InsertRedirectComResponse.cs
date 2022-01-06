﻿using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication
{
    [DataContract(Name = "InsertRedirectComResponseDashboard")]
    public class InsertRedirectComResponse
    {
        [DataMember]
        public string ResultRegCommunication { get; set; }
        [DataMember]
        public string Sequence { get; set; }
        [DataMember]
        public string Url { get; set; }

    }
}
