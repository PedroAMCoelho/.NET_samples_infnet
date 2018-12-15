using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialNetwork.Blobs
{
    public class AzureBlobs
    {
        private CloudBlobContainer _blobContainer;
        private static readonly string _connectionString;

        public AzureBlobs()
        {
            //client
            CloudBlobClient blobClient =
                CloudStorageAccount
                        .Parse("DefaultEndpointsProtocol=https;AccountName=blobatazure;AccountKey=gbLcZNe4Pj9Q0C5aCLuQFoi9J+mMVHdF/iFmXPKAq4/qy79EUNcSOkcVpWAwpAglV2u07liBhShaRHLT9C7K+w==;EndpointSuffix=core.windows.net")
                        .CreateCloudBlobClient();

            //container referece
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("containersocialnetworkpics");

            blobContainer.CreateIfNotExists();

            //Instantiating a class through container permissions
            BlobContainerPermissions containerPermissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            };

            blobContainer.SetPermissions(containerPermissions);

            _blobContainer = blobContainer;
        }


        public async Task<string> UploadFile(HttpPostedFileBase file)
        {
            //timestamp approach, so that the pics do not have the same name
            string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

            string fileName = Path.GetFileName(file.FileName + timestamp);

            //Instantiating a blockblob upon a container reference
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);

            using (var s = file.InputStream)
            {

                await blob.UploadFromStreamAsync(s);

                s.Dispose();
            }

            // changing metadata
            blob.Metadata.Add("FileName", fileName);
            blob.Metadata.Add("FileExtension", file.ContentType);
            await blob.SetMetadataAsync();

            // setting blob property
            blob.Properties.ContentType = file.ContentType;
            await blob.SetPropertiesAsync();

            //this line returns all the file's path after the upload
            return blob.Uri.ToString();
        }

        public async Task DeleteFile(string filename)
        {
            //Instantiating blob
            CloudBlockBlob blob = new CloudBlockBlob(new Uri(filename));

            CloudBlockBlob blockblob = _blobContainer.GetBlockBlobReference(blob.Name);

            await blockblob.DeleteIfExistsAsync();

        }


    }
}

