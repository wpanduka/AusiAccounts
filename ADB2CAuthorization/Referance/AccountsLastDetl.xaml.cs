using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Android.Util;
using Microsoft.Identity.Client;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADB2CAuthorization
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountsLastDetl : ContentPage
	{
        static string resultdetail;
       // AuthenticationResult authenticationResult ;

        private const string Url = "https://accountsfunc.azurewebsites.net/api/HttpTriggerCSharpAusi/num1/{num1}/num2/{num2}?code=AqS1OsL/bpYKBjB2Iy8GkUd20IYTGIniviWYNonSIGYx/Czhi4t8aw==";

        private HttpClient _client;

        private HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }

                return _client;
            }
        }


        public AccountsLastDetl ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";
            //AuthenticationResult authenticationResult;

            AddButton.Clicked += async (s, e) =>
            {
                CategoryTYP.Text = TextFileUploaderPage.catdetail;
                TotalAmountTYP.Text = TextFileUploaderPage.totalGST;
                //    GSTAmountTYP.Text = "test3";
                //    AccountTYP.Text = "test4";

                string number1 , number2 ;

                number1 = TextFileUploaderPage.catdetail;
                number2 = TextFileUploaderPage.totalGST;

              //  var success = (CategoryTYP.Text    &&  TotalAmountTYP.Text);

                //if (!success)
                //{
                //    await DisplayAlert("Error in inputs", "You must enter two integers", "OK");
                //    return;
                //}

                Result.Text = "Please wait...";
                AddButton.IsEnabled = false;
                Exception error = null;

                try
                {
                    var url = Url.Replace("{num1}", number1).Replace("{num2}", number2);
                    var result = await Client.GetStringAsync(url);
                    Result.Text = result + $" {result.GetType()}";
                }
                catch (Exception ex)
                {
                    error = ex;
                }

                if (error != null)
                {
                    Result.Text = "Error!!";
                    await DisplayAlert("There was an error", error.Message, "OK");
                }

                AddButton.IsEnabled = true;
            };
        




        /////// from hear was the early code with sqllite and just to show //////////////////////////////////////////////
        //try
        //{
        //    //AcountsQuery IPRI = new AcountsQuery();
        //    //SQLiteConnection IPKI;
        //    //IPKI = DependencyService.Get<ISQLite>().GetConnection();
        //    //IPKI.Table<AcountsDB>();
        //    ////  var tikcount = d.ExecuteScalar<string>("SELECT TicketNum FROM TicketNumber");
        //    //var Categoryout = IPKI.ExecuteScalar<string>("SELECT Category FROM AcountsDB ORDER BY Id DESC LIMIT 1");
        //    //var TotalAmountout = IPKI.ExecuteScalar<string>("SELECT TotalAmount FROM AcountsDB ORDER BY Id DESC LIMIT 1");
        //    //var GSTAmountout = IPKI.ExecuteScalar<string>("SELECT GSTAmount FROM AcountsDB ORDER BY Id DESC LIMIT 1");
        //    //var Account = IPKI.ExecuteScalar<string>("SELECT Account FROM AcountsDB ORDER BY Id DESC LIMIT 1");

        //    //CategoryTYP.Text = Categoryout;
        //    //TotalAmountTYP.Text = TotalAmountout;
        //    //GSTAmountTYP.Text = GSTAmountout;
        //    //AccountTYP.Text = Account;

        //    CategoryTYP.Text = TextFileUploaderPage.catdetail;
        //    TotalAmountTYP.Text = "Test2";
        //    GSTAmountTYP.Text = "test3";
        //    AccountTYP.Text = "test4";

        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine(ex.ToString());
        //  //  return null;
        //}
        ////////////////early code finish hear //////////////////////////////////////////////////////////
        // DisplayAlert("INSERTED DETAILS", Categoryout, "OK");
    }

        private void Goback(object sender, EventArgs eventArgs)
        {
            Navigation.PushAsync(new ImageUploaderPage());
            //StartActivity(Intent);
        }

        private void Closenow(object sender, EventArgs eventArgs)
        {

            Navigation.PopToRootAsync();
            //StartActivity(Intent);
        }
    }
}