using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ADB2CAuthorization.Droid
{
    [Activity(Label = "ListImagesActivity")]
    public class ListImagesActivity : Activity
    {
        //string[] images;

        //protected async override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    images = await ImageManager.ListImages();

        //    var adapter = new ArrayAdapter<string>(this, Resource.Layout.TextViewItem, images);

        //    ListAdapter = adapter;
        //}

        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    this.Intent = new Intent(this, typeof(ImageActivity));
        //    this.Intent.PutExtra("image", images[position]);

        //    StartActivity(this.Intent);
        //}
    }
}