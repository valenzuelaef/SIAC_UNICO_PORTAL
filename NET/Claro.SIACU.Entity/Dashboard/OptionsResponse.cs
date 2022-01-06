using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard
{
    public class OptionsResponse
    {
        public System.Collections.Generic.List<Option> menu { get; set; }
        public System.Collections.Generic.List<Option> options { get; set; }
        public System.Collections.Generic.List<Option> toolbar { get; set; }
    }
}
