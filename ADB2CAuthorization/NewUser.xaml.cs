using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;

using Xamarin.Forms;

namespace ADB2CAuthorization
{
	public partial class NewUser : ContentPage
	{
		public NewUser ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";
        }

        async void btnRegUser_Clicked(object sender, EventArgs e)
        {
            try
            {

                activity.IsVisible = true;
                RegisterUser regUser = new RegisterUser(txtFName.Text, txtLName.Text, txtCity.Text, txtAdd.Text, txtAdd2.Text, txtZipCode.Text, txtPhoneNo.Text, txtEmail.Text, txtPass.Text, txtConPass.Text);
                if (regUser.CheckInformation())
                {
                        var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                        if (addr.Address == txtEmail.Text)
                        {

                            if (txtPass.Text == txtConPass.Text)
                            {

                                if (CrossConnectivity.Current.IsConnected)
                                {
                                        var result = await App.RestService.Register(regUser);                                        
                                        DependencyService.Get<Toast>().Show(result.Message.ToString());
                                        Application.Current.MainPage = new NavigationPage(new LoginNew());
                                }
                                else
                                {
                                    DependencyService.Get<Toast>().Show("Connection Lost, Please check and retry");
                                }

                            }
                            else
                            {
                                DependencyService.Get<Toast>().Show("Registration Unsuccessful, Password and Confirm password dont not match");
                            }

                        }
                        else
                        {
                            DependencyService.Get<Toast>().Show("Registration Unsuccessful, Please enter a valid Email Address");
                        }
                }
                else
                {
                    DependencyService.Get<Toast>().Show("Registration Unsuccessful, Please recheck the entry fields and retry");
                }

                activity.IsVisible = false;

            }
            catch (Exception ye)
            {

                DependencyService.Get<Toast>().Show("Issue occured! " + ye.ToString());
            }

        }
    }
}
