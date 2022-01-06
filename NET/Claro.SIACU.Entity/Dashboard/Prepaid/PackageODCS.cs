using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "PackageODCSPrePaid")]
    public class PackageODCS
    {
        [DataMember]
        public string ConvergedCode { get; set; }
        [DataMember]
        public string PackageCode { get; set; }
        [DataMember]
        public string DescriptionPackage { get; set; }
        [DataMember]
        public string ActivationDate { get; set; }
        [DataMember]
        public string ExpirationDate { get; set; }
        [DataMember]
        public string Channel { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string MBConsumed { get; set; }
        [DataMember]
        public string MBAvailable { get; set; }
        [DataMember]
        public string MBTotal { get; set; }
        [DataMember]
        public string RatingGroup { get; set; }
        [DataMember]
        public string Zone { get; set; }
        [DataMember]
        public string TypePurchase { get; set; }
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string LineTypeId { get; set; }
        [DataMember]
        public string FamilyPackage { get; set; }
        [DataMember]
        public string AcquisitionDate{ get; set; }
        [DataMember]
        public string IdPurchase { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string DatePost { get; set; }
        [DataMember]
        public string mbincDES { get; set; }
        [DataMember]
        public string validity { get; set; }
        [DataMember]
        public string NameBag { get; set; }
        [DataMember]
        public string ExpirationDatePost { get; set; }
        [DataMember]
        public string cost { get; set; }
        [DataMember]
        public string TypePackage { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }

        //Agregado
        [DataMember]
        public string TypeValidity { get; set; }
        [DataMember]
        public string TypeBalance { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        
    }
}
