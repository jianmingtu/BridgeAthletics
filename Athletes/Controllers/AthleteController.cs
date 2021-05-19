using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Athletes.DAL;
using Athletes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNet.Identity.Owin;


namespace Athletes.Controllers
{
    public class AthleteController : Controller
    {
        private AthletesContext db = new AthletesContext();

        // GET: Athlete
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            Athlete athlete = db.Athletes.Where(a => a.Id == userId).FirstOrDefault();

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

                // ----- get whole image url from Azure to render existing profile image on athlete profile page ----- 
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(athlete.ImgUrl);

                return View(blob.Uri);
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View();
            }
        }

        [Authorize]
        public ActionResult Browse(string AthleteGender, string AthletePosition, string searchString)
        {
            var GenderList = new List<string>(); //creates variable for a list of string 
            var PositionList = new List<string>();//creates variable for a list of string 

            var GenderQry = from d in db.Athletes
                            orderby d.Gender
                            where d.Gender != ""
                            select d.Gender;
            //checks db for athletes, orderes by gender 
            var PositionQry = from d in db.Athletes
                              orderby d.Position
                              where d.Position != ""
                              select d.Position;
            //checks db for athletes, orderes positions
            
            GenderList.AddRange(GenderQry.Distinct());
            ViewBag.athleteGender = new SelectList(GenderList);
            //generates a dynmic list based on Genders in the DB

            PositionList.AddRange(PositionQry.Distinct());
            ViewBag.athletePosition = new SelectList(PositionList);
            //generates a dynmic list based on Positions in the DB

            var athletes = from m in db.Athletes
                           select m;

            athletes = athletes.Where(user => !string.IsNullOrEmpty(user.Gender) && !string.IsNullOrEmpty(user.Height) && !string.IsNullOrEmpty(user.Position));

            if (!string.IsNullOrEmpty(searchString)) //check for a null value 
            {
                athletes = athletes.Where(s => (s.FirstName.Contains(searchString) || s.LastName.Contains(searchString)));
            }

            if (!string.IsNullOrEmpty(AthleteGender)) //check for a null value 
            {
                athletes = athletes.Where(x => x.Gender == AthleteGender);
            }

            if (!string.IsNullOrEmpty(AthletePosition)) //check for a null value 
            {
                athletes = athletes.Where(x => x.Position == AthletePosition);
            }

            return View(athletes);
        }

    
		static CloudBlobClient blobClient;
		const string blobContainerName = "athleteprofilepicture";
		static CloudBlobContainer blobContainer;

		private ApplicationUserManager _userManager;

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

        // ---- Renders Azure Image to Upload page ----
        [Authorize]
        public async Task<ActionResult> Upload() 
        {
            var userId = User.Identity.GetUserId();

            Athlete athlete = db.Athletes.Where(a => a.Id == userId).FirstOrDefault();

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

                // ----- get whole image url from Azure to render existing profile image on upload page -----
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(athlete.ImgUrl);

                return View(blob.Uri);
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View();
            }
        }

        [Authorize]
        [HttpPost]
		public async Task<ActionResult> UploadAsync()
		{
			var userId = User.Identity.GetUserId();

			Athlete athlete = db.Athletes.Where(a => a.Id == userId).FirstOrDefault();

			try
			{

				HttpFileCollectionBase files = Request.Files;
                var file = files[0];

                var source = files[0].InputStream;

                // ------ create meaningful dynamic img file name ([userid]-profile-img.[file extension]) ------
                athlete.ImgUrl = GetRandomBlobName(userId, file);

                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(athlete.ImgUrl);
                await blob.UploadFromStreamAsync(source);

                // ------ save athlete imgUrl to db ------
                db.SaveChanges();

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewData["message"] = ex.Message;
				ViewData["trace"] = ex.StackTrace;
				return View("Error");
			}
		}
        // Use in Register function
        // ------ create dynamic img file name ------
        private string GetRandomBlobName(string userId, HttpPostedFileBase source)
		{
			string ext = Path.GetExtension(source.FileName);
            var filename = userId + "-profile-img" + ext;
            return filename;
        }
	}
}        
