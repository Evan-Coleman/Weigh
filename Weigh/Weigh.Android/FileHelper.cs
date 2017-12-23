using Weigh.Droid;
using Weigh.Data;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace Weigh.Droid
{
    class FileHelper : IFileHelper
    {
        public string GetPath(string fileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}
