
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenTechSoftwareApp.FacturasModule
{
    public  class Factura
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Vendedor { get; set; }
        public string Comprador { get; set; }
        public string Descripcion { get; set; }
        public double Total { get; set; }

        public override string ToString()
        {
            return $"#{Id} | {Fecha} | {Vendedor} | {Comprador} | {Descripcion} | {Total} |";
        }

    }
}
