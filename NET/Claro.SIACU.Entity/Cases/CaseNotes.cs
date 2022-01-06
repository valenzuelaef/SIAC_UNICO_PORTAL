namespace Claro.SIACU.Entity.Cases
{
    [Data.DbTable("TEMPO")]
    public class CaseNotes
    {
        [Data.DbColumn("NOTAS_CASO")]
        public string Notes { get; set; }
    }
}
