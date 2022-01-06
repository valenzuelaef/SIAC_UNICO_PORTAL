using System.Collections.Generic;
using System.Xml.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUsageThresholdsCounter
{
    [XmlRoot(ElementName = "data")]
    public class Data_Response
    {
        [XmlElement(ElementName = "value")]
        public List<Value_Response> value { get; set; }
        
    }

    [XmlRoot(ElementName = "array")]
    public class Array_Response
    {
        [XmlElement(ElementName = "data")]
        public Data_Response data { get; set; }
    }

    [XmlRoot(ElementName = "member")]
    public class Member_Response
    {
        [XmlElement(ElementName = "name")]
        public string name { get; set; }
        
        [XmlElement(ElementName = "value")]
        public Value_Response value { get; set; }
    }

     [XmlRoot(ElementName = "struct")]
    public class Struct_Response
    {
          [XmlElement(ElementName = "member")]
         public List<Member_Response> member { get; set; }
    }

    [XmlRoot(ElementName = "value")]
     public class Value_Response
    {
        [XmlElement(ElementName = "array")]
        public Array_Response array { get; set; }
        [XmlElement(ElementName = "string")]
        public string @string { get; set; }
        [XmlElement(ElementName = "i4")]
        public string i4 { get; set; }
        [XmlElement(ElementName="struct")]
        public Struct_Response @struct { get; set; }
    }

    [XmlRoot(ElementName = "param")]
    public class Param_Response
    {
        [XmlElement(ElementName = "value")]
        public Value_Response value { get; set; }
    }

    [XmlRoot(ElementName = "params")]
    public class Params_Response
    {
        [XmlElement(ElementName = "param")]
        public Param_Response param { get; set; }
    }

    [XmlRoot(ElementName = "methodResponse")]
    public class Method_Response
    {
        [XmlElement(ElementName = "params")]
        public Params_Response @params { get; set; }
    }

    public class RootObject_Response
    {
        public Method_Response methodResponse { get; set; }
    }

}
