using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Weigh.Models;

namespace Weigh.Data
{
    public class WeightDatabase
    {
        #region Fields

        private readonly SQLiteAsyncConnection _database;

        #endregion

        #region Constructor

        public WeightDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<WeightEntry>().Wait();
        }

        #endregion

        #region Methods

        public Task<List<WeightEntry>> GetWeightsAsync()

        {
            return _database.Table<WeightEntry>().ToListAsync();
        }

        public Task<List<WeightEntry>> GetLatestWeightsAsync()

        {
            return _database.Table<WeightEntry>().OrderByDescending(t => t.ID).Take(10).ToListAsync();
        }

        public Task<WeightEntry> GetWeightasync(int id)
        {
            return _database.Table<WeightEntry>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveWeightAsync(WeightEntry weight)
        {
            if (weight.ID != 0)
                return _database.UpdateAsync(weight);
            return _database.InsertAsync(weight);
        }


        public Task<int> DeleteWeightInfoAsync(WeightEntry weight)
        {
            return _database.DeleteAsync(weight);
        }

        #endregion
    }
}