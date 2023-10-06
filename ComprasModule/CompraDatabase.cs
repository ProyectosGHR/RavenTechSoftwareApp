using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenTechSoftwareApp.ComprasModule
{
    public class CompraDatabase
    {
        readonly SQLiteAsyncConnection _database;//iniciamos una coneccion con sqlite

        public CompraDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);//creamos la instancia y le mandamos  un dbpath como argumento
            _database.CreateTableAsync<Compra>().Wait();//creamos la tabla de compra
        }

        public Task<List<Compra>> GetFactsAsync()
        {
            return _database.Table<Compra>().ToListAsync();//enlistamos los registros de las tablas
        }

        public Task<int> SaveFact(Compra compras)
        {
            //metodo guardda el objeto que recibe, si este ya existe lo actualiza
            if (compras.Id != 0)
            {
                return _database.UpdateAsync(compras);
            }
            else
            {
                return _database.InsertAsync(compras);
            }
        }

        //borra el objeto que recibe
        public Task<int> DeleteFactAsync(Compra  compras)
        {
            return _database.DeleteAsync(compras);
        }
    }
}
