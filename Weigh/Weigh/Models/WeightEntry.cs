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
        public DateTime WeighDate { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
