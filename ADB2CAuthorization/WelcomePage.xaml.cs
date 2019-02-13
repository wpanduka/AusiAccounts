using ADB2CAuthorization.DataModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADB2CAuthorization
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : ContentPage
	{
        SQLiteConnection _connection;

        public WelcomePage ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";
         
        }

        

        //private void btnHome_Clicked(object sender, EventArgs e)
        //{
        //    DisplayAlert("Home Button", "HOME", "OK");
        //   // _connection = DependencyService.Get<ISQLite>().GetConnection();
        //    //_connection.DropTable<LoggedUser>();
         

        //}

        private void btnAboutus_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("Home Button", "ABOUT US", "OK");
            Navigation.PushAsync(new AboutUs());
        }

        private void btnIndex_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Home Button", "INDEX", "OK");
        }

        async private void btnSignIn_Clicked(object sender, EventArgs e)
        {
            activity.IsVisible = true;
            await Navigation.PushAsync(new LoginNew());
            activity.IsVisible = false;
        }
    }
}