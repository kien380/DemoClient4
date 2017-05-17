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
        //private List<LichChieu> DSLichChieu = new List<LichChieu>();

        private bool IsInitting = true;
        private bool IsStudioChanging = false;
        private bool IsTicketPriceInShow = false;
        private bool IsBookFilmClicked = false;

        private const string thong_bao = "Thông báo";
        private const string ban_co_muon_thoat = "Bạn có muốn đăng xuất?";
        private const string dong_y = "Đồng ý";
        private const string khong = "Không";
        private const string lich_chieu = "Lịch Chiếu";
        private const string gia_ve = "Giá Vé";
        private const string thong_tin_tai_khoan = "Thông Tin Tài Khoản";
        private const string dang_xuat = "Đăng Xuất";
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
            ShowLoading();

            ListMenuOption.ItemsSource = new List<MenuOption>()
            {
                new MenuOption { Text = thong_tin_tai_khoan, ImageSource = "user.png" },
                new MenuOption { Text = lich_chieu, ImageSource = "list.png" },
                new MenuOption { Text = gia_ve, ImageSource = "price_tag.png" },
                new MenuOption { Text = dang_xuat, ImageSource = "logout.png" }
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
                    ListViewLichChieu.ItemsSource = getLichChieu;
                    // Set mặc định ban đầu
                    CinemaPicker.SelectedIndex = 0;
                    // Set image giá vé
                    TicketPriceImage.Source = new UriImageSource()
                    {
                        Uri = new Uri(Cinemas[0].UrlImg),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(1, 0, 0, 0)
                    };
                }
            }

            // Hiển thị thông tin
            Pickers.IsVisible = true;
            HideLoading();

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

        private void ShowLoading()
        {
            // Hiển thị indicator loading
            MovieScheduleView.IsVisible = false;
            TicketPriceView.IsVisible = false;
            IndicatorLoading.IsVisible = true;
            IndicatorLoading.IsRunning = true;
        }

        private void HideLoading()
        {
            // Hiển thị thông tin
            MovieScheduleView.IsVisible = !IsTicketPriceInShow;
            TicketPriceView.IsVisible = IsTicketPriceInShow;
            IndicatorLoading.IsVisible = false;
            IndicatorLoading.IsRunning = false;
        }
        #endregion

        #region EVENT HANDLE
        private async void StudioPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!IsInitting)
            {
                IsStudioChanging = true;
                ShowLoading();

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

                if (getCinemas.Count > 0)
                {
                    // Lấy danh sách lịch chiếu phim của rạp đầu tiên
                    var getLichChieu = await service.GetLichChieu(getCinemas[0].UrlRap);
                    ListViewLichChieu.ItemsSource = getLichChieu;

                    // Set mặc định ban đầu
                    CinemaPicker.SelectedIndex = 0;

                    // Set image giá vé
                    TicketPriceImage.Source = new UriImageSource()
                    {
                        Uri = new Uri(getCinemas[0].UrlImg),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(1,0,0,0)
                    };
                }

                HideLoading();
                IsStudioChanging = false;
            }
        }

        private async void CinemaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsInitting && !IsStudioChanging)
            {
                ShowLoading();
                
                // Lấy danh sách lịch chiếu phim
                var service = new Service();
                var getLichChieu = await service.GetLichChieu(Cinemas[CinemaPicker.SelectedIndex].UrlRap);
                ListViewLichChieu.ItemsSource = getLichChieu;

                // Set image giá vé
                TicketPriceImage.Source = new UriImageSource()
                {
                    Uri = new Uri(Cinemas[CinemaPicker.SelectedIndex].UrlImg),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1, 0, 0, 0)
                };

                HideLoading();
            }
        }

        private async void ListMenuOption_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListMenuOption.SelectedItem = null;
            var item = e.Item as MenuOption;
            if(item.Text.Equals(lich_chieu))
            {
                MovieScheduleView.IsVisible = true;
                TicketPriceView.IsVisible = false;
                IsTicketPriceInShow = false;

                HeaderTitle.Text = item.Text;
                OnClickLeftMenuIcon();
            }
            else if (item.Text.Equals(gia_ve))
            {
                MovieScheduleView.IsVisible = false;
                TicketPriceView.IsVisible = true;
                IsTicketPriceInShow = true;

                HeaderTitle.Text = item.Text;
                OnClickLeftMenuIcon();
            }
            else if (item.Text.Equals(thong_tin_tai_khoan))
            {
                await Navigation.PushModalAsync(new AccountInfoPage());
            }
            else if (item.Text.Equals(dang_xuat))
            {
                var IsAcceptLogout = await DisplayAlert(thong_bao, ban_co_muon_thoat, dong_y, khong);
                if(IsAcceptLogout)
                {
                    await Navigation.PopAsync();
                }
            }
        }

        private async void ListViewLichChieu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListViewLichChieu.SelectedItem = null;
            if (!IsBookFilmClicked)
            {
                IsBookFilmClicked = true;
                await Navigation.PushModalAsync(new PaymentPage());
            }
        }

        protected override void OnAppearing()
        {
            IsBookFilmClicked = false;   
        }
        #endregion
    }
}
