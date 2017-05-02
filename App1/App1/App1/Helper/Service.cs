using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class Service
    {
        public Service()
        {

        }

        public async Task<List<Studio>> GetStudios ()
        {
            var studios = new List<Studio>();
            var client = new HttpClient(); //thiết lập HttpClient() để truyền dữ liệu bằng http
            string url = "http://iseteam.com/lichchieuphim/index.php/api/getstudio"; //link chứa chuỗi Json.
            var uri = new Uri(url); //Getlink
            try
            {
                var response = await client.GetAsync(uri); //Kết nối với link giao thức Get
                if (response.IsSuccessStatusCode) //Kiểm tra kết nối 
                {
                    //Đọc dữ liệu kết nối đưa vào biến contentGet
                    var contentGet = await response.Content.ReadAsStringAsync();
                    //Dùng JsonConvert để đưa dữ liệu từ json (trong biến contentGet) thành 1 List với kiểu dữ liệu là data
                    var items = JsonConvert.DeserializeObject<List<Studio>>(contentGet);
                    if(items != null)
                    {
                        foreach(var item in items)
                        {
                            studios.Add(item);
                        }
                    }
                    }

                }
            catch { }
            return studios;
        }

        public async Task<List<Cinema>> GetCinemas(string studioName)
        {
            var cinemas = new List<Cinema>();
            var client = new HttpClient(); //thiết lập HttpClient() để truyền dữ liệu bằng http
            string url = "http://iseteam.com/lichchieuphim/index.php/api/getcinema/cinema/" + studioName.ToLower(); //link chứa chuỗi Json.
            var uri = new Uri(url); //Getlink
            try
            {
                var response = await client.GetAsync(uri); //Kết nối với link giao thức Get
                if (response.IsSuccessStatusCode) //Kiểm tra kết nối 
                {
                    //Đọc dữ liệu kết nối đưa vào biến contentGet
                    var contentGet = await response.Content.ReadAsStringAsync();
                    //Dùng JsonConvert để đưa dữ liệu từ json (trong biến contentGet) thành 1 List với kiểu dữ liệu là data
                    var items = JsonConvert.DeserializeObject<List<Cinema>>(contentGet);
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            cinemas.Add(item);
                        }
                    }
                }
            }
            catch { }
            return cinemas;
        }

        public async Task<List<LichChieu>> GetLichChieu(string urlRap)
        {
            var dsLichChieu = new List<LichChieu>();
            var client = new HttpClient(); //thiết lập HttpClient() để truyền dữ liệu bằng http
            string url = "http://iseteam.com/lichchieuphim/index.php/api/getfilm/rap/" + urlRap; //link chứa chuỗi Json.
            var uri = new Uri(url); //Getlink
            try
            {
                var response = await client.GetAsync(uri); //Kết nối với link giao thức Get
                if (response.IsSuccessStatusCode) //Kiểm tra kết nối 
                {
                    //Đọc dữ liệu kết nối đưa vào biến contentGet
                    var contentGet = await response.Content.ReadAsStringAsync();
                    //Dùng JsonConvert để đưa dữ liệu từ json (trong biến contentGet) thành 1 List với kiểu dữ liệu là data
                    var items = JsonConvert.DeserializeObject<List<LichChieu>>(contentGet);
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            dsLichChieu.Add(item);
                        }
                    }
                }
            }
            catch { }
            return dsLichChieu;
        }
    }
}
