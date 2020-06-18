using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AbbottCredentialMobile.Classes
{
    class DataBase
    {
        //private static string connectionString = "server=10.10.8.250;Initial Catalog=AbbottCredential;user=Abbott;password=Abbott;";
        private static string connectionString = "server=192.168.200.193;Initial Catalog=AbbottCredential;user=Abbott;password=Abbott;";

        private static SqlConnection sqlConn = new SqlConnection(connectionString);

        public static SqlDataReader Procurar(string coluna, long parametro)
        {
            try
            {
                sqlConn.Open();
                SqlCommand sql = new SqlCommand("SELECT * FROM ListaPresenca WHERE " + coluna + "=" + parametro, sqlConn);
                SqlDataReader dr = sql.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static SqlDataReader Procurar(string coluna, string parametro)
        {
            try
            {
                sqlConn.Open();
                SqlCommand sql = new SqlCommand("SELECT * FROM ListaPresenca WHERE " + coluna + "= '" + parametro + "'", sqlConn);
                SqlDataReader dr = sql.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Checkin(long pessoa)
        {
            sqlConn.Open();
            SqlCommand sql = new SqlCommand("update ListaPresenca set Checkin = '" + System.DateTime.Now + "' where id = " + pessoa, sqlConn);
            sql.ExecuteNonQuery();
            sqlConn.Close();
        }

        public static int Salvar(long crm, long cpf, string nome, string email, long upi)
        {
            Int32 resp;
            sqlConn.Open();
            SqlCommand sql = new SqlCommand($"INSERT INTO ListaPresenca (CRM, CPF, Nome, Email, upi) values ({crm}, {cpf},'{nome}','{email}',{upi}) SELECT SCOPE_IDENTITY();", sqlConn);
            resp = Convert.ToInt32(sql.ExecuteScalar());
            sqlConn.Close();
            return resp;
        }

        public static void SalvarImagem(string img, long id)
        {
            sqlConn.Open();
            SqlCommand sql = new SqlCommand($"UPDATE ListaPresenca SET Assinatura = '{img}' where Id = {id};", sqlConn);
            sql.ExecuteNonQuery();
            sqlConn.Close();
        }

        public static SqlDataReader ProcurarGeral(string coluna, string parametro)
        {
            try
            {
                sqlConn.Open();
                SqlCommand sql = new SqlCommand("SELECT * FROM ListaPresenca WHERE " + coluna + " like '%" + parametro + "%'", sqlConn);
                SqlDataReader dr = sql.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void FecharConexao()
        {
            sqlConn.Close();
        }
    }
}
