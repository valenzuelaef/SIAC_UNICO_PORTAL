using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUsageThresholdsCounter
{
    [XmlRoot(ElementName = "value")]
    public class Value2_Request
    {
        [XmlElement(ElementName = "string")]
        public string @string { get; set; }
        [XmlElement(ElementName = "dateTime.iso8601")]
        public string dateTime_iso8601 { get; set; }
    }

    [XmlRoot(ElementName = "member")]
    public class Member_Request
    {
        [XmlElement(ElementName = "name")]
        public string name { get; set; }
        [XmlElement(ElementName = "value")]
        public Value2_Request value { get; set; }
    }

    [XmlRoot(ElementName = "struct")]
    public class Struct_Request
    {
        [XmlElement(ElementName = "member")]
        public List<Member_Request> member { get; set; }
    }

    [XmlRoot(ElementName = "value")]
    public class Value_Request
    {
        [XmlElement(ElementName = "struct")]
        public Struct_Request @struct { get; set; }
    }

    [XmlRoot(ElementName = "param")]
    public class Param_Request
    {
        [XmlElement(ElementName = "value")]
        public Value_Request value { get; set; }
    }

    [XmlRoot(ElementName = "params")]
    public class Params_Request
    {
        [XmlElement(ElementName = "param")]
        public Param_Request param { get; set; }
    }

    [XmlRoot(ElementName = "methodCall")]
    public class MethodCall_Request 
    {
        [XmlElement(ElementName = "methodName")]
        public string methodName { get; set; }
        [XmlElement(ElementName = "params")]
        public Params_Request @params { get; set; }
    }

    public class RootObject_Request
    {
        public MethodCall_Request methodCall { get; set; }
    }
}
