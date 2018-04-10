using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Eshop
{
    public class Conection
    {
        private SQLiteAsyncConnection database;

        public Conection(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Obednavka>().Wait();
        }
        public Task<List<Obednavka>> GetItemsAsync()
        {
            return database.Table<Obednavka>().ToListAsync();
        }
        public Task<List<Obednavka>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Obednavka>("SELECT * FROM [Obednavka] WHERE [Done] = 0");
        }
        public Task<List<Obednavka>> DeleteObednavka()
        {
            return database.QueryAsync<Obednavka>("DROP TABLE Obednavka");
        }
        public Task<Obednavka> GetItemAsync(int id)
        {
            return database.Table<Obednavka>().Where(i => i.id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Obednavka item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(Obednavka item)
        {
            return database.DeleteAsync(item);
        }
    }
}
