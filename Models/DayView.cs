using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTracking.Models
{
    //Subject,Description,Start,End
    public class DayView
    {
        //   public List<DayView> dayview { get; set; } 
        public string AllDay { get; set; }
        public int EventID { get; set; }
        public string Subject { get; set; }
        public List<Description> Description { get; set; }
        public string Start { get; set; }
        public string Desc { get; set; }
        public string Message { get; set; }
        public string End { get; set; }
        public string ThemeColor { get; set; }
        public string IsFullDay { get; set; }
        public string deleteActive { get; set; }
        public string Empcd { get; set; }
        public string EventDate { get; set; }
        public string PlanDate { get; set; }
        public string dayhourid { get; set; }
    }

    public class Description
    {
        public string Desc{ get; set; }
        public string dayhour { get; set; }
        public string EventDate { get; set; }
        public string dayhourid { get; set; }
    }

    public class DayPlans
    {
        public string Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Description { get; set; }
        public string Empcd { get; set; }

        public int EventID { get; set; }
    }
    public class ListPlans
    {
        public List<DayPlans> DayPlan { get; set; }
    }
}