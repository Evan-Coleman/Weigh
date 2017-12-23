using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weigh.Models;

namespace Weigh.Data
{
    public class WeightDatabase
    {
        #region Fields
        private readonly SQLiteAsyncConnection database;
        #endregion

        #region Constructor
        public WeightDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<WeightEntry>().Wait();
        }
        #endregion

        #region Methods
        public Task<List<WeightEntry>> GetWeightsAsync()

        {
            return database.Table<WeightEntry>().ToListAsync();
        }

        public Task<WeightEntry> GetWeightasync(int id)
        {
            return database.Table<WeightEntry>().
                Where(i => i.ID == id).
                FirstOrDefaultAsync();
        }

        public Task<int> SaveWeightAsync(WeightEntry weight)
        {
            if (weight.ID != 0)
            {
                return database.UpdateAsync(weight);
            }
            else
            {
                return database.InsertAsync(weight);
            }
        }

        public Task<int> DeleteWeightInfoAsync(WeightEntry weight)
        {
            return database.DeleteAsync(weight);
        }
        #endregion
    }
}
