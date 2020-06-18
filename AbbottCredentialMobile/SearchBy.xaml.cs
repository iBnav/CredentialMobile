using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbbottCredentialMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBy : ContentPage
    {
        private bool isColaborator;
        public SearchBy(bool isCol)
        {
            InitializeComponent();
            isColaborator = isCol;
            Load();
        }

        private void Load()
        {
            var btn1 = new Button {
                BackgroundColor = Color.FromHex("#2196F3"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 100,
                WidthRequest = 400,
                FontSize = 30
            };

            var btn2 = new Button
            {
                Text = "Pesquisar por nome",
                BackgroundColor = Color.FromHex("#2196F3"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 100,
                WidthRequest = 400,
                FontSize = 30
            };
            btn2.Clicked += BuscarNome;

            if (isColaborator)
            {
                btn1.Text = "Pesquisar por UPI";
                btn1.Clicked += BuscarUPI;
                
            } else
            {
                btn1.Text = "Pesquisar por CPF";
                btn1.Clicked += BuscarCPF;
            }

            grid.Children.Add(btn1, 0, 0);
            grid.Children.Add(btn2, 0, 1);

        }

        private async void BuscarNome(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuscarNome(isColaborator));
        }

        private async void BuscarCPF(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuscarCPF());
        }

        private async void BuscarUPI(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuscarCRM());
        }

    }
}