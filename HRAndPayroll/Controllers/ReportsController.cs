using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAndPayroll.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        private Entities db = new Entities();


        public ActionResult PaySlipReport()
        {            
            return View();
        }

        [HttpPost, ActionName("PaySlipReport")]
        public ActionResult PaySlipReport(String Action)
        {
            String format;   
            //if (Action.Substring(Action.Length - 3, 3) == "PDF") format = "PDF"; 
            //else 
            if (Action.Substring(Action.Length - 4, 4) == "Word") format = "WORDOPENXML";
            else if (Action.Substring(Action.Length - 5, 5) == "Excel") format = "EXCELOPENXML";             
            else  format = "PDF"; 
            
            LocalReport slipReport = new LocalReport();
            if (System.IO.File.Exists(Server.MapPath("~/Report/Salary_Sheet.rdlc")))
                slipReport.ReportPath = Server.MapPath("~/Report/Salary_Sheet.rdlc");
            else
                return View();
            
            var ReportData = (from EM in db.Employee_Masters
                              join SS in db.Salary_Sheets
                                  on EM.Employee_Id equals SS.Employee_Id
                              join DM in db.Designation_Masters.Distinct()
                                  on EM.Designation_Id equals DM.Designation_Id
                              select new HRAndPayroll.Models.clsSalarySheet
                              {
                                  Employee_Code = EM.Employee_Code,
                                  Employee_Id = EM.Employee_Id,
                                  Employee_Name = EM.First_Name + " " + EM.Last_Name,
                                  Designation = DM.Designation_Name,
                                  //Salary_Month = (Int32)SS.Salary_Month,
                                  //Salary_Year = (Int32)SS.Salary_Year,
                                  Gross_Salary = (Int32)SS.Gross_Salary,
                                  Days_Payable = (Double)SS.Days_Payable,
                                  //Basic_Salary = SS.
                                  //HRA = 
                                  //Conveyance_Allowance = 
                                  //Medical_Allowance = 
                                  //Other_Allowance = 
                                  Incentive = (Double)SS.Incentive,
                                  TDS = (Double)SS.TDS,
                                  Net_Payable = (Double)SS.Net_Payable,
                                  ESI_EE_Share = (Double)SS.ESI_EE_Share,
                                  ESI_EY_Share = (Double)SS.ESI_EY_Share,
                                  EPF_EE_Share = (Double)SS.EPF_EE_Share,
                                  EPF_EY_Share = (Double)SS.EPF_EY_Share,
                                  DatePaid = (DateTime)SS.DatePaid,
                                  DateTimeLastUpdated = (DateTime)SS.DateTimeLastUpdated
                              }).Distinct().ToList();


            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ReportData);
            slipReport.DataSources.Add(reportDataSource);
            //slipReport.Refresh();

            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + format + "</OutputFormat>" +
            "  <PageWidth>20in</PageWidth>" +
            "  <PageHeight>8.2in</PageHeight>" +
            "  <MarginTop>0.2in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";
            
            //Render the report
           renderedBytes = slipReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
           using (FileStream fs = new FileStream(Server.MapPath("~/Report/Download") + "\\" + "Salary_Sheet_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") +"_"+ format, FileMode.Create))
           //using (FileStream fs = new FileStream(_jobPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf", FileMode.Create))
           {
               fs.Write(renderedBytes, 0, renderedBytes.Length);
               //return File(fs);
               fs.Close();
               fs.Dispose();
           }
           //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
            if (format == null)
            {
                return File(renderedBytes, "image/jpeg");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, format);
            }
            else
            {
                return File(renderedBytes, "PDF");
            }
            
            //return View();
        }

        


































        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Reports/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reports/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reports/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reports/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
