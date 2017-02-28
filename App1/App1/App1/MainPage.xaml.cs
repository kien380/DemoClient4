using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
        /// <summary>
        /// Is Menu in show?
        /// </summary>
        private bool IsMenuPresented = false;

        public double InitTranslationXLeftMenuView { get; set; }


        ///// <summary>
        ///// Is Right Menu in show?
        ///// </summary>
        //public bool IsRightMenuPresented { get; set; } = false;

        public MainPage()
        {
            // Hide navigation bar
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            InitTranslationXLeftMenuView = -this.Width;
            BindingContext = this;

            InitUI();
            AddClickListener();
        }

        private void InitUI()
        {
            //// Rotate _LeftMenuIcon
            //_LeftMenuIcon.Rotation = 180;

            //// Set _RightMenuSide
            ////_RightMenuSide.BackgroundColor = Color.FromRgba(0, 0, 0, 0.5);
            //_RightMenuSide.FadeTo(0, 0);

            //_LeftMenuView.TranslateTo((int)-(this.Width - 40), 0, 0);

            ListOptions.ItemsSource = new List<ListOptionsModal>
            {
                new ListOptionsModal
                {
                    imgSource = "home.png",
                    text = "Explore"
                },
                new ListOptionsModal
                {
                    imgSource = "list.png",
                    text = "Activity"
                },
                new ListOptionsModal
                {
                    imgSource = "cart.png",
                    text = "Cart"
                },
                new ListOptionsModal
                {
                    imgSource = "settings.png",
                    text = "Settings"
                },
                new ListOptionsModal
                {
                    imgSource = "exit.png",
                    text = "Log out"
                },
            };
        }

        private void AddClickListener()
        {
            // Add Tap Gesture for _LeftMenuIcon
            TapGestureRecognizer tapLeftMenuIcon = new TapGestureRecognizer();
            tapLeftMenuIcon.Tapped += (s, e) => OnClickLeftMenuIcon();
            _LeftMenuIcon.GestureRecognizers.Add(tapLeftMenuIcon);

            //// Add Tap Gesture for _RightMenuIcon
            //TapGestureRecognizer tapRightMenuIcon = new TapGestureRecognizer();
            //tapRightMenuIcon.Tapped += (s, e) => OnClickRightMenuIcon();
            //_RightMenuIcon.GestureRecognizers.Add(tapRightMenuIcon);

            //// Add Tap Gesture when tap on _RightMenuSide
            //_RightMenuSide.GestureRecognizers.Add(tapRightMenuIcon);
        }

        private void OnClickLeftMenuIcon()
        {
            if (!IsMenuPresented)
            {
                // Slide _DetailView to the right
                _DetailView.TranslateTo(this.Width - 40, 0, 400, Easing.CubicOut);

                // Slide _LeftMenuView to the right
                _LeftMenuView.TranslateTo(0, 0, 400, Easing.CubicOut);

                // Rotate _LeftMenuIcon
                _LeftMenuIcon.RotateTo(0, 50);
            }
            else
            {
                // Slide _DetailView to the left
                _DetailView.TranslateTo(0, 0, 400, Easing.CubicOut);

                // Slide _LeftMenuView to the left
                _LeftMenuView.TranslateTo(-(this.Width - 40), 0, 400, Easing.CubicOut);

                // Rotate _LeftMenuIcon
                _LeftMenuIcon.RotateTo(180, 50);
            }

            // Reset IsPresented value
            IsMenuPresented = !IsMenuPresented;
        }

        //private async void OnClickRightMenuIcon()
        //{
        //    if (!IsRightMenuPresented)
        //    {
        //        // Rotate _LeftMenuIcon
        //        //_RightMenuIcon.RotateTo(180, 50);

        //        // Slide _RightMenuView and and put _RightMenuSide to the left
        //        _RightMenuView.TranslateTo(0, 0, 400, Easing.CubicOut);
        //        _RightMenuSide.TranslateTo(0, 0, 0);

        //        // Show _RightMenuSide
        //        _RightMenuSide.FadeTo(0.8, 200);
        //    }
        //    else
        //    {
        //        // Rotate _LeftMenuIcon
        //        //_RightMenuIcon.RotateTo(0, 50);

        //        // Slide _RightMenuView to the Right
        //        _RightMenuView.TranslateTo(this.Width, 0, 400, Easing.CubicOut);

        //        // Hide _RightMenuSide
        //        await _RightMenuSide.FadeTo(0, 200);

        //        // Put _RightMenuSide to the Right
        //        _RightMenuSide.TranslateTo(this.Width, 0, 0);
        //    }

        //    // Reset IsPresented value
        //    IsRightMenuPresented = !IsRightMenuPresented;
        //}
    }
}
