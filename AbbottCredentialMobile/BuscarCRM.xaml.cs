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
    public partial class BuscarCRM : ContentPage
    {
        public BuscarCRM()
        {
            InitializeComponent();
        }

        private async void Buscar(object sender, EventArgs e)
        {
            long UPI = Convert.ToInt64(txtCRM.Text);
            ListView lv = new ListView(true);
            lv.isColaborator = true;
            try
            {
                SqlDataReader dr = DataBase.Procurar("UPI", UPI);
                while (dr.Read())
                {
                    if (string.IsNullOrEmpty(dr["Checkin"].ToString()))
                    {
                        Pessoa p = new Pessoa();
                        p.Nome = dr["Nome"].ToString();
                        p.CRM = Convert.ToInt64(dr["CRM"]);
                        p.upi = Convert.ToInt64(dr["upi"]);
                        p.Email = dr["Email"].ToString();
                        p.Id = dr["Id"].ToString();
                        lv.InserirLista(p);
                    }
                }
                DataBase.FecharConexao();
                await Navigation.PushAsync(lv);
            }
            catch (Exception ex)
            {
                try
                {
                    DataBase.FecharConexao();

                }
                catch (Exception)
                {

                }
                await DisplayAlert("Ops", "Nenhum resultado encontrado: " + ex.Message, "Fechar");
            }
            
        }
    }
}