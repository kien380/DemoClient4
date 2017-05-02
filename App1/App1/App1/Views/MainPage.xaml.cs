using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        private List<Studio> Studios = new List<Studio>();
        private List<Cinema> Cinemas = new List<Cinema>();
        private List<LichChieu> DSLichChieu = new List<LichChieu>();

        private bool IsInitting = true;
        private bool IsStudioChanging = false;
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
            InitVariable();
        }
        #endregion

        #region METHODS
        private void InitUI()
        {
            Pickers.IsVisible = false;
            MovieScheduleView.IsVisible = false;

            ListMenuOption.ItemsSource = new List<MenuOption>()
            {
                new MenuOption { Text = "Lịch Chiếu", ImageSource = "list.png" },
                new MenuOption { Text = "Giá Vé", ImageSource = "price_tag.png" }
            };

            // Init TapGesture on Left menu icon
            var tapLeftMenu = new TapGestureRecognizer();
            tapLeftMenu.Tapped += (se, ev) => { OnClickLeftMenuIcon(); };
            _LeftMenuIcon.GestureRecognizers.Add(tapLeftMenu);
        }

        private async void InitVariable()
        {
            // Lấy danh sách studio
            var service = new Service();
            Studios = await service.GetStudios();
            foreach (var studio in Studios)
            {
                StudioPicker.Items.Add(studio.Name);
            }

            if (Studios.Count > 0)
            {
                // Set mặc định ban đầu
                StudioPicker.SelectedIndex = 0;

                // Lấy danh sách các rạp 
                var getCinemas = await service.GetCinemas(Studios[0].Name);
                foreach (var cinema in getCinemas)
                {
                    cinema.StudioId = Studios[0].Id;
                    Cinemas.Add(cinema);
                    CinemaPicker.Items.Add(cinema.Name);
                }

                if (getCinemas.Count > 0)
                {
                    // Lấy danh sách lịch chiếu phim của rạp đầu tiên
                    var getLichChieu = await service.GetLichChieu(getCinemas[0].UrlRap);
                    foreach (var lichchieu in getLichChieu)
                    {
                        lichchieu.CinemaId = getCinemas[0].Id;
                        DSLichChieu.Add(lichchieu);
                    }

                    // Set mặc định ban đầu
                    CinemaPicker.SelectedIndex = 0;
                }
            }

            ListViewLichChieu.ItemsSource = DSLichChieu;

            // Hiển thị thông tin
            Pickers.IsVisible = true;
            MovieScheduleView.IsVisible = true;
            IndicatorLoading.IsVisible = false;
            IndicatorLoading.IsRunning = false;

            IsInitting = false;
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
        private async void StudioPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!IsInitting)
            {
                IsStudioChanging = true;

                // Hiển thị indicator loading
                MovieScheduleView.IsVisible = false;
                IndicatorLoading.IsVisible = true;
                IndicatorLoading.IsRunning = true;

                // Change Studio logo
                switch (StudioPicker.SelectedIndex)
                {
                    case 0: StudioLogo.Source = "cgv_logo.png"; break;
                    case 1: StudioLogo.Source = "lotte_logo.png"; break;
                    case 2: StudioLogo.Source = "galaxy_logo.png"; break;
                    case 3: StudioLogo.Source = "bhd_logo.png"; break;
                    default: StudioLogo.Source = "cgv_logo.png"; break;
                }

                CinemaPicker.Items.Clear();
                Cinemas.Clear();

                // Lấy danh sách các rạp 
                var service = new Service();
                var getCinemas = await service.GetCinemas(Studios[StudioPicker.SelectedIndex].Name);
                foreach (var cinema in getCinemas)
                {
                    cinema.StudioId = Studios[0].Id;
                    Cinemas.Add(cinema);
                    CinemaPicker.Items.Add(cinema.Name);
                }

                DSLichChieu.Clear();
                if (getCinemas.Count > 0)
                {
                    // Lấy danh sách lịch chiếu phim của rạp đầu tiên
                    var getLichChieu = await service.GetLichChieu(getCinemas[0].UrlRap);
                    foreach (var lichchieu in getLichChieu)
                    {
                        lichchieu.CinemaId = getCinemas[0].Id;
                        DSLichChieu.Add(lichchieu);
                    }

                    // Set mặc định ban đầu
                    CinemaPicker.SelectedIndex = 0;
                }

                ListViewLichChieu.ItemsSource = DSLichChieu;

                // Hiển thị thông tin
                MovieScheduleView.IsVisible = true;
                IndicatorLoading.IsVisible = false;
                IndicatorLoading.IsRunning = false;

                IsStudioChanging = false;
            }
        }

        private async void CinemaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsInitting && !IsStudioChanging)
            {
                // Hiển thị indicator loading
                MovieScheduleView.IsVisible = false;
                IndicatorLoading.IsVisible = true;
                IndicatorLoading.IsRunning = true;

                DSLichChieu.Clear();

                // Lấy danh sách lịch chiếu phim
                var service = new Service();
                var getLichChieu = await service.GetLichChieu(Cinemas[CinemaPicker.SelectedIndex].UrlRap);
                foreach (var lichchieu in getLichChieu)
                {
                    lichchieu.CinemaId = Cinemas[CinemaPicker.SelectedIndex].Id;
                    DSLichChieu.Add(lichchieu);
                }

                ListViewLichChieu.ItemsSource = DSLichChieu;

                // Hiển thị thông tin
                MovieScheduleView.IsVisible = true;
                IndicatorLoading.IsVisible = false;
                IndicatorLoading.IsRunning = false;
            }
        }

        private void ListMenuOption_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListMenuOption.SelectedItem = null;
            var item = e.Item as MenuOption;
            if(item.Text.Equals("Lịch Chiếu"))
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

        private void ListViewLichChieu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListViewLichChieu.SelectedItem = null;
        }
        #endregion
    }
}
