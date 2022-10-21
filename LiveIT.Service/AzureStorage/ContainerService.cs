using Common.Constants;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage
{
    public class ContainerService
    {
        public static async Task<bool> UploadOperationAsync(string directory, string name, string azureGPV2Credentials)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(AzureGPV2Credentials.STORAGE_ACCOUNT_CONNECTION_STRING);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                var cloudBlobContainer = cloudBlobClient.GetContainerReference(azureGPV2Credentials);
                var permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                string sourceFile = Path.Combine(directory, name);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(name);
                await cloudBlockBlob.UploadFromFileAsync(sourceFile);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
