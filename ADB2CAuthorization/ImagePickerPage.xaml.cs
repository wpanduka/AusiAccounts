
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace ADB2CAuthorization
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImagePickerPage : ContentPage
	{
        string Var_Category;
        double Var_TotGstAmount;
        double Var_GstAmount;
        string Var_Account;
        string Var_ImagePath;
        string Var_Image;

        List<Image> pickedImages = new List<Image>();

		public ImagePickerPage ()
		{
			InitializeComponent ();
            BackgroundImage = "backGround.jpg";

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => {
                imgcamera.IsEnabled = false;
                await ClickCameraImage();            
            };
            imgcamera.GestureRecognizers.Add(tapGestureRecognizer);

            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += async (s, e) => {
                lblCamera.IsEnabled = false;
                await ClickCameraImage();            
            };
            lblCamera.GestureRecognizers.Add(tapGestureRecognizer1);

            //Set Focus
            txtTotGstAmount.Completed += (s, e) => txtGstAmount.Focus();
           

        }

        private async void btnBrowseImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                //await CrossMedia.Current.Initialize();
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
               // UploadImage(selectedImageFile.GetStream());
            }
            catch (Exception et)
            {              
                DependencyService.Get<Toast>().Show("Issue occured! " + et.ToString());
            }
        }
      
        public async Task ClickCameraImage()
        {
            try
            {
                activityIndicator.IsVisible = true;

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsCameraAvailable)
                {
                    await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                    return;
                }


                var mediaOptions = new StoreCameraMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };


                var selectedImageFile = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                if (selectedImage == null)
                {
                    await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                    return;
                }

                if(selectedImageFile!=null)
                {
                    Var_ImagePath = selectedImageFile.Path;
                    Var_Image = ImageToBase64(Var_ImagePath);
                    //Console.WriteLine(Var_Image);

                    selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
                    selectedImage.IsVisible = true;
                }

                

                //Converting the Base64String to Image
                //Base64ToImage(Var_Image);

                activityIndicator.IsVisible = false;
                imgcamera.IsEnabled = true;
                lblCamera.IsEnabled = true;

            }
            catch (Exception ey)
            {
                DependencyService.Get<Toast>().Show("Issue occured!");
            }

        }

        public string ImageToBase64(string path)
        {
            //string path = "D:\\SampleImage.jpg";
            //using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            //{
            //    using (MemoryStream m = new MemoryStream())
            //    {
            //        image.Save(m, image.RawFormat);
            //        byte[] imageBytes = m.ToArray();
            //        string base64String = Convert.ToBase64String(imageBytes);
            //        return base64String;
            //    }
            //}

            // provide read access to the file
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];
            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
            //Close the File Stream
            fs.Close();
            string _base64String = Convert.ToBase64String(ImageData);


            return _base64String;

        }

        public void Base64ToImage(string FileResponse)
        {
            Image image_ = new Image();

            byte[] data = Convert.FromBase64String(FileResponse);
            image_.Source= ImageSource.FromStream(() => new MemoryStream(data));
          
        }

        private async void uploadButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                activityIndicator.IsVisible = true;
                try
                {                       
                        if (PickerCategory.SelectedItem != null)
                        {                         
                            if (txtTotGstAmount.Text != null && txtTotGstAmount.Text != "")
                            {

                                    if (checkNumeric(txtTotGstAmount.Text))
                                    {

                                        if (txtGstAmount.Text != null && txtGstAmount.Text != "")
                                        {

                                                if (checkNumeric(txtGstAmount.Text))
                                                {

                                                    if (PickerAccount.SelectedItem != null)
                                                    {

                                                        if (selectedImage.Source != null)
                                                        {
                                                            Var_Account = PickerAccount.SelectedItem.ToString();
                                                            Var_GstAmount = Convert.ToDouble(txtGstAmount.Text);
                                                            Var_Category = PickerCategory.SelectedItem.ToString();
                                                            Var_TotGstAmount = Convert.ToDouble(txtTotGstAmount.Text);
                                                            await App.RestService.newPostReceipt(Var_Category, Var_TotGstAmount, Var_GstAmount, Var_Account, Var_Image, Var_ImagePath);
                                                        }
                                                        else
                                                        {
                                                            DependencyService.Get<Toast>().Show("Attach the receipt to proceed ");
                                                        }
                                                    }
                                                    else
                                                    {

                                                        DependencyService.Get<Toast>().Show("Account is empty");
                                                    }


                                                }
                                                else
                                                {
                                                     DependencyService.Get<Toast>().Show("GST Amount should be numeric");
                                                }

                                        }
                                        else
                                        {
                                            DependencyService.Get<Toast>().Show("GST Amount is empty");
                                        }


                                    }
                                    else
                                    {
                                         DependencyService.Get<Toast>().Show("GST Total Amount should be numeric");
                                    }

                            }
                            else
                            {
                                DependencyService.Get<Toast>().Show("GST Total Amount is empty");
                            }

                        }
                        else
                        {
                             DependencyService.Get<Toast>().Show("Category is empty");
                        }

                }
                catch (Exception eu)
                {
                    DependencyService.Get<Toast>().Show("Issue occured! " + eu.ToString());
                }

                activityIndicator.IsVisible = false;

            }
            else
            {
                DependencyService.Get<Toast>().Show("Connection Lost, Please check and retry");
            }

        }

        private void uploadButtontwo_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Home_Page());
        }

        private bool checkNumeric(string numString)
        {
            decimal number3 = 0;

            bool canConvert = decimal.TryParse(numString, out number3);
            if (canConvert == true)
                return true;
            else
                return false;
        }


    }
}