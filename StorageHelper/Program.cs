using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;


namespace StorageHelper
{
    class Program
    { 
        public static void GetBlobContainer(string account, string key, string container)
        {
            StorageCredentials storageCredentials = new StorageCredentials(account, key);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: false);

            // Create a CloudBlobClient object from the storage account.
            // This object is the root object for all operations on the 
            // blob service for this particular account. 
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            // Get a reference to a CloudBlobContainer object in this account. 
            // This object can be used to create the container on the service, 
            // list blobs, delete the container, etc. This operation does not make a 
            // call to the Azure Storage service.  It neither creates the container 
            // on the service, nor validates its existence.
            foreach(var blobContainer in blobClient.ListContainers())
            {
                Console.WriteLine(blobContainer.Name);
            }
        }

        public static void ConnectToStorageAccount()
        {
            string key = "6bkZwHIBNAzjpNt2y0jdYxwO3+U8FWauoose3Q67sRrLtvbZV3llgL8iNfkq047iqeTdQEdPoRuKtrhfQrpwxw==";
            string account = "thshanmuswift6";
            string container = "storagedomain-default";

            GetBlobContainer(account, key, container);
        }

        public static void ValidateStorageAccount()
        {
            try
            {
                string storageString  = "DefaultEndpointsProtocol=https;AccountName=thshanmuswift6;AccountKey=6bkZwHIBNAzjpNt2y0jdYxwO3+U8FWauoose3Q67sRrLtvbZV3llgL8iNfkq047iqeTdQEdPoRuKtrhfQrpwxw==";
                string subscriptionId = "2787434c-a76e-41bc-8400-54ae7ae69e16";

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageString);
                string accountName = storageAccount.Credentials.AccountName;

                //string storageLocation = "westus2";
                
                CloudBlobClient client = storageAccount.CreateCloudBlobClient();
                try
                {
                    IEnumerable<CloudBlobContainer> containers = client.ListContainers();
                    Console.WriteLine("Listing containers for {0}", accountName);
                    foreach (var container in containers)
                    {
                        Console.WriteLine(container.Name);
                    }
                    Console.WriteLine("List containers for {0} succeed", accountName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to access storage: {0}", ex);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Validating storage account failed: {0}", ex);
            }
        }

        static void Main(string[] args)
        {
            ValidateStorageAccount();
            //ConnectToStorageAccount();
        }
    }
}