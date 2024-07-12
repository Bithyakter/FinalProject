using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ecommerce.Models
{
    [Serializable]
    public class Accounts
    {
        public string UserName { get; set; }
        public string Password { get; set; }


        public bool VerifyLogin()
        {
            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.sp_Users";
            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            cmd.Dispose();
            connection.Close();

            if (dataTable.Rows.Count > 0)
            {
                return true;
            }

            var pdata = (from p in dataTable.AsEnumerable()
                         where p.Field<string>("UserName") == this.UserName && p.Field<string>("Password") == this.Password
                         select new
                         {
                             UserName = p.Field<string>("UserName")
                         }
                         ).SingleOrDefault();

            if (pdata != null)
            {
                return true;
            }

            return false;
        }
    }
}