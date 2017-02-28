using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class ListOptionsViewModal
    {
        public List<ListOptionsModal> ListOptionData;

        public ListOptionsViewModal()
        {
            ListOptionData = new List<ListOptionsModal>
            {
                new ListOptionsModal
                {
                    imgSource = "home.png",
                    text = "Explore"
                },
                new ListOptionsModal
                {
                    imgSource = "list.png",
                    text = "Activity"
                },
                new ListOptionsModal
                {
                    imgSource = "cart.png",
                    text = "Cart"
                },
                new ListOptionsModal
                {
                    imgSource = "settings.png",
                    text = "Settings"
                },
                new ListOptionsModal
                {
                    imgSource = "exit.png",
                    text = "Log out"
                }
            };
        }
    }
}
