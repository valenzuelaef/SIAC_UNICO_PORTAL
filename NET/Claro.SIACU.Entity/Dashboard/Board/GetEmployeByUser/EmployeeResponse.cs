using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Board.GetEmployeByUser
{
    [DataContract(Name = "EmployeeResponse")]
    public class EmployeeResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Employee> lstEmployee { set; get; }
    }
}
