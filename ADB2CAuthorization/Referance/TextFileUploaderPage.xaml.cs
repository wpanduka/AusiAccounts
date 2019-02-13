using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.Storage.Blob;
using SQLite;
using Xamarin.Forms;

namespace ADB2CAuthorization
{
	public partial class TextFileUploaderPage : ContentPage
	{
        public static string catdetail;
        public static string totalGST;
        AuthenticationResult authenticationResult;
        string results;
        string uploadedFilename;
        string pickname;
        string pickcategory;
        public Nullable<int> ParallelOperationThreadCount { get; set; }

        public TextFileUploaderPage ()
		{
			InitializeComponent ();
            //Entername.Text = "$";
           // uploadEditor.Text = "$";

            BackgroundImage = "backGround.jpg";
            thePicker.SelectedIndexChanged += (sender, args) =>
            {
                if (thePicker.SelectedIndex == -1)
                {
                    pickname = "SELECT CATEGORY";
                }                
                else
                {
                    pickname = thePicker.Items[thePicker.SelectedIndex];

                }
            };

            PickerCategory.SelectedIndexChanged += (sender, args) =>
            {
                if (PickerCategory.SelectedIndex == -1)
                {
                    pickcategory = "SELECT AN ACCOUNT";
                }
                else
                {
                    pickcategory = PickerCategory.Items[PickerCategory.SelectedIndex];
                    //boxView.Color = nameToColor[colorName];
                }
            };
        }

        async void OnUploadButtonClicked(object sender, EventArgs e)
        {

            //string man = Entername.Text;
            try
            {
                //AcountsDB IPA = new AcountsDB();
                //SQLiteConnection s;
                //s = DependencyService.Get<ISQLite>().GetConnection();
                //s.Table<AcountsDB>();
                //IPA.Category = pickcategory;
                //IPA.TotalAmount = uploadEditor.Text;
                //IPA.GSTAmount = Entername.Text;
                //IPA.Account = pickname;
                //// IPA.backgrund = themetyp;            
                //AcountsQuery c = new AcountsQuery();
                //c.InsertDetails(IPA);


                //AcountsQuery IPRI = new AcountsQuery();
                //SQLiteConnection IPKI;
                //IPKI = DependencyService.Get<ISQLite>().GetConnection();
                //IPKI.Table<AcountsDB>();
                ////  var tikcount = d.ExecuteScalar<string>("SELECT TicketNum FROM TicketNumber");
                //var backnow = IPKI.ExecuteScalar<string>("SELECT Category FROM AcountsDB ORDER BY Id DESC LIMIT 1");

               // await DisplayAlert("INSERTED DETAILS", backnow, "OK");


                if (!string.IsNullOrWhiteSpace(uploadEditor.Text) && !string.IsNullOrWhiteSpace(Entername.Text))
                {
                    activityIndicator.IsRunning = true;

                    BlobRequestOptions parallelThreadCountOptions = new BlobRequestOptions();

                    string listname = "Category:" + "-" + pickcategory + Environment.NewLine + "Total Amount GST:" + "-" + uploadEditor.Text + Environment.NewLine + "GST Amount:" + "-" + Entername.Text + Environment.NewLine + "Amount:" + "-" + pickname;

                    //AcountsDB IPA = new AcountsDB();
                    //SQLiteConnection s;
                    //s = DependencyService.Get<ISQLite>().GetConnection();
                    //s.Table<AcountsDB>();
                    catdetail = pickcategory;
                    totalGST = uploadEditor.Text;
                    //IPA.TotalAmount = uploadEditor.Text;
                    //IPA.GSTAmount = Entername.Text;
                    //IPA.Account = pickname;
                    //// IPA.backgrund = themetyp;            
                    //AcountsQuery c = new AcountsQuery();
                    //c.InsertDetails(IPA);

                    //  var byteData = Encoding.UTF8.GetBytes(uploadEditor.Text);
                    var byteDatayw = Encoding.UTF8.GetBytes(listname);

                    // Allow up to four simultaneous I/O operations.
                    parallelThreadCountOptions.ParallelOperationThreadCount = 4;
                    // blob.UploadFromFile(uploadedFilename, accessCondition: null, options: parallelThreadCountOptions, operationContext: null);

                    // uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Text, new MemoryStream(listname));
                    uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Text, new MemoryStream(byteDatayw));

                    // downloadButton.IsEnabled = true;
                    activityIndicator.IsRunning = false;

                  //  var itemSelectedData = e.SelectedItem as AccountsModel;

                    await Navigation.PushAsync(new AccountsLastDetl());
                }

                else
                {
                    await DisplayAlert("INSERTED DETAILS","SOME FILEDS ARE MISSING", "OK");
                }

                //await Navigation.PushAsync(new TextFileBrowserPage());
               // await Navigation.PushAsync(new AccountsLastDetl());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Test 8");
                Debug.WriteLine(ex.ToString());
               // return null;
            }


        }

        //async void OnDownloadButtonClicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(uploadedFilename))
        //    {
        //        activityIndicator.IsRunning = true;

        //        var byteData = await AzureStorage.GetFileAsync(ContainerType.Text, uploadedFilename);
        //        var text = Encoding.UTF8.GetString(byteData);
        //        downloadEditor.Text = text;

        //        activityIndicator.IsRunning = false;
        //    }
        //}
    }
}
