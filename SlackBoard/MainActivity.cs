using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace SlackBoard
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#9ca9b5")));
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            

            Button loginBtn = (Button)FindViewById(Resource.Id.loginInBtn);

            loginBtn.Click += delegate
            {
                var mainMenu = new Intent(this, typeof(MainMenu));
                StartActivity(new Intent( mainMenu));
                
            };
        }
    }
}

