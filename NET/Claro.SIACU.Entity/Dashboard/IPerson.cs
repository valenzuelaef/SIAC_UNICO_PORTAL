namespace Claro.SIACU.Entity.Dashboard
{
    public interface IPerson
    {
        string NOMBRES { get; set; }
        string APELLIDOS { get; set; }
        string TIPO_DOC { get; set; }
        string NRO_DOC { get; set; }
        string ESTADO_CIVIL { get; set; }
        string ESTADO_CIVIL_ID { get; set; }
        string LUGAR_NACIMIENTO_ID { get; set; }
        string LUGAR_NACIMIENTO_DES { get; set; }
        string FECHA_NAC { get; set; }
        string SEXO { get; set; }
        string EMAIL { get; set; }
        string TELEF_REFERENCIA { get; set; }
        string TELEFONO { get; set; }
        string OCUPACION { get; set; }
        string CARGO { get; set; }
        string FAX { get; set; }
        string FLAG_EMAIL { get; set; }
        string DOMICILIO { get; set; }
        string URBANIZACION { get; set; }
        string DISTRITO { get; set; }
        string ZIPCODE { get; set; }
        string CIUDAD { get; set; }
        string DEPARTAMENTO { get; set; }
        string PROVINCIA { get; set; }
        string REFERENCIA { get; set; }
        string LONG_NRO_DOC { get; set; }
        string PAIS { get; set; }
        string RAZON_SOCIAL { get; set; }
    }
}