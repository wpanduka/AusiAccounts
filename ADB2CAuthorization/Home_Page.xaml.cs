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
	public partial class Home_Page : ContentPage
	{
        SQLiteConnection _connection;

        public Home_Page ()
		{
			InitializeComponent ();    
            BackgroundImage = "backGround.jpg";
        }


        private void logoutButton_Clicked(object sender, EventArgs e)
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.DropTable<LoggedUser>();

            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }

        private void PickButton_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ImageUploaderPage());
        }

        private void UploadButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ImagePickerPage());
        }

        private void TrackMilage_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new TextFileBrowserPage());
        }

        private void LoyalityCard_Clicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new TextFileUploaderPage());
        }


    }
}