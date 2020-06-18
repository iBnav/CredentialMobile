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
    public partial class Cadastrar : ContentPage
    {
        public Cadastrar()
        {
            InitializeComponent();
        }

        private void Salvar(object sender, EventArgs e)
        {
            long crm = Convert.ToInt64(txtCrm.Text);
            long cpf = Convert.ToInt64(txtCPF.Text);
            long upi = Convert.ToInt64(txtUpi.Text);

            if (!isCPF())
            {
                DisplayAlert("Ops", "CPF inválido", "Fechar");
                return;
            }

            if (!isEmail())
            {
                DisplayAlert("Ops", "Email inválido", "Fechar");
                return;
            }

            try
            {
                long id;
                id = DataBase.Salvar(crm, cpf, txtNome.Text, txtEmail.Text, upi);
                CallNextPage(id);
            }
            catch (Exception ex)
            {
                DataBase.FecharConexao();
                Console.WriteLine(ex.Message);
            }
        }

        private async void CallNextPage(long id)
        {
            await Navigation.PushAsync(new Assinar(id));
        }

        private void chk_colaborador_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private bool isCPF()
        {
            string cpf = txtCPF.Text;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private bool isEmail()
        {
            string email = txtEmail.Text;
            for (var i = 0; i < email.Length; i++)
            {
                if (email[i] == '@')
                {
                    return true;
                }
            }
            return false;
        }
    }
}