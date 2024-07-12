using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Ecommerce.Models
{
    [Serializable]
    public class BaseEquipment
    {
        [DataMember]
        public int EquipmentID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EcCount { get; set; }
        [DataMember]
        public int Stock { get; set; }
        [DataMember]
        public DateTime EntryDate { get; set; }

        public List<BaseEquipment> ListEquipment { get; set; }

        public BaseEquipment()
        {
            ListEquipment = new List<BaseEquipment>();
        }

        public static List<BaseEquipment> ListEquipmentData()
        {
            List<BaseEquipment> plstData = new List<BaseEquipment>();
            //DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstEquipment";
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
                    BaseEquipment obj = new BaseEquipment();
                    obj.EquipmentID = Convert.ToInt32(mrd["EquipmentID"].ToString());
                    obj.Name = mrd["EquipmentName"].ToString();
                    obj.EcCount = Convert.ToInt16(mrd["Quantity"].ToString());
                    obj.Stock = Convert.ToInt16(mrd["Stock"].ToString());
                    obj.EntryDate = Convert.ToDateTime(mrd["EntryDate"].ToString());
                    plstData.Add(obj);
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
            return plstData;
        }

        public int SaveEquipment()
        {
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_InsEquipment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
            cmd.Parameters.Add(new SqlParameter("@EcCount", this.EcCount));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", this.EntryDate));
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