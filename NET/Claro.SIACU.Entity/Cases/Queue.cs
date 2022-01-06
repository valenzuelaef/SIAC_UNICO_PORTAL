namespace Claro.SIACU.Entity.Cases
{
    [Data.DbTable("TEMPO")]
    public class Queue
    {
        [Data.DbColumn("COLA")]
        public string Title { get; set; }
    }
}