using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ADB2CAuthorization
{

	public partial class TextFileBrowserPage : ContentPage
	{
        string fileName;

        public TextFileBrowserPage ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";
        }

        async void OnGetFileListButtonClicked(object sender, EventArgs e)
        {
            var fileList = await AzureStorage.GetFilesListAsync(ContainerType.Text);
            listView.ItemsSource = fileList;
            editor.Text = string.Empty;
            deleteButton.IsEnabled = false;
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fileName = e.SelectedItem.ToString();
            var byteData = await AzureStorage.GetFileAsync(ContainerType.Text, fileName);
            var text = Encoding.UTF8.GetString(byteData);
            editor.Text = text;
            deleteButton.IsEnabled = true;
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                bool isDeleted = await AzureStorage.DeleteFileAsync(ContainerType.Text, fileName);
                if (isDeleted)
                {
                    OnGetFileListButtonClicked(sender, e);
                }
            }
        }
    }
}
