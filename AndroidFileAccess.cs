using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFileAccess))]
namespace portal.PortalExInteractive.Droid
{
    public class AndroidFileAccess : IMobileFileAccess
    {
        public void SaveFile(string fileName, byte[] data)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            File.WriteAllBytes(filePath, data);
        }

        public byte[] LoadFileBytes(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            return File.ReadAllBytes(filePath);
        }

        public Stream LoadFileStream(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            return ! File.Exists(filePath) ? null : File.OpenRead(filePath);
        }
    }
}