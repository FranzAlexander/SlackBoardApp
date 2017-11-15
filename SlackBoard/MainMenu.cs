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

using Android.Graphics;

using SlackBoard.Fragments;
using Android.Graphics.Drawables;

namespace SlackBoard
{
    [Activity(Label = "MainMenu")]

    class MainMenu : Activity
    {
        public Fragment currentFragment;
        Stack<Fragment> fragmentStack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#9ca9b5")));
            ActionBar.SetStackedBackgroundDrawable(new ColorDrawable(Color.ParseColor("#c8cfd6")));


            SetContentView(Resource.Layout.MainMenu);

            CourseFragment courseFragment = new CourseFragment(); ;
            AssessmentFragment assessmentFragment = new AssessmentFragment();
            ChatFragment chatFragment = new ChatFragment();

            fragmentStack = new Stack<Fragment>();

            FragmentTransaction changeFragment = this.FragmentManager.BeginTransaction();
            changeFragment.Add(Resource.Id.frameLayout1, courseFragment);
            changeFragment.Add(Resource.Id.frameLayout1, assessmentFragment);
            changeFragment.Add(Resource.Id.frameLayout1, chatFragment);
            changeFragment.Hide(assessmentFragment);
            changeFragment.Hide(chatFragment);
            changeFragment.Commit();

            currentFragment = courseFragment;

            var tab = this.ActionBar.NewTab();
            tab.SetText("Courses");
            
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                ShowFragment(courseFragment);
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Assessments");
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                ShowFragment(assessmentFragment);
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Chat");
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            { 
                ShowFragment(chatFragment);
            };
            ActionBar.AddTab(tab);

        }

        private void ShowFragment(Fragment fragment)
        {
            FragmentTransaction changeFragment = this.FragmentManager.BeginTransaction();

            changeFragment.Hide(currentFragment);
            changeFragment.Show(fragment);
            changeFragment.AddToBackStack(null);
            changeFragment.Commit();

            fragmentStack.Push(currentFragment);
            currentFragment = fragment;
        }

        public override void OnBackPressed()
        {
            if (FragmentManager.BackStackEntryCount > 0)
            {
                FragmentManager.PopBackStack();
                currentFragment = fragmentStack.Pop();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}