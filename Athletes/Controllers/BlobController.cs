using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.Configuration;


namespace Athletes.Controllers
{
    public class BlobController : Controller
    {

        static CloudBlobClient blobClient;
        const string blobContainerName = "athleteprofilepicture";
        static CloudBlobContainer blobContainer;
        public async Task<ActionResult> Index()
        {
            try
            {
                // Retrieve storage account information from the connection string
                // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

                blobClient = storageAccount.CreateCloudBlobClient();
                blobContainer = blobClient.GetContainerReference(blobContainerName);
                await blobContainer.CreateIfNotExistsAsync();

                // To view the uploaded blob in the browser, there are two options. First, use a Shared Access Signature (SAS) token to 
                // delegate access to the resource. Second, set permissions to allow public access to blobs in this container. Comment the
                // line below to not use this approach and to use SAS. Then you can view the image using
                // https://[InsertYourStorageAccountNameHere].blob.core.windows.net/webappstoragedotnet-imagecontainer/FileName
                // This line is to be commented out if SAS is not to be used
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Gets all Cloud Block Blobs in the blobContainerName and passes them to the view
                List<Uri> allBlobs = new List<Uri>();
				CloudBlockBlob blob = blobContainer.GetBlockBlobReference("1637553056123023559_a29cc6d3-672c-49d2-95e8-6fe23c833873.jpeg");
				// use blobContainer.GetBlob or something to get one image vs a list
				//            foreach (IListBlobItem blob in blobContainer.ListBlobs())
				//{
				//	if (blob.GetType() == typeof(CloudBlockBlob))
				//		allBlobs.Add(blob.Uri);
				//}
				allBlobs.Add(blob.Uri);
				return View(allBlobs);
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View();
            }
        }


		// Task<ActionResult> UploadAsync()
		// UploadFromFileAsync Method: https://msdn.microsoft.com/en-us/library/azure/microsoft.windowsazure.storage.blob.cloudpageblob.uploadfromfileasync.aspx
		

		// Task<ActionResult> DeleteImage(String name)
		// Delete Blobs: https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#delete-blobs

		[HttpPost]
        public async Task<ActionResult> DeleteImage(string name)
        {
            try
            {
                Uri uri = new Uri(name);
                string filename = Path.GetFileName(uri.LocalPath);

                var blob = blobContainer.GetBlockBlobReference(filename);
                await blob.DeleteIfExistsAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
        }

        // Task<ActionResult> DeleteAll(string name)
        // Delete Blobs: https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#delete-blobs

        [HttpPost]
        public async Task<ActionResult> DeleteAll()
        {
            try
            {
                foreach (var blob in blobContainer.ListBlobs())
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        await ((CloudBlockBlob)blob).DeleteIfExistsAsync();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
        }

        // string GetRandomBlobName(string filename): Generates a unique randomfile name to be uploaded

        
    }
}