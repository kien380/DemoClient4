using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
	{
        #region VARIABLES DECLARETION
        /// <summary>
        /// Is Menu in show?
        /// </summary>
        private bool IsMenuPresented = false;

        public double InitTranslationXLeftMenuView { get; set; }
        #endregion

        #region CONSTRUCTORS
        public MainPage()
        {
            // Hide navigation bar
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            InitTranslationXLeftMenuView = -this.Width;
            BindingContext = this;

            InitUI();
        }
        #endregion

        #region METHODS
        private void InitUI()
        {
            ListMenuOption.ItemsSource = new List<MenuOption>()
            {
                new MenuOption { Text = "Movie Schedule", ImageSource = "list.png" },
                new MenuOption { Text = "Ticket Price", ImageSource = "price_tag.png" }
            };

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
        #endregion

        #region EVENT HANDLE
        private void StudioPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change Studio logo
            switch(StudioPicker.SelectedIndex)
            {
                case 0: StudioLogo.Source = "lotte_logo.png"; break;
                case 1: StudioLogo.Source = "cgv_logo.png"; break;
                case 2: StudioLogo.Source = "galaxy_logo.png"; break;
                case 3: StudioLogo.Source = "bhd_logo.png"; break;
                default: StudioLogo.Source = "lotte_logo.png"; break;
            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListMenuOption_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListMenuOption.SelectedItem = null;
            var item = e.Item as MenuOption;
            if(item.Text.Equals("Movie Schedule"))
            {
                MovieScheduleView.IsVisible = true;
                TicketPriceView.IsVisible = false;
            }
            else
            {
                MovieScheduleView.IsVisible = false;
                TicketPriceView.IsVisible = true;
            }
            HeaderTitle.Text = item.Text;
            OnClickLeftMenuIcon();
        }
        #endregion
    }
}
