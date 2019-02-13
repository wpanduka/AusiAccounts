using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADB2CAuthorization
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageUploaderPage : ContentPage
	{
		public ImageUploaderPage ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";

        }

        private async void takepicturebutton_clicked(object sender, System.EventArgs e)
        {
            //await CrossMedia.Current.Initialize();

            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    await DisplayAlert("No Camera", ":( No camera available.", "OK");
            //    return;
            //}

            //var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //{
            //    SaveToAlbum = true,
            //    Name = "test.jpg"
            //});

            //if (file == null)
            //    return;

            //selectedImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void UploadButton_clicked(object sender, System.EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                //// if you want to take a picture use this
                if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsCameraAvailable)
                /// if you want to select from the gallery use this
                // if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                    return;
                }
                //! added using Plugin.Media.Abstractions;
                // if you want to take a picture use StoreCameraMediaOptions instead of PickMediaOptions
                var mediaOptions = new StoreCameraMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                // if you want to take a picture use TakePhotoAsync instead of PickPhotoAsync
                var selectedImageFile = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                if (selectedImage == null)
                {
                    await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                    return;
                }
                selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
                UploadImage(selectedImageFile.GetStream());
            }
            catch (NullReferenceException et)
            {
                Debug.WriteLine("Something is thrown ! " + et.ToString());
            }


        }



        async void Handle_Clicked(object sender, System.EventArgs e)
        {
           
            //! added using Plugin.Media;
           // await CrossMedia.Current.Initialize();

           // //// if you want to take a picture use this
           // if(!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsCameraAvailable)
           // /// if you want to select from the gallery use this
           //// if (!CrossMedia.Current.IsPickPhotoSupported)
           // {
           //     await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
           //     return;
           // }

           // //! added using Plugin.Media.Abstractions;
           // // if you want to take a picture use StoreCameraMediaOptions instead of PickMediaOptions
           // var mediaOptions = new StoreCameraMediaOptions()
           // {
           //     PhotoSize = PhotoSize.Medium
           // };
           // // if you want to take a picture use TakePhotoAsync instead of PickPhotoAsync
           // var selectedImageFile = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

           // if (selectedImage == null)
           // {
           //     await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
           //     return;
           // }

           // selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());


           // UploadImage(selectedImageFile.GetStream());
        }

        private async void UploadImage(Stream imageToUpload)
        {
            //! added using Microsoft.WindowsAzure.Storage;
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=wpanduka;AccountKey=a7yreWgAA3kUiG4u2igZ1KaJJD1Ss2P97B9o39GKTb9vmIRL0SsVVEiTmHxuPzPE8CLyK/h+UFc5OITP8f+UiQ==;EndpointSuffix=core.windows.net");
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");

            string uniqueName = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{uniqueName}.jpg");
            await blockBlob.UploadFromStreamAsync(imageToUpload);

            string thePlaceInTheInternetWhereThisImageIsNowLocated = blockBlob.Uri.OriginalString;

            await DisplayAlert("Images is on", thePlaceInTheInternetWhereThisImageIsNowLocated, "OK");

            await Navigation.PushAsync(new TextFileUploaderPage());
        }
    }
}