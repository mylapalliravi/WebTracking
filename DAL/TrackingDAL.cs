using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using WebTracking.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebTracking.DAL
{
    public class TrackingDAL
    {

        public static string strConnLive = (string)ConfigurationManager.ConnectionStrings["connectLive"].ConnectionString;
        public DataSet GetTrackingData(string docketno, string TrackType)
        {
            SqlConnection con = new SqlConnection(strConnLive);
            try
            {
              
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_webtracking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Docketno", docketno);
                cmd.Parameters.AddWithValue("@trackType", TrackType);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
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

        public DataSet Godown_details(string docketno,string TrackType)
        {

            SqlConnection con = new SqlConnection(strConnLive);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GoDown_details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Docketno", docketno);
                cmd.Parameters.AddWithValue("@trackType", TrackType);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception )
            {

                return null;
            }
            finally
            {
                con.Close();
            }

        }


        public List<TrackingSummary> getSummaryTable(string docketno)
        {
            List<TrackingSummary> model = new List<TrackingSummary>();


            DataSet ds = GetTrackingData(docketno, "C");



            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TrackingSummary del1 = new TrackingSummary();
                    del1.Docketno = dr["Docketno"].ToString();
                    del1.DeliveryDate = dr["dlydate"].ToString();
                    del1.Origin = dr["Origin"].ToString();
                    del1.Destination = dr["Destination"].ToString();
                    del1.PickupDate = dr["pickupdate"].ToString();
                    del1.ReferenceNo = dr["referane"].ToString();
                    del1.Address = dr["address"].ToString();
                    del1.DT = dr["dt"].ToString();
                    del1.GoogleETA = dr["GoogleETA"].ToString();
                    model.Add(del1);
                }
            }
            catch (Exception)
            {

                return null;
            }

            return model;
        }


        public List<TrackingDetails> getTrackingDetails(string docketno)
        {

            List<TrackingDetails> model = new List<TrackingDetails>();
            //  string dktno = TempData["dcktno"].ToString();
            DataSet ds = GetTrackingData(docketno, "C");
            try
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    TrackingDetails del2 = new TrackingDetails();
                    del2.locationcode = dr["locationcode"].ToString();
                    del2.ArrivalDate = dr["ArrivalDate"].ToString();
                    del2.arrivalTime = dr["arrivalTime"].ToString();
                    del2.EntryDate = dr["EntryDate"].ToString();
                    del2.Origin = dr["Origin"].ToString();
                    del2.Status = dr["Status"].ToString();
                    model.Add(del2);
                }
            }
            catch (Exception)
            {

                return null;
            }

            return model;
        }


        public List<TrackingGodown_details> getTracking_Godowndetails(string docketno)
        {

            List<TrackingGodown_details> model = new List<TrackingGodown_details>();
            //  string dktno = TempData["dcktno"].ToString();
            DataSet ds = Godown_details(docketno,"C");
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TrackingGodown_details dt = new TrackingGodown_details();
                    dt.GodownCode = dr["GodownCode"].ToString();
                    dt.GodownName = dr["GodownName"].ToString();
                    dt.ContactNo = dr["ContactNo"].ToString();
                    dt.Arrivaldate = dr["Arrivaldate"].ToString();
                    model.Add(dt);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return model;
        }


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
                    //return e.Message;
                    return null;
                }
            }
        }


    }
}