using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SlackBoard
{
    [Activity(Label = "ListViews")]
    public class ListViews : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string[] courseNames = { "English", "Math", "Sport", "Computer Science", "Physics" };

            base.OnCreate(savedInstanceState);

            ListAdapter = new ArrayAdapter<string>(this,Resource.Layout.AssessmentFragmentLayout,courseNames);
            ListView.TextFilterEnabled = true;
            // Create your application here
        }
    }
}