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
        readonly SQLiteAsyncConnection _database;

        public CompraDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Compra>().Wait();
        }

        public Task<List<Compra>> GetFactsAsync()
        {
            return _database.Table<Compra>().ToListAsync();
        }

        public Task<int> SaveFact(Compra compras)
        {
            if (compras.Id != 0)
            {
                return _database.UpdateAsync(compras);
            }
            else
            {
                return _database.InsertAsync(compras);
            }
        }

        public Task<int> DeleteFactAsync(Compra  compras)
        {
            return _database.DeleteAsync(compras);
        }
    }
}
