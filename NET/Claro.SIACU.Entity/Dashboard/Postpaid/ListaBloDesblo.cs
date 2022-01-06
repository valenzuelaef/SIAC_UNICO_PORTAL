using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Serializable()]
    public class ListaBloDesblo
    {

        private string _Codigo_Cliente;
        private string _Codigo;
        private string _Estado;
        private string _Prioridad;
        private string _Descripcion;
        private string _Usuario_Seguimiento;
        private DateTime _Fecha_Seguimiento;
        private string _sFecha;
        private DateTime _Fecha;
        private string _Accion_Seguimiento;
        private DateTime _Fecha_Apertura;
        private DateTime _Fecha_Cierre;
        private string _desc_tipo;
        private string _tipo;
        private string _Nro_Tickler;

        public ListaBloDesblo() { }

        public string Codigo
        {
            set { _Codigo = value; }
            get { return _Codigo; }
        }
        public string Codigo_Cliente
        {
            set { _Codigo_Cliente = value; }
            get { return _Codigo_Cliente; }
        }
        public string Estado
        {
            set { _Estado = value; }
            get { return _Estado; }
        }
        public string Prioridad
        {
            set { _Prioridad = value; }
            get { return _Prioridad; }
        }
        public string Descripcion
        {
            set { _Descripcion = value; }
            get { return _Descripcion; }
        }
        public string Usuario_Seguimiento
        {
            set { _Usuario_Seguimiento = value; }
            get { return _Usuario_Seguimiento; }
        }
        public DateTime Fecha_Seguimiento
        {
            set { _Fecha_Seguimiento = value; }
            get { return _Fecha_Seguimiento; }
        }
        public DateTime Fecha
        {
            set { _Fecha = value; }
            get { return _Fecha; }
        }
        public string sFecha
        {
            set { _sFecha = value; }
            get { return _sFecha; }
        }
        public string Accion_Seguimiento
        {
            set { _Accion_Seguimiento = value; }
            get { return _Accion_Seguimiento; }
        }
        public DateTime Fecha_Apertura
        {
            set { _Fecha_Apertura = value; }
            get { return _Fecha_Apertura; }
        }
        public DateTime Fecha_Cierre
        {
            set { _Fecha_Cierre = value; }
            get { return _Fecha_Cierre; }
        }
        public string Nro_Tickler
        {
            set { _Nro_Tickler = value; }
            get { return _Nro_Tickler; }
        }
        public string TIPO
        {
            set { _tipo = value; }
            get { return _tipo; }
        }
        public string DESC_TIPO
        {
            set { _desc_tipo = value; }
            get { return _desc_tipo; }
        }
        public int CompareTo(object obj)
        {
            ListaBloDesblo Compare = (ListaBloDesblo)obj;
            int intSortDirection = -1;
            int result = this.Fecha.CompareTo(Compare.Fecha);
            if (result == 0)
                result = this.Fecha.CompareTo(Compare.Fecha);
            return result * intSortDirection;
        }
    }
}
