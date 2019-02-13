using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ADB2CAuthorization
{
    public class AcountsDB
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [MaxLength(500)]
        public string Category { get; set; }
        public string TotalAmount { get; set; }
        public string GSTAmount { get; set; }
        public string Account { get; set; }

        public AcountsDB()
        {
        }
    }
}
