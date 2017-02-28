using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{
    public class ListItemsModal
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string PictureUrl { get; set; }

        public string Detail { get; set; }

        public ICommand CommandEvent { get; set; }
    }
}
