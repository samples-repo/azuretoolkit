using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;
using Microsoft.WindowsAzure.Storage.Auth;
using AzureToolkit.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Diagnostics;

namespace AzureToolkit.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController: Controller
    {
        private CloudBlobContainer _container;
        private AzureToolkitContext _context;

        public ImagesController(AzureToolkitContext context)
        {
            _context = context; 
            
            var storageAccount = new CloudStorageAccount(
                new StorageCredentials(
                    "azuretktstorage",
                    "JbDRkZ+v5/M48J53UQz53EM5jzEOXRzDtSKxy0z1rfgTLpRI0s48p1T+7lDM9kwCtzfH9UtfPgzSJroYE5hsKg=="
                ), true
            );

            // create a blob client
            var blobClient = storageAccount.CreateCloudBlobClient();
            _container = blobClient.GetContainerReference("savedimages");
        }

        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody]ImagePostRequest request)
        {
            // upload image
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{request.Id}.{request.EncodingFormat}");
            HttpWebRequest aRequest = (HttpWebRequest)WebRequest.Create(request.URL);
            HttpWebResponse aResponse = (await aRequest.GetResponseAsync()) as HttpWebResponse;
            var stream = aResponse.GetResponseStream();
            await blockBlob.UploadFromStreamAsync(stream);
            stream.Dispose();

            // save metadata
            var savedImage = new SavedImage{
                UserId = request.Id,
                Description = request.Description,
                StorageUrl = blockBlob.Uri.ToString(),
                Tags = new List<SavedImageTag>(),
            };
            foreach (var tag in request.Tags)
            {
                savedImage.Tags.Add(new SavedImageTag{Tag = tag});
            }

            _context.Add(savedImage);
            _context.SaveChanges();

            return Ok();
        }

        //[HttpGet("{userId}")]
        //public IActionResult GetImages(string userId)
        [HttpGet]
        public IActionResult GetImages()
        {
            //var images = _context.SavedImages.Where(img => img.UserId == userId).ToList();
            var images = _context.SavedImages.ToList();
            return Ok(images);
        }

        [HttpGet("search/{userId}/{term}")]
        public IActionResult SearchImages(string userId, string term)
        {
            string searchServiceName ="azuretoolkit-search";
            string queryApiKey = "4CAC13492C775D5502C254A0F55F9D87";

            DocumentSearchResult<SavedImage> results = null;

            try
            {
                SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, "description", new SearchCredentials(queryApiKey));

                SearchParameters parameters = new SearchParameters() { Filter = $"UserId eq '{userId}'" };
                //DocumentSearchResult<SavedImage> results = indexClient.Documents.Search<SavedImage>(term, parameters);
                results = indexClient.Documents.Search<SavedImage>(term);
                
            }
            catch (Exception ex)
            {                
                Debug.WriteLine($".........{ex.ToString()}");
            }
            
            return Ok(results.Results.Select((savedImage) => savedImage.Document));    
        }
    }
}