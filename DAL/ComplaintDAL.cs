using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebTracking.Models;

namespace WebTracking.DAL
{
    public class ComplaintDAL
    {
        public static string strConnLive = (string)ConfigurationManager.ConnectionStrings["connectLive"].ConnectionString;

        public string Customer_Complaint(Customer_ComplaintBo obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                string result = string.Empty;
                
                con.Open();
                SqlCommand cmd = new SqlCommand("inCore.dbo.Usp_Customer_complaint", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "Insert");
                cmd.Parameters.AddWithValue("@Customername", obj.Customername);
                cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                cmd.Parameters.AddWithValue("@ComplainType", obj.Related);
                cmd.Parameters.AddWithValue("@Docketno", obj.DocketNo);
                cmd.Parameters.AddWithValue("@Address1", obj.Address1);
                cmd.Parameters.AddWithValue("@Grievance", obj.Issue);
                cmd.Parameters.AddWithValue("@Pin", obj.Pin);
                cmd.Parameters.Add("@outputpara", SqlDbType.VarChar, 50);
                cmd.Parameters["@outputpara"].Direction = ParameterDirection.Output; 
               
                int i = cmd.ExecuteNonQuery();
                result = (string)cmd.Parameters["@outputpara"].Value;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }
        //DropDown code
        public List<Customer_Complaint> DrpRelated(Customer_Complaint obj)
        {
            List<Customer_Complaint> list = new List<Customer_Complaint>();
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("inCore.dbo.Usp_Customer_complaint", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Mode", "DrpRelated");
                DataSet ds = new DataSet();

                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        Customer_Complaint cust = new Customer_Complaint();
                        cust.Complaindesc = item["Complaindesc"].ToString();
                        cust.complaincode = item["complaincode"].ToString();
                        list.Add(cust);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
            return list;            
        }
    }
}