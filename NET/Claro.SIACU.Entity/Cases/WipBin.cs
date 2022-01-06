namespace Claro.SIACU.Entity.Cases
{
    [Data.DbTable("TEMPO")]
    public class WipBin
    {
        [Data.DbColumn("BANDEJA_ENTRADA")]
        public string Inbox { get; set; }
    }
}