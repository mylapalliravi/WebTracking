using System;
using System.Web.Mvc;
using WebTracking.DAL;
using WebTracking.Models;
using static WebTracking.DAL.TrackingDAL;
using System.Linq;
using System.Data;


namespace WebTracking.Controllers
{
    public class HomeController : Controller
    {
        TrackingDAL db = new TrackingDAL();
     //   WeeklyPlanCalender _weeklyPlan = new WeeklyPlanCalender();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string docketno)
        {

            TrackingTables model = new TrackingTables();
            if (docketno != "")
            {
                try
                {
                    TempData["dcktno"] = docketno;

                    var summary = db.getSummaryTable(docketno);
                    var details = db.getTrackingDetails(docketno);
                    var Godown_details = db.getTracking_Godowndetails(docketno);
                   


                    model.trackingsummary = summary;
                    model.trackingdetails = details;
                    model.tracking_godowndetails = Godown_details;
                    ViewBag.check = true;

                    if (model.trackingsummary.Count == 0 && model.trackingdetails.Count == 0 && model.tracking_godowndetails.Count == 0)
                    {
                        ViewBag.dataempty = "Data is not Found";
                    }
                    else
                    {
                        ViewBag.dataempty = "";
                    }
                }

                catch (Exception e)
                {
                    ViewBag.Exception = e;
                }
                
            }
            return View(model);
        }

        public ActionResult payment_Link()
        {
            return View();
        }

        //#region Ado code
        //public ActionResult WeeklyPlans(WeeklyPlanBO _WPB)
        //{
        //    string strReq = "";
        //    string[] arrMsgs;
        //    strReq = Request.Url.Query;
        //    strReq = strReq.Substring(strReq.IndexOf('?') + 1);
        //    if (strReq != null)
        //    {
        //        arrMsgs = strReq.Split('?');
        //    }
        //    Session["Empcd"] = strReq;

        //    string[] arrEmpcd;
        //    strReq = EncryDecry.Decrypt(strReq);
        //    if (strReq != null)
        //    {
        //        arrMsgs = strReq.Split('&');
        //    }
        //    else
        //    {
        //        arrMsgs = null;
        //    }
        //    arrEmpcd = arrMsgs[0].Split('=');
        //    string emp = arrEmpcd[1].ToString().Trim();
        //    _WPB.Empcd = emp;
        //    WeeklyPlanCalender Da_obj = new WeeklyPlanCalender();
        //    ViewBag.Badge = Da_obj.EventBadge(_WPB);

        //    _WPB.Empcd = emp;
        //    ViewBag.GetTodayEvents = Da_obj.GetTodayEvents(_WPB);
        //    GetEvents();
        //    return View();
        //}

        //public JsonResult GetEvents()
        //{
        //    string strReq = "";
        //    strReq = Session["Empcd"].ToString();
        //    string[] arrEmpcd;
        //    string[] arrBrcd;
        //    string[] arrMsgs;

        //    strReq = strReq.Replace("%2f", "/");
        //    strReq = strReq.Replace("%3F", "");
        //    strReq = strReq.Replace("%2F", "/");
        //    strReq = strReq.Replace("%2B", "+");
        //    strReq = EncryDecry.Decrypt(strReq);
        //    if (strReq != null)
        //    {
        //        arrMsgs = strReq.Split('&');
        //    }
        //    else
        //    {
        //        arrMsgs = null;
        //    }
        //    arrEmpcd = arrMsgs[0].Split('=');
        //    string emp = arrEmpcd[1].ToString().Trim();

        //    arrBrcd = arrMsgs[1].Split('=');
        //    string Brcd = arrBrcd[1].ToString().Trim();
        //    //-- date 

        //    //
        //    WeeklyPlanBO obj = new WeeklyPlanBO();
        //    try
        //    {
        //        obj.Empcd = emp;
        //        var ds = _weeklyPlan.GetWeeklyPlanEvents(obj);
        //        return new JsonResult { Data = ds, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //public JsonResult SaveEvent(WeeklyPlanBO obj)
        //{
        //    string strReq = "";
        //    strReq = Session["Empcd"].ToString();
        //    string[] arrEmpcd;
        //    string[] arrMsgs;

        //    strReq = strReq.Replace("%2f", "/");
        //    strReq = strReq.Replace("%3F", "");
        //    strReq = strReq.Replace("%2F", "/");
        //    strReq = strReq.Replace("%2B", "+");
        //    strReq = EncryDecry.Decrypt(strReq);
        //    if (strReq != null)
        //    {
        //        arrMsgs = strReq.Split('&');
        //    }
        //    else
        //    {
        //        arrMsgs = null;
        //    }
        //    arrEmpcd = arrMsgs[0].Split('=');
        //    string emp = arrEmpcd[1].ToString().Trim();

        //    var status = false;
        //    WeeklyPlanCalender Da_obj = new WeeklyPlanCalender();
        //    WeeklyPlanBO BO_obj = new WeeklyPlanBO();
        //    try
        //    {
        //        if (obj.EventID > 0)
        //        {
        //            obj.Empcd = emp;
        //            Da_obj.UpdateWeeklyPlan(obj);
        //        }
        //        else
        //        {
        //            obj.Empcd = emp;
        //            Da_obj.CreateWeeklyPlan(obj);
        //        }
        //        status = true;
        //        GetEvents();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}

        //[HttpPost]
        //public JsonResult DeleteEvent(WeeklyPlanBO obj)
        //{
        //    WeeklyPlanCalender Da_obj = new WeeklyPlanCalender();
        //    WeeklyPlanBO BO_obj = new WeeklyPlanBO();
        //    var status = false;
        //    try
        //    {
        //        BO_obj.EventID = obj.EventID;
        //        Da_obj.DeleteWeeklyPlan(BO_obj);
        //        status = true;
        //        return new JsonResult { Data = new { status = status } };
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //} 
        //#endregion

        //public ActionResult Badge()
        //{
        //    WeeklyPlanCalender Da_obj = new WeeklyPlanCalender();
        //    try
        //    {
        //        ViewBag.Badge = Da_obj.EventBadge();

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return View();
        //}

        public ActionResult DayandWeeklyPlans()
        {
            return View();
        }
        #region Weekly plan Calender

        public ActionResult tabs()
        {
            return View();
        }

        public ActionResult WeeklyPlans()
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
            return View();
        }

        public JsonResult GetEvents()
        {
            string strReq = "";
            strReq = Session["Empcd"].ToString();
            string[] arrEmpcd;
            string[] arrBrcd;
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

            arrBrcd = arrMsgs[1].Split('=');
            string Brcd = arrBrcd[1].ToString().Trim();

            try
            {
                using (HRAlign_InlandEntities1 dc = new HRAlign_InlandEntities1())
                {
                    var events = dc.Events.Where(x => x.Empcd == emp & x.deleteActive == "Y").ToList();
                    return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Event e)
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

            var status = false;
            try
            {

                using (HRAlign_InlandEntities1 dc = new HRAlign_InlandEntities1())
                {
                    if (e.EventID > 0)
                    {
                        //Update the event
                        var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Subject = e.Subject;
                            v.Start = e.Start;
                            v.End = e.End;
                            v.Description = e.Description;
                            v.IsFullDay = e.IsFullDay;
                            v.ThemeColor = e.ThemeColor;
                            v.Empcd = emp;
                            v.deleteActive = "Y";
                        }
                    }
                    else
                    {
                        e.deleteActive = "Y";
                        e.Empcd = emp;
                        dc.Events.Add(e);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(Event e)
        {
            var status = false;
            try
            {
                using (HRAlign_InlandEntities1 dc = new HRAlign_InlandEntities1())
                {
                    if (e.EventID > 0)
                    {
                        //Update the deleted event as "N"
                        var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                        if (v != null)
                        {
                            v.deleteActive = "N";
                        }
                    }
                    dc.SaveChanges();
                    status = true;
                }
                return new JsonResult { Data = new { status = status } };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}