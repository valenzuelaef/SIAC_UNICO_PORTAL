using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard
{
    public class Option
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public System.Collections.Generic.List<Option> items { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int order { get; set; }
        public bool isDefault { get; set; }
        public bool isExternal { get; set; }
        public string optionType { get; set; }
    }
}
