using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPetitions
{
    [DataContract(Name = "PetitionstRequestPostPaid")]
    public class PetitionRequest : Claro.Entity.Request
    {

        [DataMember]
        public string ContractId { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string PetitionType { get; set; }
        [DataMember]
        public string flagConvivencia { get; set; }
        [DataMember]
        public string flagPlataforma { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string migrado { get; set; }
        [DataMember]
        public string Application { get; set; }
    }
}
