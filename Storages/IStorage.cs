using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagegram.Functions.Storages
{
    public interface IStorage
    {
        Task<string> Upload(byte[] resource);
    }
}
