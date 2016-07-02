using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAndPayroll.Controllers
{
    public class SalarySheetController : Controller
    {
        //
        // GET: /SalarySheet/
        private Entities db = new Entities();
        HRAndPayroll.Models.clsSalarySheet objCls = new Models.clsSalarySheet();

        private List<HRAndPayroll.Models.clsSalarySheet> EmpList()
        {
            try
            {
                var SFdata = (from SF in db.Salary_Formulas
                              select SF).SingleOrDefault();
                var BasicSal = Int32.Parse(SFdata.Basic);
                var HRA = Int32.Parse(SFdata.HRA);
                var CA = Int32.Parse(SFdata.Conveyance_Allowance);
                var MA = Int32.Parse(SFdata.Medical_Allowance);
                var OA = Int32.Parse(SFdata.Other_Allowance);
                var EsiLimit = SFdata.ESI_Limit;
                var EpfLimit = SFdata.EPF_Limit;
                var ESIEE = float.Parse(SFdata.ESI_Employee_Share);
                var ESIEY = float.Parse(SFdata.ESI_Employer_Share);
                var EPFEE = float.Parse(SFdata.EPF_Employee_Share);
                var EPFEY = float.Parse(SFdata.EPF_Employer_Share);
                var DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                var EmpListQuery = (from EM in db.Employee_Masters
                                    join DM in db.Designation_Masters
                                        on EM.Designation_Id equals DM.Designation_Id
                                    join SM in db.Salary_Masters
                                        on EM.Employee_Id equals SM.Employee_Id
                                    //from SS in db.Salary_Sheets
                                    orderby EM.Employee_Code ascending
                                    select new HRAndPayroll.Models.clsSalarySheet
                                    {
                                        Employee_Id = EM.Employee_Id,
                                        Employee_Name = EM.First_Name + " " + EM.Last_Name,
                                        Employee_Code = EM.Employee_Code,
                                        Designation = DM.Designation_Name,
                                        Gross_Salary = (Int32)(SM.Salary_Amount),
                                        Basic_Salary = (float)((BasicSal) * (SM.Salary_Amount) / 100),
                                        HRA = (Double)((HRA) * (SM.Salary_Amount) / 100),
                                        Conveyance_Allowance = (Double)((CA) * (SM.Salary_Amount) / 100),
                                        Medical_Allowance = (Double)((MA) * (SM.Salary_Amount) / 100),
                                        Other_Allowance = (Double)((OA) * (SM.Salary_Amount) / 100),
                                        EPF_EE_Share = ((float)((BasicSal) * (SM.Salary_Amount) / 100)) <= EpfLimit ? EPFEE * ((float)((BasicSal) * (SM.Salary_Amount) / 100)) / 100 : 0,
                                        EPF_EY_Share = ((float)((BasicSal) * (SM.Salary_Amount) / 100)) <= EpfLimit ? EPFEY * ((float)((BasicSal) * (SM.Salary_Amount) / 100)) / 100 : 0,
                                        ESI_EE_Share = (Int32)(SM.Salary_Amount) <= EsiLimit ? (double)((ESIEE * (Int32)(SM.Salary_Amount)) / 100) : 0,
                                        ESI_EY_Share = (Int32)(SM.Salary_Amount) <= EsiLimit ? (double)((ESIEY * (Int32)(SM.Salary_Amount)) / 100) : 0,
                                        Days_Payable = DaysInMonth
                                        //DatePaid = DateTime.Now,
                                        //DateTimeLastUpdated = DateTime.Now,
                                        //LastUpdateUserId = 1,
                                        //Entry_Id=0,
                                        //TDS = 0,
                                        //Incentive = 0, 
                                        //Net_Payable = 0
                                    }).Distinct();
                return EmpListQuery.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        

        public ActionResult ViewSalarySheet()
        {
            objCls.mySalarySheetList = EmpList();
            return View(objCls);
        }

        [HttpPost, ActionName("ViewSalarySheet")]
        public ActionResult ViewSalarySheet(String Action, String gridData, String SalaryMonth, String SalaryYear)
        {
            try
            {
                if (Action == "ViewSalarySheet" && gridData != null && gridData != "")
                {                    
                       Session["gridData"] = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(gridData);
                       return RedirectToAction("ViewSalarySheet", "SalarySheet");
                }
                if (Action == "Save")
                {
                    if (Session["gridData"] != null)
                    {
                        var gridDataList = Session["gridData"] as Dictionary<string, string[]>;
                        var arrList = gridDataList.Values.ToArray();
                        Int32 countNew = 0 , countExist = 0 ;
                        foreach (var row in arrList)
                        {
                            HRAndPayroll.Salary_Sheet obj = new Salary_Sheet();

                            obj.Salary_Month = DateTime.Now.Month;
                            obj.Salary_Year = DateTime.Now.Year;

                            var ECode = row[1];
                            Int32 EmpId = (from e in db.Employee_Masters
                                           where e.Employee_Code == ECode
                                           select e.Employee_Id).FirstOrDefault();
                            obj.Employee_Id = EmpId;
                            obj.Gross_Salary = Convert.ToInt32(row[3].ToString());
                            obj.ESI_EE_Share = Convert.ToDouble(row[9]);
                            obj.ESI_EY_Share = Convert.ToDouble(row[10]);
                            obj.EPF_EE_Share = Convert.ToDouble(row[11]);
                            obj.EPF_EY_Share = Convert.ToDouble(row[12]);
                            obj.TDS = Convert.ToDouble(row[13]);
                            obj.Incentive = Convert.ToDouble(row[14]);
                            obj.Days_Payable = Convert.ToDouble(row[15]);
                            obj.Net_Payable = Convert.ToDouble(row[16]);
                            obj.DatePaid = DateTime.Now;
                            obj.LastUpdateUserId = (Int32)Session["UserId"];
                            obj.DateTimeLastUpdated = DateTime.Now;

                            var salQue = (from SS in db.Salary_Sheets
                                          where SS.Employee_Id==obj.Employee_Id && SS.Salary_Month == obj.Salary_Month && SS.Salary_Year == obj.Salary_Year 
                                          select SS.Employee_Id).FirstOrDefault();
                            if (salQue != obj.Employee_Id)
                            {
                                db.Salary_Sheets.Add(obj);
                                db.SaveChanges();
                                countNew++;
                            }
                            else
                            {
                                countExist++;
                                //List<Int32> list = new List<int>();
                                // Int32[] list = new Int32[100];
                                //list[count] = (Int32)salQue;
                                //var list = new List<int>();
                                //list.Add((Int32)salQue);
                                //list.Add((Int32)salQue);
                                //var paidId = list.ToArray();
                                //paidId.ToList().Insert(count, (Int32)salQue);
                                //count++;
                                //return Content("<script language='javascript' type='text/javascript'>alert('The Salary has already been credited for this month!!');window.location.replace('ViewSalarySheet');</script>");
                            }
                        }
                        if (countNew > 0)
                            return Content("<script language='javascript' type='text/javascript'>alert('The Salary Data has been saved successfully!');window.location.replace('ViewSalarySheet');</script>");
                        if (countExist > 0)
                            return Content("<script language='javascript' type='text/javascript'>alert('The Salary has already been credited for this month!!');window.location.replace('ViewSalarySheet');</script>");

                    }
                }
                if (Action == "Search")
                {                    
                    List<HRAndPayroll.Models.clsSalarySheet> obj = new List<Models.clsSalarySheet>();
                    obj = EmpList();                    
                    if (SalaryMonth != null && SalaryMonth != "")
                    {
                        var Month = Convert.ToInt32(SalaryMonth);
                        obj = (EmpList().Where(s => s.Salary_Month == Month)).ToList();

                    }
                    if (SalaryYear != "" && SalaryYear != null)
                    {
                        var Year = Convert.ToInt32(SalaryYear);
                        obj = (EmpList().Where(s => s.Salary_Year == Year)).ToList();
                        //return View(search);
                    }
                    if (SalaryMonth != null && SalaryMonth != "" && SalaryYear != "" && SalaryYear != null)
                    {
                        var Month = Convert.ToInt32(SalaryMonth);
                        var Year = Convert.ToInt32(SalaryYear);
                        obj = (EmpList().Where(s => s.Salary_Month == Month && s.Salary_Year == Year)).ToList();
                    }
                    objCls.mySalarySheetList = obj;
                    return View(objCls);
                   
                }
                if (Action == "Clear")
                {
                    return RedirectToAction("ViewSalarySheet", "SalarySheet");
                }
                if (Action == "Generate Pay Slip")
                {
                    //return Content("<script language='javascript' type='text/javascript'>alert('Not working right now!');window.location.replace('ViewSalarySheet');</script>");

                }
                if (Action == "Generate Salary Sheet")
                {
                    return RedirectToAction("PaySlipReport", "Reports");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            objCls.mySalarySheetList = EmpList();
            return View(objCls);
        }










        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SalarySheet/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SalarySheet/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SalarySheet/Create

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
        // GET: /SalarySheet/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SalarySheet/Edit/5

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
        // GET: /SalarySheet/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SalarySheet/Delete/5

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
