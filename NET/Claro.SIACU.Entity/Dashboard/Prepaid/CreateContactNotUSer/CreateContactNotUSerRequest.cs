using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.CreateContactNotUSer
{
    [DataContract(Name = "CreateContactNotUSerRequestPrePaid")]
    public class CreateContactNotUSerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Modality { get; set; }
        [DataMember]
        public string TypeProcess { get; set; }
        [DataMember]
        public string TelephoneCustomer { get; set; }
        [DataMember]
        public string Registered { get; set; }
        [DataMember]
        public string MotiveRegister { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string DateBirth { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public string TypeDocument { get; set; }
        [DataMember]
        public string NumberDocument { get; set; }
        [DataMember]
        public string StateCivil { get; set; }
        [DataMember]
        public string Occupation { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public string TelephoneReference { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string Urbanization { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public string ConfirmMail { get; set; }
    }
}
