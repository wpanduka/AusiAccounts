using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ADB2CAuthorization.iOS;
using Foundation;
using SQLite;
using UIKit;


[assembly: Xamarin.Forms.Dependency(typeof(SQLite_IOS))]
namespace ADB2CAuthorization.iOS
{
    public class SQLite_IOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqlLiteFilename = "AusAccountsDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); 
            var path = Path.Combine(libraryPath, sqlLiteFilename);

            return new SQLite.SQLiteConnection(path);
        }
    }
}