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
        readonly SQLiteAsyncConnection _database;

        public FacturaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Factura>().Wait();
        }

        public Task<List<Factura>> GetFactsAsync()
        {
            return _database.Table<Factura>().ToListAsync();
        }

        public Task<int> SaveFact(Factura factura)
        {
            if (factura.Id != 0)
            {
                return _database.UpdateAsync(factura);
            }
            else
            {
                return _database.InsertAsync(factura);
            }
        }

        public Task<int> DeleteFactAsync(Factura factura)
        {
            return _database.DeleteAsync(factura);
        }


    }
}
