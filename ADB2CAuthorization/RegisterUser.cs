using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADB2CAuthorization
{
    public class RegisterUser
    {

        [PrimaryKey]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterUser(){}

        public RegisterUser(string FirstName,string LastName,string City,string Address,string Address2,string ZipCode,string PhoneNumber, string Email,string Password,string ConfirmPassword)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.City = City;
            this.Address = Address;
            this.Address2 = Address2;
            this.ZipCode = ZipCode;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Password = Password;
            this.ConfirmPassword = ConfirmPassword;
        }

        public bool CheckInformation()
        {         

            if(this.FirstName==null && this.LastName==null && this.City==null && this.Address==null
                && this.Address2==null && this.ZipCode==null && this.PhoneNumber ==null && this.Email==null
                && this.Password==null && this.ConfirmPassword==null)
            {
                return false;
            }
            else if (this.FirstName.Equals("") && this.LastName.Equals("") && this.City.Equals("") && this.Address.Equals("")
                && this.Address2.Equals("") && this.ZipCode.Equals("") && this.PhoneNumber.Equals("") && this.Email.Equals("")
                && this.Password.Equals("") && this.ConfirmPassword.Equals(""))
            {
                return false;
            }             
            else
                return true;

        }
    }
}
