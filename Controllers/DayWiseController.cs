using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTracking.Models;
using WebTracking.DAL;
using static WebTracking.DAL.DayWise_DAL;
using System.Data;

namespace WebTracking.Controllers
{

    public class DayWiseController : Controller
    {
        DayWise_DAL _WPO = new DayWise_DAL();
        #region Waste code
        // GET: DayWise
        //public ActionResult DayWise(DayView dayView)
        //{
        // string strReq = "";
        // string[] arrMsgs;
        // strReq = Request.Url.Query;
        // strReq = strReq.Substring(strReq.IndexOf('?') + 1);
        // if (strReq != null)
        // {
        // arrMsgs = strReq.Split('?');
        // }
        // Session["Empcd"] = strReq;

        // var DV= _WPO.GetWeeklyPlanEvents(dayView);
        // return View(DV);
        //}
        //[HttpPost]
        //public ActionResult DayWise(DayView dayView, FormCollection frm)
        //{
        // string strReq = "";
        // strReq = Session["Empcd"].ToString();
        // string[] arrEmpcd;
        // string[] arrMsgs;

        // strReq = strReq.Replace("%2f", "/");
        // strReq = strReq.Replace("%3F", "");
        // strReq = strReq.Replace("%2F", "/");
        // strReq = strReq.Replace("%2B", "+");
        // strReq = EncryDecry.Decrypt(strReq);
        // if (strReq != null)
        // {
        // arrMsgs = strReq.Split('&');
        // }
        // else
        // {
        // arrMsgs = null;
        // }
        // arrEmpcd = arrMsgs[0].Split('=');
        // string emp = arrEmpcd[1].ToString().Trim();

        // dayView.EventDate = frm["date"];
        // string dayhour = frm["dayhourid"];
        // string Description = frm["Description"];
        // string[] desarr = Description.Split(',');
        // string[] hr = dayhour.Split(',');

        // for (int i = 0; i < desarr.Length; i++)
        // {
        // Description obj = new Description();
        // obj.Desc = desarr[i].ToString();
        // obj.dayhour = hr[i].ToString();
        // dayView.Description.Add(obj);

        // }
        // dayView.Empcd = emp;
        // var DV = _WPO.DayWiseInsert(dayView);
        // var DV1 = _WPO.GetWeeklyPlanEvents(dayView);

        // return View(DV1);
        //}
        #endregion

        [HttpGet]
        public ActionResult DayPlan(DayPlans dayView)
        {
            try
            {
                string strReq = "";
                string[] arrMsgs;

                strReq = Request.Url.Query;
                strReq = strReq.Substring(strReq.IndexOf('?') + 1);
                if (strReq != null)
                {
                    arrMsgs = strReq.Split('?');
                }
                Session["Empcd"] = strReq;
                ListPlans lst = new ListPlans();
                dayView.Empcd = EncryDecry.Decrypt(Session["Empcd"].ToString()).Replace("Empcd=", "");
                dayView.Date = DateTime.Today.ToString("dd/MM/yyyy");
                var Get_Plans = _WPO.GetDayPlanss(dayView);
                lst.DayPlan = Get_Plans;
                return View(lst);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        public ActionResult SHOW()
        {
            try
            {
                if (Request.Form["ShowPlans"] != null)
                {
                    string principle = Convert.ToString(Request["date"].ToString());

                    DayPlans dayPlans = new DayPlans();
                    dayPlans.Empcd = Session["Empcd"].ToString();
                    dayPlans.Date = principle;

                    var getHistory = _WPO.GetDayPlanss(dayPlans);
                }
                ListPlans lst1 = new ListPlans();
                return View(lst1);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public ActionResult DeletePlan(int EventId)
        {
            string strReq = "";
            string[] arrMsgs;
            string[] arrEmpcd;
            string[] arrID;
            strReq = Request.Url.Query;
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            if (strReq != null)
            {
                arrMsgs = strReq.Split('?');
            }
            if (strReq != null)
            {
                arrMsgs = strReq.Split('&');
            }
            else
            {
                arrMsgs = null;
            }
            arrEmpcd = arrMsgs[1].Split('=');
            string emp = arrEmpcd[0].ToString().Trim();

            arrID = arrMsgs[0].Split('=');
            string ID = arrEmpcd[1].ToString().Trim();
            string empcd = arrEmpcd[1];
            Session["Empcd"] = strReq;
            _WPO.DeletePlan(EventId);
            //string EmpcdDecry =EncryDecry.Encrypt(Session["Empcd"].ToString()).Replace("Empcd=", "");
            return Redirect("https://localhost:44382/DayWise/DayPlan?s2o+vZQxoM0UHPZVdQPaCA==");
            //return Redirect("DayWise/DayPlan?" + EmpcdDecry);
        }
        //Popup insert
        [HttpPost]
        public ActionResult DayPlansInsert(DayPlans obj, FormCollection frms)
        {
            obj.Date = frms["date"];
            obj.Start = frms["From"];
            obj.End = frms["To"];
            obj.Description = frms["Description"];
            obj.Empcd = EncryDecry.Decrypt(Session["Empcd"].ToString()).Replace("Empcd=", "");
            _WPO.DayPlanList(obj);
            //ViewBag.PopRecords = _WPO.GetPopupRecord(obj);

            //ListPlans lst1 = new ListPlans();
            return Redirect("https://localhost:44382/DayWise/DayPlan?s2o+vZQxoM0UHPZVdQPaCA==");
        }
        //entry insert
        [HttpPost]
        public ActionResult DayPlan(DayPlans dayView, FormCollection frms)
        {
            try
            {
                string strReq = "";
                strReq = Session["Empcd"].ToString();
                string[] arrEmpcd;
                string[] arrMsgs;

                strReq = strReq.Replace("%2f", "/");
                strReq = strReq.Replace("%3F", "");
                strReq = strReq.Replace("%2F", "/");
                strReq = strReq.Replace("%2B", "+");
                strReq = EncryDecry.Decrypt(strReq);
                if (strReq != null)
                {
                    arrMsgs = strReq.Split('&');
                }
                else
                {
                    arrMsgs = null;
                }
                arrEmpcd = arrMsgs[0].Split('=');
                string emp = arrEmpcd[1].ToString().Trim();

                dayView.Start = frms["From"];
                dayView.End = frms["To"];
                dayView.Description = frms["Description"];
                dayView.Empcd = emp;

                ListPlans lst1 = new ListPlans();
                var Get_Plans1 = _WPO.DayPlanInsert(dayView);
                lst1.DayPlan = Get_Plans1;
                return View(lst1);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult GetWorksheetInfo(DayView view)
        {
            try
            {
                var getworksheet = _WPO.GetWorksheetInfo(view);
                List<DayView> List = new List<DayView>();
                DayView obj1 = new DayView();
                if (getworksheet != null && getworksheet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in getworksheet.Tables[0].Rows)
                    {
                        DayView _obj = new DayView();
                        _obj.Start = item["start"].ToString();
                        _obj.End = item["end"].ToString();
                        _obj.Desc = item["Description"].ToString();
                        _obj.Empcd = item["empcd"].ToString();
                        _obj.PlanDate = item["Plandate"].ToString();
                        List.Add(_obj);
                    }
                    return Json(List, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    obj1.Message = "No Data";
                    return Json(obj1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}