using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAndPayroll.Controllers
{
    public class SalaryFormulaeController : Controller
    {
        //HRAndPayroll_TestEntities db = new HRAndPayroll_TestEntities();
        private Entities db = new Entities();
        //
        // GET: /SalaryFormulae/
        //public ActionResult 
        /// <summary>
        /// ViewFormulae
        /// </summary>
        public ActionResult ViewFormulae()
        {
            HRAndPayroll.Salary_Formula myFormula = new Salary_Formula();
            myFormula.myFormulae = db.Salary_Formulas.ToList();
            //HRAndPayroll.Models.Formulae objFormulae = new Models.Formulae();
            //objFormulae.AllFormulae = db.Salary_Formulas.ToList();
            return View(myFormula);
        }
        [HttpPost, ActionName("ViewFormulae")]
        [ValidateInput(false)]
        public ActionResult ViewFormulae(String Action, String StartDate, String basic, String HRA, String ConveyanceAllowance, String MedicalAllowance)
        {
            try
            {
                if (Action == "Search")
                {
                    
                    var que = from sf in db.Salary_Formulas
                              select new HRAndPayroll.Models.clsSalaryFormulae
                              {
                                  Entry_Id = sf.Entry_Id,
                                  Start_Date = sf.Start_Date,
                                  Basic = sf.Basic,
                                  HRA = sf.HRA,
                                  Conveyance_Allowance = sf.Conveyance_Allowance,
                                  Medical_Allowance = sf.Medical_Allowance,
                                  Other_Allowance = sf.Other_Allowance,
                                  EPF_Limit = sf.EPF_Limit,
                                  EPF_Employee_Share = sf.EPF_Employee_Share,
                                  EPF_Employer_Share = sf.EPF_Employer_Share,
                                  EPF_Applicable_On = sf.EPF_Applicable_On,
                                  ESI_Limit = sf.ESI_Limit,
                                  ESI_Employee_Share = sf.ESI_Employee_Share,
                                  ESI_Employer_Share = sf.ESI_Employer_Share,
                                  ESI_Applicable_On = sf.ESI_Applicable_On,
                                  DateTimeLastUpdated = sf.DateTimeLastUpdated,
                                  LastUpdateUserId = sf.LastUpdateUserId
                              };
                    if (StartDate != "" && StartDate != null) 
                    { 
                        DateTime sDate = Convert.ToDateTime(StartDate);
                        que = que.Where(m => m.Start_Date == sDate); 
                    }
                    if (basic != "" && basic != null) { que = que.Where(m => m.Basic.Contains(basic)); }
                    if (HRA != "" && HRA != null) { que = que.Where(m => m.HRA.Contains(HRA)); }
                    if (ConveyanceAllowance != "" && ConveyanceAllowance != null) { que = que.Where(m => m.Conveyance_Allowance.Contains(ConveyanceAllowance)); }
                    if (MedicalAllowance != "" && MedicalAllowance != null) { que = que.Where(m => m.Medical_Allowance.Contains(MedicalAllowance)); }
                    HRAndPayroll.Models.clsSalaryFormulae obj = new Models.clsSalaryFormulae();
                    obj.myFormulae = que.ToList();
                    return View(obj);
                }
                if (Action == "Clear") 
                {
                    ModelState.Clear();
                    HRAndPayroll.Salary_Formula myFormula = new Salary_Formula();
                    myFormula.myFormulae = db.Salary_Formulas.ToList();
                    return View(myFormula);
                }
                
                return View("ViewFormulae");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

               
        public ActionResult Delete(Int32 Id)
        {
            Salary_Formula del = db.Salary_Formulas.Find(Id);
            db.Salary_Formulas.Remove(del);
            db.SaveChanges();
            return RedirectToAction("ViewFormulae");
        }



        /// <summary>
        /// AddFormulae
        /// </summary>
        public ActionResult AddFormulae(Int32 Id = 0)
        {
            Salary_Formula formulae = db.Salary_Formulas.Find(Id);
            if (formulae == null)
            {
                ViewBag.btn = "Save";
                return View();
            }
            else
            {
                ViewBag.btn = "Update";
                return View(formulae);
            }
        }
        /// <summary>
        /// Post method to save data
        /// </summary>
        /// 
        [HttpPost, ActionName("AddFormulae")]
        public ActionResult AddFormulae(Salary_Formula formula, String Action, Int32 Id = 0)
        {
            try
            {
                //if(String.IsNullOrEmpty(formula.Other_Allowance))
                //    ModelState.AddModelError("Other_Allowance","*");
                if (Action == "Save" || Action == "Update")
                {
                    if (ModelState.IsValid)
                    {

                        formula.DateTimeLastUpdated = DateTime.Now;
                        formula.LastUpdateUserId = (Int32)Session["UserId"]; 
                        if (Id == 0)
                        {
                            db.Salary_Formulas.Add(formula);
                            db.SaveChanges();
                            ModelState.Clear();
                            return Content("<script language='javascript' type='text/javascript'>alert('Data has been saved successfully!');window.location.replace('ViewFormulae');</script>");
                        }
                        else
                        {
                            formula.Entry_Id = Id;
                            db.Entry(formula).State = EntityState.Modified;
                            db.SaveChanges();
                            return Content("<script language='javascript' type='text/javascript'>alert('Data has been updated successfully!');window.location.replace('ViewFormulae');</script>");

                        }
                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                    }
                }

                if (Action == "Cancel")
                {
                    ModelState.Clear();
                    return RedirectToAction("ViewFormulae", "SalaryFormulae");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(formula);
        }








        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SalaryFormulae/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SalaryFormulae/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SalaryFormulae/Create

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
        // GET: /SalaryFormulae/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SalaryFormulae/Edit/5

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

        ////
        //// GET: /SalaryFormulae/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /SalaryFormulae/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
