using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPetitions
{
    [DataContract(Name = "PetitionResponsePostPaid")]
    public class PetitionResponse
    {
        [DataMember]
        public List<Petitions> Petitions { get; set; }
    }
}
