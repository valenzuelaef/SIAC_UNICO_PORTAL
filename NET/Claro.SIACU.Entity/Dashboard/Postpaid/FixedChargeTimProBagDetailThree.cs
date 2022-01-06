﻿using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "FixedChargeTimProBagDetailThreePostPaid")]
    public class FixedChargeTimProBagDetailThree
    {
        [Data.DbColumn("Msisdn")]
        [DataMember]
        public string Msisdn { get; set; }

       
        [DataMember]
        public string Description { get; set; }

        [Data.DbColumn("FromDate")]
        [DataMember]
        public string FromDate { get; set; }

        [Data.DbColumn("ToDate")]
        [DataMember]
        public string ToDate { get; set; }

       
        [DataMember]
        public decimal Amount { get; set; }

       
        [DataMember]
        public string RatePlan { get; set; }

        [Data.DbColumn("Fu1")]
        [DataMember]
        public string Fu1 { get; set; }

        [Data.DbColumn("Fu2")]
        [DataMember]
        public string Fu2 { get; set; }

        [Data.DbColumn("Fu3")]
        [DataMember]
        public string Fu3 { get; set; }

      
        [DataMember]
        public string Fu4 { get; set; }

    
        [DataMember]
        public int MsgTexto2 { get; set; }

      
        [DataMember]
        public int MsgTexto4 { get; set; }

       
        [DataMember]
        public int MsgTexto5 { get; set; }

    
        [DataMember]
        public int Quantity { get; set; }


       
    }
}
