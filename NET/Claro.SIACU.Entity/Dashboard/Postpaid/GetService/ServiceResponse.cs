using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetService
{
    [DataContract(Name = "ServiceResponsePostPaid")]
    public class ServiceResponse
    {  
         [DataMember]
         public Entity.Dashboard.Postpaid.Service ObjService { get; set; }

         [DataMember]
         public List<Entity.Dashboard.Postpaid.Service> ListService { get; set; } 
    }
}
