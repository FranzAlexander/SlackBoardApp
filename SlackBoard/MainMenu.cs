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
using Android.Animation;

using Android.Graphics;

using SlackBoard.Fragments;
using Android.Graphics.Drawables;
using Android.Views.Animations;

namespace SlackBoard
{
    [Activity(Label = "Prototype")]

    class MainMenu : Activity
    {
        public Fragment currentFragment;
        Stack<Fragment> fragmentStack;
         
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            //Setting the action bar navigation mode to tabs. Also setting colors of the tabs
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#9ca9b5")));
            ActionBar.SetStackedBackgroundDrawable(new ColorDrawable(Color.ParseColor("#c8cfd6")));
          
            SetContentView(Resource.Layout.MainMenu);

            //Declaring the fragments.
            CourseFragment courseFragment = new CourseFragment(); ;
            AssessmentFragment assessmentFragment = new AssessmentFragment();
            ChatFragment chatFragment = new ChatFragment();

            //Adding the fragments to a stack.
            fragmentStack = new Stack<Fragment>();

            //Adding the fragments to a fragment manager and then commiting them.
            FragmentTransaction changeFragment = this.FragmentManager.BeginTransaction();
            changeFragment.Add(Resource.Id.frameLayout1, courseFragment);
            changeFragment.Add(Resource.Id.frameLayout1, assessmentFragment);
            changeFragment.Add(Resource.Id.frameLayout1, chatFragment);
            changeFragment.Hide(assessmentFragment);
            changeFragment.Hide(chatFragment);
            changeFragment.Commit();

            //Setting the current fragment.
            currentFragment = courseFragment;

            //Creating the tabs.
            var tab = this.ActionBar.NewTab();
            tab.SetText("Courses");

            //Tab OnClick event handler.
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

        //Changing the fragments.
        private void ShowFragment(Fragment fragment)
        {
            //Declaring fragment manager.
            FragmentTransaction changeFragment = this.FragmentManager.BeginTransaction();

            //Hiding, Showing and adding fragment to stack.
            changeFragment.Hide(currentFragment);
            changeFragment.Show(fragment);
            changeFragment.AddToBackStack(null);
            changeFragment.Commit();

            //Push the fragment on the stack.
            fragmentStack.Push(currentFragment);
            //Changing current stack.
            currentFragment = fragment;
        }

        //Override for OnBackPressed method to handle fragments.
        public override void OnBackPressed()
        {
            //Making sure that the fragments are poped off the stack on the back press.
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