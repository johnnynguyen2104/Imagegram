using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagegram.Functions.Storages
{
    public class S3Storage : IStorage
    {
        public Task<string> Upload(byte[] resource)
        {
            throw new NotImplementedException();
        }
    }
}
