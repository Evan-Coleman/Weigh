using System;
using System.IO;
using Weigh.Data;
using Weigh.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace Weigh.Droid
{
    internal class FileHelper : IFileHelper
    {
        public string GetPath(string fileName)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}