using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RavenTechSoftwareApp.VentasModule
{
    public class VentaDatabase
    {
        readonly SQLiteAsyncConnection _database;//iniciamos una coneccion con sqlite

        public VentaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);//creamos la instancia y le mandamos  un dbpath como argumento
            _database.CreateTableAsync<Venta>().Wait();//creamos la tabla de compra
        }

        public Task<List<Venta>> GetFactsAsync()
        {
            return _database.Table<Venta>().ToListAsync();//enlistamos los registros de las tablas
        }

        public Task<int> SaveFact(Venta ventas)
        {
            //metodo guardda el objeto que recibe, si este ya existe lo actualiza
            if (ventas.Id != 0)
            {
                return _database.UpdateAsync(ventas);
            }
            else
            {
                return _database.InsertAsync(ventas);
            }
        }

        //borra el objeto que recibe
        public Task<int> DeleteFactAsync(Venta ventas)
        {
            return _database.DeleteAsync(ventas);
        }
    }
}
