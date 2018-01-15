using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Weigh.Models
{
    public class WeightEntry
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public double Weight { get; set; }
        public double WaistSize { get; set; }
        public double WeightDelta { get; set; }
        public string Note { get; set; }
        public DateTime WeighDate { get; set; } = DateTime.UtcNow.ToLocalTime();

        public WeightEntry()
        {

        }

        public WeightEntry(double weight, DateTime date, double waistSize)
        {
            Weight = weight;
            WeighDate = date;
            WaistSize = waistSize;
        }
    }
}
