using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenTechSoftwareApp.FacturasModule
{
    public class FacturaDatabase
    {
        readonly SQLiteAsyncConnection _database;//iniciamos una coneccion con sqlite

        public FacturaDatabase(string dbPath)
        {
            
            _database = new SQLiteAsyncConnection(dbPath);//creamos la instancia y le mandamos  un dbpath como argumento
            _database.CreateTableAsync<Factura>().Wait();//creamos la tabla de Factura
        }

        public Task<List<Factura>> GetFactsAsync()
        {
            return _database.Table<Factura>().ToListAsync();//enlistamos los registros de las tablas
        }


        public Task<int> SaveFact(Factura factura)
        {
            //metodo guardda el objeto que recibe, si este ya existe lo actualiza
            if (factura.Id != 0)
            {
                return _database.UpdateAsync(factura);
            }
            else
            {
                return _database.InsertAsync(factura);
            }
        }

        //borra el objeto que recibe
        public Task<int> DeleteFactAsync(Factura factura)
        {
            return _database.DeleteAsync(factura);
        }
    }
}
