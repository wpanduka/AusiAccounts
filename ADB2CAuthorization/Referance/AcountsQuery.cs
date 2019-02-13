using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace ADB2CAuthorization
{
    public class AcountsQuery
    {
        static object locker = new object();

        SQLiteConnection IPD;

        public AcountsQuery()
        {
            IPD = DependencyService.Get<ISQLite>().GetConnection();
            IPD.CreateTable<AcountsDB>();
        }

        //Insert 
        public int InsertDetails(AcountsDB acountsDB)
        {
            lock (locker)
            {
                return IPD.Insert(acountsDB);
            }
        }

        //Update 
        public int UpdateDetails(AcountsDB acountsDB)
        {
            lock (locker)
            {
                return IPD.Update(acountsDB);
            }
        }

        //Delete 
        public int DeleteNote(AcountsDB acountsDB)
        {
            lock (locker)
            {
                return IPD.Delete(acountsDB);
            }
        }

        //Get all 
        public IEnumerable<AcountsDB> GetAccountlist()
        {
            lock (locker)
            {
                return (from i in IPD.Table<AcountsDB>() select i).ToList();
            }
        }


        //Get specific customer by ID
        public void AccountID()
        {
            lock (locker)
            {
                // return s.Table<TempTbl>().LastOrDefault();
                List<AcountsDB> TikName = (from i in IPD.Table<AcountsDB>() select i).ToList();
            }
        }

        //Dispose
        public void Dispose()
        {
            IPD.Dispose();
        }
    }
}
