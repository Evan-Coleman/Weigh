using System.IO;
using Windows.Storage;
using Weigh.Data;
using Weigh.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace Weigh.UWP
{
    internal class FileHelper : IFileHelper
    {
        public string GetPath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
    }
}