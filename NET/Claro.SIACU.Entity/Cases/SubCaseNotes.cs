namespace Claro.SIACU.Entity.Cases
{
    [Data.DbTable("TEMPO")]
    public class SubCaseNotes
    {
        [Data.DbColumn("NOTAS_CASO")]
        public string Notes { get; set; }
    }
}