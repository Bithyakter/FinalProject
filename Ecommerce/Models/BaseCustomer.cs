using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Models
{
    public class BaseCustomer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }

        public static List<BaseCustomer> ListCustomer()
        {
            List<BaseCustomer> plstCust = new List<BaseCustomer>();
            //DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstCustomer";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    BaseCustomer obj = new BaseCustomer();
                    obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
                    obj.CustomerName = mrd["CustomerName"].ToString();
                    obj.CustomerMobile = mrd["CustomerMobile"].ToString();
                    plstCust.Add(obj);
                }
            }

            cmd.Dispose();
            connection.Close();


            //for (int i = 0; i < 50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop_"+i.ToString();
            //    baseEquipment.EcCount = 5+i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;
            //    plstData.Add(baseEquipment);
            //}
            return plstCust;
        }

        public static DataTable ListCustomerEquipment()
        {
            //List<BaseCustomer> plstCust = new List<BaseCustomer>();
            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOst_LstCustomerEquiAssignment";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            //SqlDataReader mrd = cmd.ExecuteReader();
            //if (mrd.HasRows)
            //{
            //    while (mrd.Read())
            //    {
            //        BaseCustomer obj = new BaseCustomer();
            //        obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
            //        obj.CustomerName = mrd["CustomerName"].ToString();
            //        obj.CustomerMobile = mrd["CustomerMobile"].ToString();
            //        plstCust.Add(obj);
            //    }
            //}
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            cmd.Dispose();
            connection.Close();


            //for (int i = 0; i < 50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop_"+i.ToString();
            //    baseEquipment.EcCount = 5+i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;
            //    plstData.Add(baseEquipment);
            //}
            return dataTable;
        }
        public static int SaveAssignment(int CustomerID, int EquipmentID, int EquipmentQuantity)
        {
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_InsEquiAssignment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EquipmentID", EquipmentID));
            cmd.Parameters.Add(new SqlParameter("@EquiCount", EquipmentQuantity));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            int returnResult = cmd.ExecuteNonQuery();


            cmd.Dispose();
            connection.Close();


            //for (int i = 0; i < 50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop_"+i.ToString();
            //    baseEquipment.EcCount = 5+i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;
            //    plstData.Add(baseEquipment);
            //}
            return returnResult;
        }
    }
}