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
    public partial class BuscarCPF : ContentPage
    {
        public BuscarCPF()
        {
            InitializeComponent();
        }

        private async void Buscar(object sender, EventArgs e)
        {
            ListView lv = new ListView(false);
            
            try
            {
                SqlDataReader dr = DataBase.Procurar("CPF", Convert.ToInt64(txtCPF.Text));
                Pessoa p = new Pessoa();
                while (dr.Read())
                {
                    if (string.IsNullOrEmpty(dr["Checkin"].ToString()))
                    {
                        
                        p.Nome = dr["Nome"].ToString();
                        if(!string.IsNullOrEmpty(dr["CRM"].ToString()))
                            p.CRM = Convert.ToInt64(dr["CRM"]);
                        p.CPF = Convert.ToInt64(dr["CPF"]);
                        p.Email = dr["Email"].ToString();
                        p.Estado = dr["Estado"].ToString();
                        p.Id = dr["Id"].ToString();
                        lv.InserirLista(p);
                    }
                }
                DataBase.FecharConexao();
                await Navigation.PushAsync(lv);
            }
            catch (Exception ex)
            {
                DataBase.FecharConexao();
                await DisplayAlert("Ops", "Nenhum resultado encontrado :"+ex.Message, "Fechar");
            }
            
        }
    }
}