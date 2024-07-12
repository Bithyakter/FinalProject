using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Services
{
    public class ProductService
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            con = new SqlConnection(constr);
        }

        #region Add New Product   
        public bool AddProduct(Products obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewProduct", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Price", obj.Price);
            com.Parameters.AddWithValue("@Description", obj.Description);
            com.Parameters.AddWithValue("@CategoryID", obj.CategoryID);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        #endregion

        #region Get All Products    
        public List<Products> GetAllProducts()
        {
            connection();
            List<Products> categoryList = new List<Products>();

            SqlCommand com = new SqlCommand("GetProducts", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                categoryList.Add(new Products
                {
                    ProductID = Convert.ToInt32(dr["ProductID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Price = Convert.ToDecimal(dr["Price"]),
                    Description = Convert.ToString(dr["Description"]),
                    CategoryID = Convert.ToInt32(dr["CategoryID"]),
                    Categories = new Categories { Name = Convert.ToString(dr["CategoryName"]) }
                });
            }

            return categoryList;
        }
        #endregion

        #region Update Product    
        public bool UpdateProduct(Products obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateProduct", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductID", obj.ProductID);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Price", obj.Price);
            com.Parameters.AddWithValue("@Description", obj.Description);
            com.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
            con.Open();

            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        #endregion

        #region Delete Product    
        public bool DeleteProduct(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("DeleteProduct", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductID", Id);

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