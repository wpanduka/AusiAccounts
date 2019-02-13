using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Xamarin.Forms;

namespace ADB2CAuthorization
{
    public class ImagePage : ContentPage
    {
        readonly Label _title = new Label { HorizontalTextAlignment = TextAlignment.Center };
        readonly Image _image = new Image();
        readonly ActivityIndicator _activityIndicator = new ActivityIndicator();

        public ImagePage()
        {
            Content = new StackLayout
            {
                Spacing = 15,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                _title,
                _image,
                _activityIndicator
            }
            };

            Title = "Image Page";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _activityIndicator.IsRunning = true;

            // var blobList = await BlobStorageService.GetBlobs<CloudBlockBlob>("photos");


            var blobList = await BlobStorageService.GetBlobs<CloudBlockBlob>("images");
            

             var firstBlob = blobList?.LastOrDefault();
            var photo = new PhotoModel { Title = firstBlob?.Name, Uri = firstBlob?.Uri };

            _title.Text = photo?.Title;
            _image.Source = ImageSource.FromUri(photo?.Uri);

            _activityIndicator.IsRunning = false;
            _activityIndicator.IsVisible = false;

            var allBlobsList = new List<string>();
            BlobContinuationToken token = null;

            //do
            //{
            //    var result = await container.ListBlobsSegmentedAsync(token);
            //    if (result.Results.Count() > 0)
            //    {
            //        var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
            //        allBlobsList.AddRange(blobs);
            //    }
            //    token = result.ContinuationToken;
            //} while (token != null);

        }

        //public static async Task<IList<string>> GetFilesListAsync(ContainerType containerType)
        //{
        //    var container = GetContainer(containerType);

        //    var allBlobsList = new List<string>();
        //    BlobContinuationToken token = null;

        //    do
        //    {
        //        var result = await container.ListBlobsSegmentedAsync(token);
        //        if (result.Results.Count() > 0)
        //        {
        //            var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
        //            allBlobsList.AddRange(blobs);
        //        }
        //        token = result.ContinuationToken;
        //    } while (token != null);

        //    return allBlobsList;
        //}
    }
}
