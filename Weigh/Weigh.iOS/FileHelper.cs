using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigh.Data;
using Weigh.iOS;
using Foundation;
using UIKit;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Weigh.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetPath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
