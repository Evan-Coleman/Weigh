using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weigh.Models;
using System.Linq;

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
            database.CreateTableAsync<SetupInfoDB>().Wait();
        }
        #endregion

        #region Methods
        public Task<List<WeightEntry>> GetWeightsAsync()

        {
            return database.Table<WeightEntry>().ToListAsync();
        }

        public Task<List<WeightEntry>> GetLatestWeightsAsync()

        {
            return database.Table<WeightEntry>().OrderByDescending(t => t.ID).Take(10).ToListAsync();               
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



        // SetupInfo section
        public Task<List<SetupInfoDB>> GetSetupInfoasync()
        {
            return database.Table<SetupInfoDB>().ToListAsync();
        }

        public Task<int> NewSetupInfoAsync(SetupInfoDB setupInfo)
        {
            // TODO: Check if works
            return database.InsertAsync(setupInfo);
        }

        public Task<int> SaveSetupInfoAsync(SetupInfoDB setupInfo)
        {
            // TODO: Check if works
            return database.UpdateAsync(setupInfo);
        }
        #endregion
    }
}
