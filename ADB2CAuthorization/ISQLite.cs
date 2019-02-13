using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ADB2CAuthorization
{
    public interface ISQLite
    { 
        SQLiteConnection GetConnection();

    }
}
