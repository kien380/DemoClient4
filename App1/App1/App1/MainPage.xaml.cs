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
            AddClickListener();
        }

        private void InitUI()
        {
            ListOptionsViewModal OptionsData = new ListOptionsViewModal();
            ListViewOptions.ItemsSource = OptionsData.ListOptionData;


            // ========== Load data from server ============
            List<ListItemsModal> data = new List<ListItemsModal>();
            LoadDataFromServer(data);
            ListViewItem.ItemsSource = data;

            // ============ Load Local data ================
            //var ItemData = new List<ListItemsModal>
            //{
            //    new ListItemsModal
            //    {
            //        Name = "Underwear Girl",
            //        Price = "15$",
            //        PictureUrl = "picture_a.jpg",
            //        Detail = "red, white",
            //        CommandEvent = new Command(Ontap)
            //    },

            //    new ListItemsModal
            //    {
            //        Name = "Pijama",
            //        Price = "21$",
            //        PictureUrl = "picture_b.jpg",
            //        Detail = "black, blue",
            //        CommandEvent = new Command(Ontap)
            //    },

            //    new ListItemsModal
            //    {
            //        Name = "Handbag",
            //        Price = "29$",
            //        PictureUrl = "picture_c.jpg",
            //        Detail = "blue, white",
            //        CommandEvent = new Command(Ontap)
            //    },

            //    new ListItemsModal
            //    {
            //        Name = "Shoes",
            //        Price = "20$",
            //        PictureUrl = "picture_d.jpg",
            //        Detail = "white, black",
            //        CommandEvent = new Command(Ontap)
            //    },

            //    new ListItemsModal
            //    {
            //        Name = "T-Shirt Girl",
            //        Price = "11$",
            //        PictureUrl = "picture_e.jpg",
            //        Detail = "blue, black",
            //        CommandEvent = new Command(Ontap)
            //    }
            //};
            //ListViewItem.ItemsSource = ItemData;
        }


        private async void LoadDataFromServer(List<ListItemsModal> itemSource)
        {
            LoadingIcon.IsVisible = true;
            LoadingIcon.IsRunning = true;

            var client = new HttpClient(); //thiết lập HttpClient() để truyền dữ liệu bằng http
            string url = "http://ilandapp.com/demoproject/projectlistitem/listitem.php?pass=12569"; //link chứa chuỗi Json.
            var uri = new Uri(url); //Getlink
            var response = await client.GetAsync(uri); //Kết nối với link giao thức Get
            if (response.IsSuccessStatusCode) //Kiểm tra kết nối 
            {
                var contentGet = await response.Content.ReadAsStringAsync();
                //Đọc dữ liệu kết nối đưa vào biến contentGet
                var items = JsonConvert.DeserializeObject<List<ListItemsModal>>(contentGet);
                /*Dùng JsonConvert để đưa dữ liệu từ json (trong biến contentGet) thành 1 List với kiểu dữ liệu là data*/

                LoadingIcon.IsRunning = false;
                LoadingIcon.IsVisible = false;


                foreach (ListItemsModal item in items)
                {

                    itemSource.Add(new ListItemsModal
                    {
                        Name = item.Name,
                        Price = item.Price,
                        PictureURI = new UriImageSource
                        {
                            Uri = new Uri(item.PictureUrl),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(5, 0, 0, 0)
                        },
                        Detail = item.Detail,
                        CommandEvent = new Command(Ontap)
                    });

                    //await DisplayAlert("", item.PictureUrl, "OK");
                }
            }

        }


        /// <summary>
        /// Event when tap on button add on each item
        /// </summary>
        private void Ontap()
        {
            DisplayAlert("Demo", "Demo: add to cart", "OK");
        }

        private void AddClickListener()
        {
            // Add Tap Gesture for _LeftMenuIcon
            TapGestureRecognizer tapLeftMenuIcon = new TapGestureRecognizer();
            tapLeftMenuIcon.Tapped += (s, e) => OnClickLeftMenuIcon();
            _LeftMenuIcon.GestureRecognizers.Add(tapLeftMenuIcon);
            
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
