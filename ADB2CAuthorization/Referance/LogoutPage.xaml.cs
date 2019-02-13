using System;
using System.IO;
//using Android.App;
//using Android.Content;
//using Android.Widget;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ADB2CAuthorization
{
	public partial class LogoutPage : ContentPage
	{
		AuthenticationResult authenticationResult;
        public static readonly int PickImageId = 1000;
    //    private ImageView imageView;
     //   private Android.Net.Uri imageUri;

      //  private Button uploadButton;

        public LogoutPage(AuthenticationResult result)
		{
			InitializeComponent();
            BackgroundImage = "backGround.jpg";
            authenticationResult = result;
		}

        public LogoutPage()
        {
            InitializeComponent();
            BackgroundImage = "backGround.jpg";
            //authenticationResult = result;
        }

        //protected override void OnAppearing()
        //{
        //	if (authenticationResult != null)
        //	{
        //		if (authenticationResult.User.Name != "unknown")
        //		{
        //			messageLabel.Text = string.Format("Welcome {0}", authenticationResult.User.Name);
        //		}
        //		else
        //		{
        //			messageLabel.Text = string.Format("UserId: {0}", authenticationResult.User.UniqueId);
        //		}
        //	}

        //	base.OnAppearing();
        //}

        async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
			await Navigation.PopAsync();
		}

       private void UploadButtonOnClick(object sender, EventArgs eventArgs)
        {

            // new NavigationPage(new ImagePage());

             Navigation.PushAsync(new ImageUploaderPage());
            //! added using Plugin.Media;
            // await CrossMedia.Current.Initialize();

            ////// if you want to take a picture use this
            //if(!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsCameraAvailable)
            ///// if you want to select from the gallery use this
            ////if (!CrossMedia.Current.IsPickPhotoSupported)
            //{
            //    await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
            //    return;
            //}

            ////! added using Plugin.Media.Abstractions;
            //// if you want to take a picture use StoreCameraMediaOptions instead of PickMediaOptions
            //var mediaOptions = new PickMediaOptions()
            //{
            //    PhotoSize = PhotoSize.Medium
            //};
            //// if you want to take a picture use TakePhotoAsync instead of PickPhotoAsync
            //var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            ////if (selectedImage == null)
            ////{
            ////    await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
            ////    return;
            ////}

            ////selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

            //UploadImage(selectedImageFile.GetStream());
            //// UploadImage(selectedImageFile.GetStream());

            ////try
            ////{
            ////    var inputStream = ContentResolver.OpenInputStream(imageUri);

            ////    var imageName = await ImageManager.UploadImage(inputStream);

            ////    new AlertDialog.Builder(this)
            ////        .SetMessage("Image uploaded successfully!. Image name: " + imageName)
            ////        .SetTitle("Image upload")
            ////        .Show();
            ////}
            ////catch (Exception ex)
            ////{
            ////    new AlertDialog.Builder(this)
            ////        .SetMessage("The image could not be uploaded. Error " + ex.Message)
            ////        .SetTitle("Image upload")
            ////        .Show();
            ////}
        }

        //async void UploadImage(Stream imageToUpload)
        //{
        //    var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=wpanduka;AccountKey=a7yreWgAA3kUiG4u2igZ1KaJJD1Ss2P97B9o39GKTb9vmIRL0SsVVEiTmHxuPzPE8CLyK/h+UFc5OITP8f+UiQ==;EndpointSuffix=core.windows.net");
        //    var blobClient = account.CreateCloudBlobClient();
        //    var container = blobClient.GetContainerReference("images");
        //    string uniqueName = Guid.NewGuid().ToString();
        //    var blockBlob = container.GetBlockBlobReference($"{uniqueName}.jpg");
        //    await blockBlob.UploadFromStreamAsync(imageToUpload);
        //}

        //private async void UploadImage(Stream imageToUpload)
        //{
        //    //! added using Microsoft.WindowsAzure.Storage;
        //    var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=wpanduka;AccountKey=a7yreWgAA3kUiG4u2igZ1KaJJD1Ss2P97B9o39GKTb9vmIRL0SsVVEiTmHxuPzPE8CLyK/h+UFc5OITP8f+UiQ==;EndpointSuffix=core.windows.net");
        //    var blobClient = account.CreateCloudBlobClient();
        //    var container = blobClient.GetContainerReference("images");

        //    string uniqueName = Guid.NewGuid().ToString();
        //    var blockBlob = container.GetBlockBlobReference($"{uniqueName}.jpg");
        //    await blockBlob.UploadFromStreamAsync(imageToUpload);

        //    string thePlaceInTheInternetWhereThisImageIsNowLocated = blockBlob.Uri.OriginalString;
        //}

        private void SelectButtonOnClick(object sender, EventArgs eventArgs)
        {
            Navigation.PushAsync(new ImagePage());
            //    Intent = new Intent();
            //    Intent.SetType("image/*");
            //    Intent.SetAction(Intent.ActionGetContent);
            //    StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);

            //    uploadButton.Enabled = true;
        }

        //protected void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
        //    {
        //        imageUri = data.Data;
        //        imageView.SetImageURI(imageUri);
        //    }
        //}

        private void ShowButtonOnClick(object sender, EventArgs eventArgs)
        {
            Navigation.PushAsync(new TextFileBrowserPage());
            //StartActivity(Intent);
        }

        private void LoyalOnClicked(object sender, EventArgs eventArgs)
        {
            Navigation.PushAsync(new TextFileUploaderPage());
            //Intent = new Intent(this, typeof(ListImagesActivity));
            //StartActivity(Intent);
        }

    }
}

