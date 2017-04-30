using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
        

        public MainPage()
        {
            // Hide navigation bar
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            InitTranslationXLeftMenuView = -this.Width;
            BindingContext = this;

            InitUI();
        }

        private void InitUI()
        {
            // Init TapGesture on Left menu icon
            var tapLeftMenu = new TapGestureRecognizer();
            tapLeftMenu.Tapped += (se, ev) => { OnClickLeftMenuIcon(); };
            _LeftMenuIcon.GestureRecognizers.Add(tapLeftMenu);

            Test();
        }

        private void Test()
        {
            StudioPicker.Items.Add("Lotte");
            StudioPicker.Items.Add("CGV");
            StudioPicker.Items.Add("Galaxy");
            StudioPicker.Items.Add("BHD");
            StudioPicker.SelectedIndex = 0;
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
    }
}
