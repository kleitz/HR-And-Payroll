using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAndPayroll.Controllers
{
    public class SalaryAppraisalController : Controller
    {
        //
        private Entities db = new Entities();
        HRAndPayroll.Models.clsSalaryAppraisal objMain = new Models.clsSalaryAppraisal();
        HRAndPayroll.Models.SalaryAppraisal objForQuery = new Models.SalaryAppraisal();

        public ActionResult AddSalaryAppraisal()
        {
            var q = from em in db.Employee_Masters
                    select new HRAndPayroll.Models.ddlSalaryAppraisal
                    {
                        Employee_Id=em.Employee_Id,
                        Full_Name=em.First_Name + " " + em.Last_Name + " (" + em.Employee_Code + ")"
                    };
            //HRAndPayroll.Models.ddlSalaryAppraisal ddl = new Models.ddlSalaryAppraisal();           
            //ddl.ddlList = q.ToList();
            ViewData["list"] = q.ToList();
            ViewBag.EntryDate = objForQuery.MyDateTime;

            return View();
        }

        [HttpPost, ActionName("AddSalaryAppraisal")]
        public ActionResult AddSalaryAppraisal(Salary_Master SM, String Action, String ddlName)
        {
            try
            {
                if (Action == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        if(ddlName=="" || ddlName==null)
                            return Content("<script language='javascript' type='text/javascript'>alert('Please Select Employee from Dropdownlist!');window.location.replace('AddSalaryAppraisal');</script>");
                        SM.Employee_Id = Convert.ToInt32(ddlName);
                        SM.Entry_Date = objForQuery.MyDateTime;
                        SM.DateTimeLastUpdated = DateTime.Now;
                        SM.LastUpdateUserId = Convert.ToInt32(Session["UserId"]);
                        db.Salary_Masters.Add(SM);
                        db.SaveChanges();
                        ModelState.Clear();
                        return Content("<script language='javascript' type='text/javascript'>alert('Data has been saved successfully!');window.location.replace('ViewSalaryAppraisal');</script>");
                        
                    }
                }
                if (Action == "Cancel")
                {
                    ModelState.Clear();
                    return RedirectToAction("ViewSalaryAppraisal", "SalaryAppraisal");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }


        public ActionResult ViewSalaryAppraisal()
        {
            return View();
        }





















        // GET: /SalaryAppraisal/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SalaryAppraisal/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SalaryAppraisal/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SalaryAppraisal/Create

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
        // GET: /SalaryAppraisal/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SalaryAppraisal/Edit/5

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
        // GET: /SalaryAppraisal/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SalaryAppraisal/Delete/5

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
