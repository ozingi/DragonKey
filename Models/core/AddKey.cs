using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonKey.Models.core
{

    internal class AddKey
    {
        public string Filename { get; set; }
        public string SecretKey { get; set; }
        public string FilenamePath { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public BackButtonBehavior BackButtonBehavior { get; set; }
        public AddKey()
        {
            BackButtonBehavior = new BackButtonBehavior();
            BackButtonBehavior.Command = new Command(OnBackButtonPressed);
        }
        private async void OnBackButtonPressed()
        {
            // Do something here
            await Shell.Current.GoToAsync("///HomePage");
        }
    }
}
