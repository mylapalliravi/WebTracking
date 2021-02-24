using System;
using System.Web.Mvc;
using WebTracking.Models;
using WebTracking.DAL;


namespace WebTracking.Controllers
{
    public class complaintController : Controller
    {
        ComplaintDAL cmplnt_DAL = new ComplaintDAL();
        // GET: Customer_complaint
        public ActionResult Customer_Complaint(Customer_Complaint obj)
        {
            try
            {
                Customer_Complaint _Complaint = new Customer_Complaint();
                ViewBag.Related = cmplnt_DAL.DrpRelated(obj);

            }
            catch (Exception e)
            {

                ViewBag.Exception = e;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Customer_Complaint(Customer_ComplaintBo obj, FormCollection form)
        {
            try
            {
                obj.Address1 = form["Address1"];
                obj.Pin = form["Pin"];
                obj.Mobile = form["Mobile"];
                obj.Customername = form["Customername"];
                obj.Related = form["complaincode"];
                //obj.ComplainType = form["ComplainType"].ToString();
                obj.Issue = form["Issue"];
                obj.DocketNo = form["DocketNo"].ToString();
                Customer_ComplaintBo _Complaint = new Customer_ComplaintBo();
                ViewBag.flag = 1;
                ViewBag.Msg = cmplnt_DAL.Customer_Complaint(obj);
            }
            catch (Exception e)
            {

                ViewBag.Exception = e;
            }
            return View();
        }
    }
}