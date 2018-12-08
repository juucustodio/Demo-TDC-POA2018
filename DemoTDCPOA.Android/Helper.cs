using System;
using System.IO;
using DemoTDCPOA.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Helper))]
namespace DemoTDCPOA.Droid
{
    public class Helper : IHelper
    {
        public string GetFilePath(string file)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, file);
        }
    }
}
