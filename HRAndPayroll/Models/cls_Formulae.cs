using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;

namespace HRAndPayroll.Models
{
    public class Formulae
    {
        public List<Salary_Formula> AllFormulae { get; set; }
    }

    

    public class loginDetails
    {
        public int Employee_Id { get; set; }
        
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class TestEmp
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Entites"].ConnectionString);
            //public IEnumerable<loginDetails> GetEmployeeList()
            //        {
            //            String que = "Select Employee_Id,First_Name,Last_Name FROM [Employee Master]";
            //            var list = 
            //            return list;
            //        }
        }
    
   
    public class clsSalaryFormulae
    {
        public int Entry_Id { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public string Basic { get; set; }
        public string HRA { get; set; }
        public string Conveyance_Allowance { get; set; }
        public string Medical_Allowance { get; set; }
        public string Other_Allowance { get; set; }
        public Nullable<int> EPF_Limit { get; set; }
        public string EPF_Employee_Share { get; set; }
        public string EPF_Employer_Share { get; set; }
        public string EPF_Applicable_On { get; set; }
        public Nullable<int> ESI_Limit { get; set; }
        public string ESI_Employee_Share { get; set; }
        public string ESI_Employer_Share { get; set; }
        public string ESI_Applicable_On { get; set; }
        public Nullable<System.DateTime> DateTimeLastUpdated { get; set; }
        public Nullable<int> LastUpdateUserId { get; set; }

        public List<clsSalaryFormulae> myFormulae { get; set; }
        
    }

    public class Test
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

        [Required(ErrorMessage = "*")]
        [Display(Name = "Entry Date")]
        public DateTime Entry_Date { get; set; }

        public Nullable<int> Employee_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "*")]
        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Salary_Amount")]
        public Nullable<int> Salary_Amount { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "*")]
        [Display(Name = "Next Due Date")]
        public DateTime Next_Due_Date { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public DateTime DateTimeLastUpdated { get; set; }
        public Nullable<int> LastUpdateUserId { get; set; }
    }
}