using AbbottCredentialMobile.Classes;
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
    public partial class ListView : ContentPage
    {
        private bool Confirmar;
        public bool isColaborator;
        public ListView(bool colaborador)
        {
            InitializeComponent();
            isColaborator = colaborador;
            Load();
        }

        private void Load() 
        {
            Confirmar = false;

            Grid grid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition { Width = 250 },
                    new ColumnDefinition { Width = 200 },
                    new ColumnDefinition { Width = 200 },
                    new ColumnDefinition { Width = 300 },
                    new ColumnDefinition { Width = 100 },
                    new ColumnDefinition { Width = 200 }
                }
            };
            var lbl1 = new Label
            {
                Text = "Nome",
                TextColor = Color.FromHex("#C0C0C0"),
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Children.Add(lbl1, 0, 0);
            
            var lbl3 = new Label
            {
                Text = "Email",
                TextColor = Color.FromHex("#C0C0C0"),
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Children.Add(lbl3, 3, 0);

            if (isColaborator)
            {
                var lbl4 = new Label
                {
                    Text = "UPI",
                    TextColor = Color.FromHex("#C0C0C0"),
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                };
                grid.Children.Add(lbl4, 2, 0);
            } else
            {
                var lbl4 = new Label
                {
                    Text = "CPF",
                    TextColor = Color.FromHex("#C0C0C0"),
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                };
                grid.Children.Add(lbl4, 2, 0);
            }
            
            var chk_confirm = new Label
            {
                Text = "Confirmar",
                TextColor = Color.FromHex("#C0C0C0"),
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center

            };
            grid.Children.Add(chk_confirm, 4, 0);

            ViewCell cell = new ViewCell() { View = grid };

            GridList.Add(cell);
        }

        public void InserirLista(Pessoa obj)
        {
            Grid grid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition { Width = 250 },
                    new ColumnDefinition { Width = 200 },
                    new ColumnDefinition { Width = 200 },
                    new ColumnDefinition { Width = 350 },
                    new ColumnDefinition { Width = 50 },
                    new ColumnDefinition { Width = 200 }
                }
            };
            var lbl1 = new Label
            {
                Text = obj.Nome,
                TextColor = Color.White,
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,

            };
            grid.Children.Add(lbl1, 0, 0);
            var lbl3 = new Label
            {
                Text = obj.Email.ToString(),
                TextColor = Color.White,
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,
            };
            grid.Children.Add(lbl3, 3, 0);

            var lbl4 = new Label
            {
                Text = obj.CPF.ToString(),
                TextColor = Color.White,
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,
            };
            grid.Children.Add(lbl4, 2, 0);

            var chk_confirm = new CheckBox
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                
            };
            chk_confirm.CheckedChanged += Chk_confirm_CheckedChanged;
            grid.Children.Add(chk_confirm, 4, 0);
            var btn = new Button
            {
                AutomationId = obj.Id,
                Text = "Assinar",
                BackgroundColor = Color.FromHex("#2196F3"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 35,
                FontSize = 10

            };
            btn.Clicked += Assinar;
            grid.Children.Add(btn, 5, 0);

            ViewCell cell = new ViewCell() { View = grid };

            GridList.Add(cell);
        }

        private void Chk_confirm_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Confirmar = !Confirmar;
        }

        private async void Assinar(object sender, EventArgs e)
        {
            if (Confirmar) { 
                Button s = (Button)sender;
                long id = Convert.ToInt64(s.AutomationId);
                await Navigation.PushAsync(new Assinar(id));
            } else
            {
                await DisplayAlert("Por favor", "Confirme os dados para continuar", "Fechar");
            }
        }
    }
}