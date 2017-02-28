using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App1
{
    public class ListItemsViewModal
    {
        public List<ListItemsModal> ListItemsData { get; set; }

        public ListItemsViewModal()
        {
            ListItemsData = new List<ListItemsModal>
            {
                new ListItemsModal
                {
                    Name = "Underwear Girl",
                    Price = "15$",
                    PictureUrl = "picture_a.jpg",
                    Detail = "red, white"
                },

                new ListItemsModal
                {
                    Name = "Pijama",
                    Price = "21$",
                    PictureUrl = "picture_b.jpg",
                    Detail = "black, blue"
                },

                new ListItemsModal
                {
                    Name = "Handbag",
                    Price = "29$",
                    PictureUrl = "picture_c.jpg",
                    Detail = "blue, white"
                },

                new ListItemsModal
                {
                    Name = "Shoes",
                    Price = "20$",
                    PictureUrl = "picture_d.jpg",
                    Detail = "white, black"
                },

                new ListItemsModal
                {
                    Name = "T-Shirt Girl",
                    Price = "11$",
                    PictureUrl = "picture_e.jpg",
                    Detail = "blue, black"
                }
            };

            //LoadDataFromServer();
        }

        //private async void LoadDataFromServer()
        //{
        //    var client = new HttpClient(); //thiết lập HttpClient() để truyền dữ liệu bằng http
        //    string url = "http://ilandapp.com/demoproject/projectlistitem/listitem.php"; //link chứa chuỗi Json.
        //    var uri = new Uri(url); //Getlink
        //    var response = await client.GetAsync(uri); //Kết nối với link giao thức Get
        //    if (response.IsSuccessStatusCode) //Kiểm tra kết nối 
        //    {
        //        var contentGet = await response.Content.ReadAsStringAsync();
        //        //Đọc dữ liệu kết nối đưa vào biến contentGet
        //        var ListItemsData = JsonConvert.DeserializeObject<List<ListItemsModal>>(contentGet);
        //        /*Dùng JsonConvert để đưa dữ liệu từ json (trong biến contentGet) thành 1 List với kiểu dữ liệu là data*/

        //    }

        //}
    }
}
