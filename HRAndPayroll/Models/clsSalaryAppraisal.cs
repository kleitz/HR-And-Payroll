using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRAndPayroll.Models
{
    public class clsSalaryAppraisal
    {
        
        public int Entry_Id { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<int> Salary_Amount { get; set; }
        public Nullable<System.DateTime> Next_Due_Date { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> DateTimeLastUpdated { get; set; }
        public Nullable<int> LastUpdateUserId { get; set; }
    }

    public class SalaryAppraisal
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MyDateTime
        {
            get
            {
                DateTime date = Convert.ToDateTime(DateTime.Now);
                return date;
            }
        }

        public int Entry_Id { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<int> Salary_Amount { get; set; }
        public Nullable<System.DateTime> Next_Due_Date { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> DateTimeLastUpdated { get; set; }
        public Nullable<int> LastUpdateUserId { get; set; }

       // 
    }
    public class ddlSalaryAppraisal
    {
        public Nullable<int> Employee_Id { get; set; }
        public string Employee_Code { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public String Full_Name { get; set; }

        public List<ddlSalaryAppraisal> ddlList { get; set; }
    }

}