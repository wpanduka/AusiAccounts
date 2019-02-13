using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ADB2CAuthorization
{
    public class BlobStorageService
    {
        readonly static CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=wpanduka;AccountKey=a7yreWgAA3kUiG4u2igZ1KaJJD1Ss2P97B9o39GKTb9vmIRL0SsVVEiTmHxuPzPE8CLyK/h+UFc5OITP8f+UiQ==;EndpointSuffix=core.windows.net");
        readonly static CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();

        public static async Task<List<T>> GetBlobs<T>(string containerName, string prefix = "", int? maxresultsPerQuery = null, BlobListingDetails blobListingDetails = BlobListingDetails.None) where T : ICloudBlob
        {
            var blobContainer = _blobClient.GetContainerReference(containerName);

            var blobList = new List<T>();
            BlobContinuationToken continuationToken = null;

            try
            {
                do
                {
                    var response = await blobContainer.ListBlobsSegmentedAsync(prefix, true, blobListingDetails, maxresultsPerQuery, continuationToken, null, null);

                    continuationToken = response?.ContinuationToken;

                    foreach (var blob in response?.Results?.OfType<T>())
                    {
                        blobList.Add(blob);
                    }

                } while (continuationToken != null);
            }
            catch (Exception e)
            {
                //Handle Exception
            }

            return blobList;
        }

        public static async Task<CloudBlockBlob> SaveBlockBlob(string containerName, byte[] blob, string blobTitle)
        {
            var blobContainer = _blobClient.GetContainerReference(containerName);

            var blockBlob = blobContainer.GetBlockBlobReference(blobTitle);
            await blockBlob.UploadFromByteArrayAsync(blob, 0, blob.Length);

            return blockBlob;
        }
    }
}
