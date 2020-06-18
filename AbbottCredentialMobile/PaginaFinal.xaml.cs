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
    public partial class PaginaFinal : ContentPage
    {
        public PaginaFinal()
        {
            InitializeComponent();
        }

        private void Finalizar(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}