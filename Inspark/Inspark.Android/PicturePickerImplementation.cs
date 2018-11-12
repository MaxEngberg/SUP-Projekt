using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Inspark.Droid;
using Inspark.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePickerImplementation))]

namespace Inspark.Droid
{
    class PicturePickerImplementation //IPhotoPicker
    {
        //public Task<Stream> GetImageStreamAsync()
        //{
        //    Intent intent = new Intent();
        //    intent.SetType("image/*");
        //    intent.SetAction(Intent.ActionGetContent);

        //    // Start the picture-picker activity (resumes in MainActivity.cs)
        //    MainActivity.Instance.StartActivityOnResult(
        //    Intent.CreateChooser(intent, "Select Picture"),
        //    MainActivity.PickImageId);

        //    // Save the TaskCompletionSource object as a MainActivity property
        //    MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

        //    // Return Task object
        //    return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        //}
    }
}