using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using Xamarin.Forms;

namespace ADB2CAuthorization
{
    public class RestService
    {
        public static string ipupdate;

        List<APIResultRegister> API_RegResult = new List<APIResultRegister>();

        public static HttpClient HttpClient { get; private set; } = new HttpClient() { MaxResponseContentBufferSize = 256000 };
        string grant_type = "password";
        public static string user_token;

        public RestService()
        {
           //  client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded' "));
        }

        //public class NativeTypeConverter : JsonConverter
        //{
        //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //    {
        //        JToken token = JToken.Load(reader);
        //        if (objectType == typeof(nfloat))
        //        {
        //            float floatValue = (float)token;
        //            return (nfloat)floatValue;
        //        }
        //        else if (objectType == typeof(nint))
        //        {
        //            int intValue = (int)token;
        //            return (nint)intValue;
        //        }
        //        else
        //        {
        //            uint uintValue = (uint)token;
        //            return (nuint)uintValue;
        //        }
        //    }

        //    public override bool CanRead
        //    {
        //        get { return true; }
        //    }

        //    public override bool CanWrite
        //    {
        //        get { return false; }
        //    }

        //    public override bool CanConvert(Type objectType)
        //    {
        //        if (objectType == typeof(nfloat) || objectType == typeof(nint) || objectType == typeof(nuint))
        //            return true;
        //        else
        //            return false;
        //    }
        //}



        public async Task<APIResult> Login(User user)
        {
            //IPQuery IPZ = new IPQuery();
            //SQLiteConnection IPY;
            //IPY = DependencyService.Get<ISQLite>().GetConnection();
            //IPY.Table<IPaddressDB>();
            ////  var tikcount = d.ExecuteScalar<string>("SELECT TicketNum FROM TicketNumber");
            //var ipdis = IPY.ExecuteScalar<string>("SELECT IPnumb FROM IPaddressDB ORDER BY Id DESC LIMIT 1");

            // ipupdate = ipdis;

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", user.Username));
            postData.Add(new KeyValuePair<string, string>("password", user.Password));
            var content = new FormUrlEncodedContent(postData);
            var weburl = "http://ausiaccounts.azurewebsites.net/api/login";

            var response = await HttpClient.PostAsync(weburl, content);
            var jasonResult = response.Content.ReadAsStringAsync().Result;
            var responseObject = (APIResult)JsonConvert.DeserializeObject<APIResult>(jasonResult);
            if (response.StatusCode == HttpStatusCode.OK)
                responseObject.IsAuthenticated = true;
                user_token = responseObject.Message;
            return responseObject;
        }

        public async Task  PostReceipt(string cat,double totamt,double amt,string acc,string img)
        {
 

            HttpClient.DefaultRequestHeaders.Add("bearer", user_token);

            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("ReceiptImageBase64String", img));
            postData.Add(new KeyValuePair<string, string>("Category", cat));
            postData.Add(new KeyValuePair<string, string>("TotalAmountWithGST", totamt.ToString()));
            postData.Add(new KeyValuePair<string, string>("GSTAmount", amt.ToString()));
            postData.Add(new KeyValuePair<string, string>("Account", acc));

        

            var content = new FormUrlEncodedContent(postData);
            var weburl = "http://ausiaccounts.azurewebsites.net/api/AddReceipt";

            var response = await HttpClient.PostAsync(weburl, content);
            var jasonResult = response.Content.ReadAsStringAsync().Result;
            var responseObject = (String)JsonConvert.DeserializeObject<String>(jasonResult);
            
        }



        public async Task newPostReceipt(string cat, double totamt, double amt, string acc, string img,string imgpath)
        {
            

            var SysURL = "https://ausiaccounts.azurewebsites.net/";


            using (var client = new HttpClient())
            {
                try
                {

                    client.BaseAddress = new Uri(SysURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                  

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user_token);

                    ReceiptModel receiptModel = new ReceiptModel();
                    receiptModel.Account = acc;
                    receiptModel.Category = cat;
                    receiptModel.GSTAmount = amt.ToString();
                    receiptModel.ReceiptImageBase64String = img;
                    receiptModel.TotalAmountWithGST = totamt.ToString();

                    var stringPayload = JsonConvert.SerializeObject(receiptModel);
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("api/AddReceipt", content).Result;

                     if(response.StatusCode == HttpStatusCode.OK)
                     {
                        var jasonResult = response.Content.ReadAsStringAsync().Result;
                        var responseObject = (APIResult)JsonConvert.DeserializeObject<APIResult>(jasonResult);
                        DependencyService.Get<Toast>().Show("Receipt Submitted Successfully");
                        Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new Home_Page());
                    }
                    else
                    {                   
                        DependencyService.Get<Toast>().Show("Issue occured!");
                    }
   

                }
                catch(Exception e1)
                {
                    DependencyService.Get<Toast>().Show("Issue occured!");
                }


            }
        }

        public class ReceiptModel
        {
            public string ReceiptImageBase64String { get; set; }
            public string Category { get; set; }
            public string TotalAmountWithGST { get; set; }
            public string GSTAmount { get; set; }
            public string Account { get; set; }
        }

      
        public static async Task<T> PostResponseLogin<T>(string weburl, FormUrlEncodedContent content) where T : class
        {
            var response = await HttpClient.PostAsync(weburl, content);
            var jasonResult = response.Content.ReadAsStringAsync().Result;
            var responseObject = (T)JsonConvert.DeserializeObject<T>(jasonResult);            
            return responseObject;
        }


        public async Task<T> PostResponse<T>(string weburl, string jasonstring) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
            string ContentType = "application/json";
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.access_token);
            try
            {
                var Result = await HttpClient.PostAsync(weburl, new StringContent(jasonstring, Encoding.UTF8, ContentType));
                if (Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var JsonResult = Result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResp = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResp;
                    }
                    catch
                    { return null; }
                }
            }
            catch { return null; }
            return null;
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.access_token);
            try
            {
                var response = await HttpClient.GetAsync(weburl);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var JsonResult = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResp = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResp;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }



        public async Task<APIResultRegister> Register(RegisterUser reguser)
        {
            APIResultRegister responseObject =new APIResultRegister();

            try
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("FirstName", reguser.FirstName));
                postData.Add(new KeyValuePair<string, string>("LastName", reguser.LastName));
                postData.Add(new KeyValuePair<string, string>("City", reguser.City));
                postData.Add(new KeyValuePair<string, string>("Address", reguser.Address));
                postData.Add(new KeyValuePair<string, string>("Address2", reguser.Address2));
                postData.Add(new KeyValuePair<string, string>("ZipCode", reguser.ZipCode));
                postData.Add(new KeyValuePair<string, string>("PhoneNumber", reguser.PhoneNumber));
                postData.Add(new KeyValuePair<string, string>("Email", reguser.Email));
                postData.Add(new KeyValuePair<string, string>("Password", reguser.Password));
                postData.Add(new KeyValuePair<string, string>("ConfirmPassword", reguser.ConfirmPassword));


                var content = new FormUrlEncodedContent(postData);
                var weburl = "http://ausiaccounts.azurewebsites.net/api/registeruser";

                var response = await HttpClient.PostAsync(weburl, content);
                var jasonResult = response.Content.ReadAsStringAsync().Result;
                //var responseObject = (APIResultRegister)JsonConvert.DeserializeObject<APIResultRegister>(jasonResult);

                 responseObject = JsonConvert.DeserializeObject<APIResultRegister>(jasonResult);

                //if (response.StatusCode == HttpStatusCode.OK)
                //responseObject.IsAuthenticated = true;
                //user_token = responseObject.Message;

               
            }
            catch (Exception er)
            {
                responseObject.Message = "Error Occured, Please try again";
            }
            return responseObject;
        }




    }

    /// <summary>
    /// API Result
    /// </summary>
    public class APIResult
    {
        /// <summary>
        /// Message when success API call  or failed api call
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Is Authenticated to check the status.
        /// </summary>
        public bool IsAuthenticated { get; set; }
    }

    public class APIResultRegister
    {
        /// <summary>
        /// Message which will return when Register User API is successfully called or failed
        /// </summary>
        //public string Message { get; set; }
        public string Message { get; set; }
    }

    public class APIResultRegisterList
    {
        public List<APIResultRegister> Message { get; set; }
    }



}
