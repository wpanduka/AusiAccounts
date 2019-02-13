using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Identity.Client;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ADB2CAuthorization
{
	public partial class App : Application
	{
		public static PublicClientApplication AuthenticationClient { get; private set; }
        static TokenDatabaseController tokendatabase;
       // static UserDatabaseController userdatabase;
        static RestService resstService;

        public App()
		{
			InitializeComponent();

            //SetMainPage();

            AuthenticationClient = new PublicClientApplication(Constants.ApplicationID);
            //MainPage = new NavigationPage(new LoginPage());
            MainPage = new NavigationPage(new WelcomePage());
        }

        //private void SetMainPage()
        //{
        //    if (!string.IsNullOrEmpty(Settings.AccessToken))
        //    {
        //        if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
        //        {
        //            var loginViewModel = new LoginViewModel();
        //            loginViewModel.LoginCommand.Execute(null);
        //        }
        //        MainPage = new NavigationPage(new WellcomePage());
        //    }
        //    else if (!string.IsNullOrEmpty(Settings.Username)
        //          && !string.IsNullOrEmpty(Settings.Password))
        //    {
        //        MainPage = new NavigationPage(new LoginNew());
        //    }
        //    else
        //    {
        //       // MainPage = new NavigationPage(new RegisterPage());
        //    }
        //}

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

        //public static UserDatabaseController UserDatabase
        //{
        //    get
        //    {
        //        if (userdatabase == null)
        //        {
        //            userdatabase = new UserDatabaseController();
        //        }
        //        return userdatabase;
        //    }
        //}

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokendatabase == null)
                {
                    tokendatabase = new TokenDatabaseController();
                }
                return tokendatabase;
            }
        }

        public static RestService RestService
        {
            get
            {
                if (resstService == null)
                {
                    resstService = new RestService();
                }
                return resstService;
            }
        }

        //private Dictionary<String, byte[]> _dictionaryPics;
        //public void PicDictAdd(string Url, byte[] bytesToAdd)
        //{
        //    lock (_dictionaryPics)
        //    {
        //        byte[] outPic;
        //        if (!_dictionaryPics.TryGetValue(Url, out outPic))
        //        {
        //            _dictionaryPics.Add(Url, bytesToAdd);
        //        }
        //    }
        //}
    }
}
