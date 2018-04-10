using System;
using System.IO;
using Eshop.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Eshop.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
