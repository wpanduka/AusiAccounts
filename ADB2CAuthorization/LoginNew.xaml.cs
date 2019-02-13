using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Xamarin.Forms;
using Plugin.Connectivity;
using SQLite;
using ADB2CAuthorization.DataModels;

namespace ADB2CAuthorization
{
	public partial class LoginNew : ContentPage
	{
        SQLiteConnection _connection;
        string _username;
        string _password;

        public LoginNew ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";    
           
        }

        protected override void OnAppearing()
        {
            // base.OnAppearing();
            Checkavailability();
        }

        private void newuser(object sender, EventArgs eventArgs)
        {
            activity.IsVisible = true;
            Navigation.PushAsync(new NewUser());
            activity.IsVisible = false;

        }

        async void Login_click(object sender, EventArgs eventArgs)
        {
            activity.IsVisible = true;

            User user = new User(username.Text, password.Text);
            if (user.CheckInformation())
            {

                var addr = new System.Net.Mail.MailAddress(username.Text);
                if (addr.Address == username.Text)
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {                                               
                            var result = await App.RestService.Login(user);
                            if (result.IsAuthenticated)
                            {
                            // App.UserDatabase.SaveUser(user);

                                _connection.CreateTable<LoggedUser>();
                                var objLoggeduser = new LoggedUser { Id = 001, Username = username.Text, Password = password.Text };
                                _connection.Insert(objLoggeduser);

                                DependencyService.Get<Toast>().Show("Login Successful");
                                Application.Current.MainPage = new NavigationPage(new Home_Page());
                                activity.IsVisible = false;
                            }
                            else
                            {                       
                            DependencyService.Get<Toast>().Show("Login Unsuccessful, Please recheck the username and password");
                            activity.IsVisible = false;
                            }

                            //    if (Device.OS == TargetPlatform.Android)
                            //    {
                            //        await DisplayAlert("Login", "Login sucess2", "Ok");
                            //        //string detail = "Play";
                            //        Application.Current.MainPage = new NavigationPage(new WellcomePage());
                            //        //  Application.Current.MainPage = new NavigationPage(new MainVedioPage());
                            //    }
                            //    else if (Device.OS == TargetPlatform.iOS)
                            //    {
                            //        // string detail = "Play";
                            //        // await Navigation.PushModalAsync(new NavigationPage(new Dashboard()));
                            //        //   await Navigation.PushModalAsync(new NavigationPage(new MainVedioPage()));
                            //    }
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Connection Lost, Please check and retry");
                        activity.IsVisible = false;
                    }

                }
                else
                {
                    DependencyService.Get<Toast>().Show("Please enter a valid Email Address");
                     activity.IsVisible = false;
                }

            }
            else
            {
                DependencyService.Get<Toast>().Show("Login Unsuccessful, Please recheck the entry fields and retry");
                activity.IsVisible = false;
            }

            activity.IsVisible = false;

        }



        private async void Checkavailability()
        {
            activity.IsVisible = true;
            btnlogin.IsEnabled = false;
           
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<LoggedUser>();
               

            if(_connection.Table<LoggedUser>().Any())
            {
                var result = _connection.Query<LoggedUser>("Select * from LoggedUser where Id ='001' ").AsEnumerable();
                foreach (var subresult in result)
                {
                    //Console.WriteLine(subresult.Id);
                    //Console.WriteLine(subresult.Username);
                    //Console.WriteLine(subresult.Password);

                    _username = subresult.Username;
                    _password = subresult.Password;

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        User user = new User(_username, _password);
                        if (user.CheckInformation())
                        {

                            var result1 = await App.RestService.Login(user);
                            if (result1.IsAuthenticated)
                            {
                                // App.UserDatabase.SaveUser(user);
                                DependencyService.Get<Toast>().Show("Login Successful");
                                Application.Current.MainPage = new NavigationPage(new Home_Page());
                                activity.IsVisible = false;
                            }
                            else
                            {
                                DependencyService.Get<Toast>().Show("Login Unsuccessful, Please recheck the username and password");
                                activity.IsVisible = false;
                            }

                        }

                    }


                }


            }

            btnlogin.IsEnabled = true;
            activity.IsVisible = false;
        }

        //public void Remove_SQLite_Table()
        //{

        //    _connection.DropTable<Places>();
        //    _connection.CreateTable<Places>();
        //}


    }
}

