using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ADB2CAuthorization
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        public long Id { get; set; }

        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        public User() { }
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;

        }

        public bool CheckInformation()
        {
            //if (Username.Equals("admin") && Password.Equals("admin"))
            //    return true;
            //else
            //    return false;

            if (this.Username==null && this.Password==null)
            {
                return false;
            }
            else if (this.Username.Equals("") && this.Password.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
              
        }
    }
}
