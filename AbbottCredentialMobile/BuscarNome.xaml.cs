using AbbottCredentialMobile.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbbottCredentialMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscarNome : ContentPage
    {
        private bool isColaborator;
        public BuscarNome(bool isCol)
        {
            InitializeComponent();
            isColaborator = isCol;
        }

        private async void Buscar(object sender, EventArgs e)
        {
            ListView lv = new ListView(isColaborator);

            try
            {
                //SqlDataReader dr = DataBase.ProcurarGeral("Nome", txtNome.Text);
                Pessoa p = new Pessoa();
                //while (dr.Read())
                //{
                //    if (string.IsNullOrEmpty(dr["Checkin"].ToString()))
                //    {

                        p.Nome = "Igor Benavente";
                        p.CPF = 43865708889;
                        p.Email = "igor.benavente@42labs.com.br";
                        p.Id = "1";
                        lv.InserirLista(p);
                //    }
                //}
                DataBase.FecharConexao();
                await Navigation.PushAsync(lv);
            }
            catch (Exception ex)
            {
                DataBase.FecharConexao();
                await DisplayAlert("Ops", "Nenhum resultado encontrado :" + ex.Message, "Fechar");
            }

        }
    }
}