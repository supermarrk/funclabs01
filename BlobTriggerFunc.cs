using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;

namespace funclabs01
{
    public class BlobTriggerFunc
    {
        private readonly ILogger<BlobTriggerFunc> _logger;

        public BlobTriggerFunc(ILogger<BlobTriggerFunc> logger)
        {
            _logger = logger;
        }

        [Function(nameof(BlobTriggerFunc))]
        public async Task Run([BlobTrigger("blobstriggercontainer/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: ");

            var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            var blobServiceClient = new BlobServiceClient(connectionString); // Storage Account
            var containerClient = blobServiceClient.GetBlobContainerClient("backup"); // Container
            await containerClient.CreateIfNotExistsAsync();
            var outputBlob = containerClient.GetBlobClient(name); // Blob Copy
            using var outputBlobStream = new MemoryStream();
            _logger.LogInformation($"Copying {name} to backup..");
            // outputBlob.Write(content);
            outputBlobStream.Position = 0; // Reset stream position to the beginning
            await outputBlob.UploadAsync(outputBlobStream, overwrite: true); // Blob Copy to Backup container
            _logger.LogInformation($"Uploaded {name} to backup..");
        }
    }
}
