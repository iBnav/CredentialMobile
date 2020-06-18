using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbbottCredentialMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Selection : ContentPage
    {
        public Selection()
        {
            InitializeComponent();
        }

        //private async void LerQRCode(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LeituraQRCode());
        //}
        private async void NaoColaborador(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchBy(false));
        }
        private async void Colaborador(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchBy(true));
        }
        
        private async void Cadastrar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastrar());
        }

    }
}