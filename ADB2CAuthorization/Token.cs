using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ADB2CAuthorization
{
    public class Token
    {
        [PrimaryKey]
        public long Id { get; set; }
        public string access_token { get; set; }
        public string error_description { get; set; }
        public DateTime expire_date { get; set; }
        public int expire_in { get; set; }
        public long LocationID { get; set; }
        public string Locationname { get; set; }

        public Token() { }
    }
}
