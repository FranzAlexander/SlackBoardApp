using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using SlackBoard.Modules;
using Android.Content.PM;
using System;

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

            loginBtn.Click += ValidateLogin;

        }

        private async void ValidateLogin(object sender, System.EventArgs e)
        {
            RestService service = new RestService();

            EditText username = FindViewById<EditText>(Resource.Id.usernameTxt);
            EditText password = FindViewById<EditText>(Resource.Id.passwordTxt);

            Token key = new Token();

            if (!(string.IsNullOrWhiteSpace(username.Text) && string.IsNullOrWhiteSpace(password.Text)))
            {
                try
                {
                    bool isValid = await service.ValidateLoginAsync(username.Text, password.Text);

                    var tokenFile = Application.Context.GetSharedPreferences("token", FileCreationMode.Private).GetString("token", key.authorizationToken);

                    if (isValid)
                    {
                        StartActivity(typeof(MainMenu));
                    }
                    else
                    {
                        Toast.MakeText(this, "Your details are incorrect. Try again.", ToastLength.Long).Show();
                        username.Text = "";
                        password.Text = "";
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(this, "Something went wrong.", ToastLength.Short).Show();
                }
            }
            else if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                Toast.MakeText(this, "Please enter the required credentials to login.", ToastLength.Long).Show();
            }
        }
    }
}

