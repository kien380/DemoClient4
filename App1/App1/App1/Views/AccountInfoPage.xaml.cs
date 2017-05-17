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
	public partial class AccountInfoPage : ContentPage
    {
        #region VARIABLE DECLARETION

        #endregion
        
        #region CONSTRUCTOR
        public AccountInfoPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
        }
        #endregion

        #region METHODS
        private async void TapGestureRecognizer_Tapped_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #region EVENTS
        private void Button_Clicked_NapTaiKhoan(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
