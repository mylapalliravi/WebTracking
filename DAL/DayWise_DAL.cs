using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using WebTracking.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace WebTracking.DAL
{
    public class DayWise_DAL
    {
        public static string strConnLive = (string)ConfigurationManager.ConnectionStrings["connectLive"].ConnectionString;
        //Insert plans in entry page
        public List<DayPlans> DayPlanInsert(DayPlans obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                DateTime.ParseExact(obj.Date.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string result = string.Empty;
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DaydataInsert");
                cmd.Parameters.AddWithValue("@eventdate", obj.Date);
                cmd.Parameters.AddWithValue("@Start", obj.Start);
                cmd.Parameters.AddWithValue("@End", obj.End);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@Empcd", obj.Empcd);
               
                int i = cmd.ExecuteNonQuery();

                return GetDayPlanss(obj); 
              
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        //Delete plans in table
        public int DeletePlan(int EventId)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@mode", "DeletePlan");
                cmd.Parameters.AddWithValue("@EventID", EventId);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet ShowBtn(DayView obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@mode", "GetDayPlans");
                cmd.Parameters.AddWithValue("@empcd", obj.Empcd);
                cmd.Parameters.AddWithValue("@eventdate", obj.PlanDate);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }


        #region Get day Plans in page load

        public DataSet GetDayPlans(DayPlans obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@mode", "GetDayPlans");
                cmd.Parameters.AddWithValue("@empcd", obj.Empcd);
                cmd.Parameters.AddWithValue("@eventdate",obj.Date); 
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }
        public List<DayPlans> GetDayPlanss(DayPlans obj)
        {
            try
            {
                List<DayPlans> model = new List<DayPlans>();
                DataSet ds = GetDayPlans(obj);

                //Description	Start	End	Empcd
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DayPlans dt = new DayPlans();
                    dt.Start = dr["Start"].ToString();
                    dt.End = dr["End"].ToString();
                    dt.Description = dr["Description"].ToString();
                    dt.Empcd = dr["Empcd"].ToString();
                    dt.EventID = Convert.ToInt32(dr["EventID"]);
                    dt.Date = dr["Plandate"].ToString();
                    model.Add(dt);
                }
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //Insert dayplans in popup model
        public string DayPlanList(DayPlans obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                string result = string.Empty;
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "WorksheetInsert");
                cmd.Parameters.AddWithValue("@eventdate", obj.Date);
                cmd.Parameters.AddWithValue("@Start", obj.Start);
                cmd.Parameters.AddWithValue("@End", obj.End);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@Empcd", obj.Empcd);
                int i = cmd.ExecuteNonQuery();

                return result;
               
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        
        //Display records in popup model
        public DataSet GetPopupRecord(DayPlans obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@mode", "GetPopUpRecords");
                cmd.Parameters.AddWithValue("@Empcd", obj.Empcd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }
        //Display records in popup model
        public DataSet GetWorksheetInfo(DayView obj)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("HRAlign_Inland.dbo.Usp_WeeklyPlanCalender", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@mode", "GetWorksheetInfo");
                cmd.Parameters.AddWithValue("@Empcd", obj.Empcd);
                cmd.Parameters.AddWithValue("@eventdate", obj.PlanDate);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }

        public List<DayPlans> GetPopupRecords(DayPlans obj)
        {
            try
            {
                List<DayPlans> model = new List<DayPlans>();
                DataSet ds = GetPopupRecord(obj);

                //Description	Start	End	Empcd
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DayPlans dt = new DayPlans();
                    dt.Start = dr["StartTime"].ToString();
                    dt.End = dr["EndTime"].ToString();
                    dt.Description = dr["Description"].ToString();
                    dt.Empcd = dr["Empcd"].ToString();
                    dt.EventID = Convert.ToInt32(dr["EventID"]);
                    dt.Date= dr["insertdate"].ToString();
                    model.Add(dt);
                }
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region Encryption Decription code
        public class EncryDecry
        {
            private static byte[] key = { };
            private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
            public static string Encrypt(string stringToEncrypt)
            {
                try
                {
                    string SEncryptionKey = "r0b1nr0y";
                    key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            public static string Decrypt(string stringToDecrypt)
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
                try
                {
                    string sEncryptionKey = "r0b1nr0y";
                    key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception)
                {
                    
                    return null;
                }
            }
        } 
        #endregion
    }
}