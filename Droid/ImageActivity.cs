using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ADB2CAuthorization.Droid
{
    [Activity(Label = "ImageActivity")]
    public class ImageActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);

            //SetContentView(Resource.Layout.Image);

            //var image = this.Intent.Extras.GetString("image");

            //var imageBytes = await ImageManager.GetImage(image);

            //var bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

            //var imageView = FindViewById<ImageView>(Resource.Id.ChildImageView);

            //imageView.SetImageBitmap(bitmap);
        }
    }
}