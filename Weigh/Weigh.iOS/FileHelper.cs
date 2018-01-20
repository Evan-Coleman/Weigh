using System;
using System.IO;
using Weigh.Data;
using Weigh.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace Weigh.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetPath(string filename)
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder)) Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, filename);
        }
    }
}