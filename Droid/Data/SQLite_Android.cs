using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ADB2CAuthorization.Droid.Data;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]

namespace ADB2CAuthorization.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
     
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqlLiteFilename = "AusAccountsDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqlLiteFilename);
            return new SQLite.SQLiteConnection(path);
           
        }

    }
}