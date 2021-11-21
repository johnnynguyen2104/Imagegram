using System;
using System.Threading.Tasks;

namespace Imagegram.Functions.Storages
{
    public class AzureBlobStorage : IStorage
    {
        /// <summary>
        /// Upload the resource to azure blob storage.
        /// </summary>
        /// <param name="resource">Resource</param>
        /// <returns>Return the resource's endpoint.</returns>
        public Task<string> Upload(byte[] resource)
        {
            throw new NotImplementedException();
        }
    }
}
