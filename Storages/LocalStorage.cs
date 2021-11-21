using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Imagegram.Functions.Storages
{
    public class LocalStorage : IStorage
    {
        public Task<string> Upload(byte[] resource)
        {
            using (var ms = new MemoryStream(resource))
            {
                Image img = Image.FromStream(ms);

                string targetDirectory = @$"{Directory.GetCurrentDirectory()}\LocalStorages\Images\";
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string endpoint = Path.Combine(targetDirectory, $"{Guid.NewGuid()}.jpg");
                img.Save(endpoint, ImageFormat.Jpeg);
                img.Dispose();

                return Task.FromResult(endpoint);
            }
        }
    }
}
