using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADB2CAuthorization.DataModels
{

    [Table("LoggedUser")]
    public class LoggedUser
    {

        [PrimaryKey]
        public long Id { get; set; }

        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

    }
}
