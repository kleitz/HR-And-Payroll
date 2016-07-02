using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAndPayroll.Models
{

    public class clsSalarySheet
    {
        public Int32 Entry_Id { get; set; }
        public Int32 Salary_Month
        {
            // get; set;
            get
            {
                Int32 date = DateTime.Now.Month;
                return date;
            }
        }

        public Int32 Salary_Year
        {
            //get; set;
            get
            {
                Int32 date = DateTime.Now.Year;
                return date;
            }
        }

        public Int32 Gross_Salary { get; set; }

        public float Basic_Salary { get; set; }

        public Int32 Employee_Id { get; set; }

        public String Employee_Name { get; set; }

        public String Employee_Code { get; set; }

        public String Designation { get; set; }

        public Double HRA { get; set; }

        public Double Conveyance_Allowance { get; set; }

        public Double Medical_Allowance { get; set; }

        public Double Other_Allowance { get; set; }

        public Double Days_Payable { get; set; }

        public Double ESI_EE_Share { get; set; }

        public Double ESI_EY_Share { get; set; }

        public Double EPF_EE_Share { get; set; }

        public Double EPF_EY_Share { get; set; }

        public Double TDS { get; set; }

        public Double Incentive { get; set; }

        public Double Net_Payable { get; set; }

        public DateTime DatePaid { get; set; }

        public DateTime DateTimeLastUpdated { get; set; }

        public int LastUpdateUserId { get; set; }

        public List<clsSalarySheet> mySalarySheetList { get; set; }

        public IEnumerable<SelectListItem> Months
        {
            get
            {
                List<SelectListItem> months = new List<SelectListItem>();
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                for (Int32 j = 1; j <= DateTime.Now.Month; j++)
                {
                    var month = new SelectListItem { Text = info.GetMonthName(j), Value = j.ToString() };
                    months.Add(month);
                }
                return months;
                //return DateTimeFormatInfo
                //       .InvariantInfo
                //       .MonthNames
                //       .Where(m => !String.IsNullOrEmpty(m))
                //       .Select((monthName, index) => new SelectListItem
                //       {
                //           Value = (index + 1).ToString(),
                //           Text = monthName
                //       });
            }
        }
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                List<SelectListItem> yrs = new List<SelectListItem>();
                Int32 current_year = DateTime.Now.Year;
                for (Int32 i = 0; i < 4; i++)
                {
                    var Year = new SelectListItem { Text = current_year.ToString(), Value = current_year.ToString() };
                    yrs.Add(Year);
                    current_year--;
                }
                return yrs;
            }

        }
    }
    public class salarySheet
    {
        public int Entry_Id { get; set; }
        public Nullable<int> Salary_Month {
            get
            {
                Int32 date = DateTime.Now.Month;
                return date;
            }
        }
        public Nullable<int> Salary_Year {
            get
            {
                Int32 date = DateTime.Now.Year;
                return date;
            }
        }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<int> Gross_Salary { get; set; }
        public Nullable<double> Days_Payable { get; set; }
        public Nullable<double> ESI_EE_Share { get; set; }
        public Nullable<double> ESI_EY_Share { get; set; }
        public Nullable<double> EPF_EE_Share { get; set; }
        public Nullable<double> EPF_EY_Share { get; set; }
        public Nullable<double> TDS { get; set; }
        public Nullable<double> Incentive { get; set; }
        public Nullable<double> Net_Payable { get; set; }
        public Nullable<System.DateTime> DatePaid { get; set; }
        public Nullable<System.DateTime> DateTimeLastUpdated { get; set; }
        public Nullable<int> LastUpdateUserId { get; set; }

        
    }


    
}