using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Helper
{
    public class Service
    {
        //private async void LoadDataFromServer(List<ListItemsModal> itemSource)
        //{
        //    LoadingIcon.IsVisible = true;
        //    LoadingIcon.IsRunning = true;

        //    var client = new HttpClient(); //thiết lập HttpClient() để truyền dữ liệu bằng http
        //    string url = "http://ilandapp.com/demoproject/projectlistitem/listitem.php?pass=12569"; //link chứa chuỗi Json.
        //    var uri = new Uri(url); //Getlink
        //    var response = await client.GetAsync(uri); //Kết nối với link giao thức Get
        //    if (response.IsSuccessStatusCode) //Kiểm tra kết nối 
        //    {
        //        var contentGet = await response.Content.ReadAsStringAsync();
        //        //Đọc dữ liệu kết nối đưa vào biến contentGet
        //        var items = JsonConvert.DeserializeObject<List<ListItemsModal>>(contentGet);
        //        /*Dùng JsonConvert để đưa dữ liệu từ json (trong biến contentGet) thành 1 List với kiểu dữ liệu là data*/

        //        LoadingIcon.IsRunning = false;
        //        LoadingIcon.IsVisible = false;


        //        foreach (ListItemsModal item in items)
        //        {

        //            itemSource.Add(new ListItemsModal
        //            {
        //                Name = item.Name,
        //                Price = item.Price,
        //                PictureURI = new UriImageSource
        //                {
        //                    Uri = new Uri(item.PictureUrl),
        //                    CachingEnabled = true,
        //                    CacheValidity = new TimeSpan(5, 0, 0, 0)
        //                },
        //                Detail = item.Detail,
        //                CommandEvent = new Command(Ontap)
        //            });

        //            //await DisplayAlert("", item.PictureUrl, "OK");
        //        }
        //    }

        //}

    }
}
