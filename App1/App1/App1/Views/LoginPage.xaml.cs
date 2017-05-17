using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        #region VARIABLE DECLARETION
        private const string thong_bao = "Thông báo";
        private const string dien_ten_dang_nhap = "Bạn phải điền tên đăng nhập";
        private const string dien_mat_khau = "Bạn phải điền mật khẩu";
        private const string tai_khoan_khong_chinh_xac = "Tên đăng nhập hoặc mật khẩu không chính xác";
        private const string ok = "OK";
        #endregion

        #region CONSTRUCTORS
        public LoginPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
        }
        #endregion

        #region METHODS
        private bool IsCorrectAccout(string username, string password)
        {
            // ======= API login in here ======
            var accounts = new AccountLoginVm();


            foreach(var account in accounts.Accounts)
            {
                if(username.ToLower().Equals(account.Username.ToLower()))
                {
                    if(password.Equals(account.Password))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ShowLoading()
        {
            LoginButton.IsVisible = false;
            LoginIndicator.IsRunning = true;
        }

        private void HideLoading()
        {
            LoginButton.IsVisible = true;
            LoginIndicator.IsRunning = false;
        }
        #endregion

        #region EVENTS
        private async void Button_Login_Clicked(object sender, EventArgs e)
        {
            ShowLoading();

            //========= test ==========
            var username = "BaoNV";
            var password = "bao123";
            //=========================

            //var username = UsernameEntry.Text;
            //var password = PasswordEntry.Text;

            //if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            //{
            //    await DisplayAlert(thong_bao, dien_ten_dang_nhap, ok);
            //} 
            //else if (string.IsNullOrEmpty(password))
            //{
            //    await DisplayAlert(thong_bao, dien_mat_khau, ok);
            //}
            //else
            if(!IsCorrectAccout(username, password))
            {
                // ======= Test =======
                await Task.Delay(1000);
                // ====================
                await DisplayAlert(thong_bao, tai_khoan_khong_chinh_xac, ok);
            }
            else
            {
                // ======= Test =======
                await Task.Delay(1000);
                // ====================
                await Navigation.PushAsync(new MainPage());
            }

            HideLoading();
        }
        #endregion
    }
}
