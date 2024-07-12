using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ecommerce.Models
{
    [Serializable]
    public class BaseAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public bool VerifyLogin()
        {
            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOst_LstMember";
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
                var pdata = (from p in dataTable.AsEnumerable()
                             where p.Field<string>("Name") == this.UserName && p.Field<string>("Password") == this.Password
                             select new
                             {
                                 UserName = p.Field<string>("Name"),
                                 Role = p.Field<string>("Role")
                             }
                         ).ToList();
                foreach (var obj in pdata)
                {
                    this.UserName = obj.UserName;
                    this.Role = obj.Role;
                }


                return true;
            }

            //if (pdata!=null)
            //{
            //    return true;
            //}
            return false;
        }
    }
}