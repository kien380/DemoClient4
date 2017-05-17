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
	public partial class PaymentPage : ContentPage
	{
        #region VARIABLE DECLARETION

        #endregion

        #region CONSTRUCTOR
        public PaymentPage ()
		{
			InitializeComponent ();
            Init();
        }
        #endregion

        #region METHODS
        private void Init()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            ThoiGianPicker.Items.Add("7:00");
            ThoiGianPicker.Items.Add("8:00");
            ThoiGianPicker.Items.Add("9:00");
            ThoiGianPicker.Items.Add("10:00");
            ThoiGianPicker.Items.Add("11:00");
            ThoiGianPicker.SelectedIndex = 0;
        }
        #endregion

        #region EVENTS
        private void Button_Clicked_DatVe(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion
    }
}
