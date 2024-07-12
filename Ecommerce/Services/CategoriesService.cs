using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Services
{
    public class CategoriesService
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            con = new SqlConnection(constr);
        }

        #region Add New Category   
        public bool AddCategory(Categories obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewCategory", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Description", obj.Description);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        #endregion

        #region Get All Categories    
        public List<Categories> GetAllCategories()
        {
            connection();
            List<Categories> categoryList = new List<Categories>();

            SqlCommand com = new SqlCommand("GetCategories", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                categoryList.Add(new Categories
                {
                    CategoryID = Convert.ToInt32(dr["CategoryID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Description = Convert.ToString(dr["Description"])
                });
            }

            return categoryList;
        }
        #endregion

        #region Update Category    
        public bool UpdateCategory(Categories obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateCategory", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Description", obj.Description);
            con.Open();

            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        #endregion

        #region Delete Category    
        public bool DeleteCategory(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("DeleteCategory", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CategoryID", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        #endregion
    }
}