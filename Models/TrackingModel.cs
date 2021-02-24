using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTracking.Models
{

    public class TrackingTables
    {

        public List<TrackingSummary> trackingsummary { get; set; }


        public List<TrackingDetails> trackingdetails { get; set; }


        public List<TrackingGodown_details> tracking_godowndetails { get; set; }

    }

    public class TrackingSummary
    {
        public string Docketno { get; set; }
        public string PickupDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DeliveryDate { get; set; }
        public string DlyRemarks { get; set; }
        public string ReferenceNo { get; set; }
        public string Address { get; set; }
        public string DT { get; set; }
        public string GoogleETA { get; set; }

    }
    public class TrackingDetails
    {
        public string locationcode { get; set; }
        public string ArrivalDate { get; set; }
        public string arrivalTime { get; set; }
        public string EntryDate { get; set; }
        public string Origin { get; set; }
        public string Status { get; set; }
    }

    public class TrackingGodown_details
    {
        public string GodownCode { get; set; }
        public string GodownName { get; set; }
        public string ContactNo { get; set; }
        public string Arrivaldate { get; set; }
    }

    public class Customer_Complaint
    {
        public string Complaindesc { get; set; }
        public string complaincode { get; set; }
    }

    public class Customer_ComplaintBo
    {
        public string Complaindesc { get; set; }
        public string Customername { get; set; }
        public string ComplainType { get; set; }
        public string Address1 { get; set; }
        public string Mobile { get; set; }
        public string Pin { get; set; }
        public string Related { get; set; }
        public string Issue { get; set; }
        public string DocketNo { get; set; }
    }
}
