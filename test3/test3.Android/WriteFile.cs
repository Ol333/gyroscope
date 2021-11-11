using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(test3.Droid.WriteFile))]
namespace test3.Droid
{
    class WriteFile : IWriteFile
    {
        public void MyWriteTxtFile(string s)
        {
            //throw new NotImplementedException();
            var filename = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, "log.txt");
            if (File.Exists(filename))
                File.AppendAllText(filename, s);
            else
                File.WriteAllText(filename, s);
        }
    }
}