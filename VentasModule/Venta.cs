using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenTechSoftwareApp.VentasModule
{
    public class Venta
    {
        //modelo de venta
        //autoincrementa el id y es primary key
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Vendedor { get; set; }
        public string Comprador { get; set; }
        public string Descripcion { get; set; }
        public double Total { get; set; }

        //retornamos una cadena con las propiedades del modelo
        public override string ToString()
        {
            return $"#{Id} |Fec: {Fecha} |Ven: {Vendedor} |Com: {Comprador} |Des: {Descripcion} |Tot: ${Total} | ";
        }
    }
}
