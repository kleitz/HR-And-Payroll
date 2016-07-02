using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRAndPayroll.Controllers
{
    public class AccountController : Controller
    {
        //private HRAndPayroll_TestModel model = new HRAndPayroll_TestEntities();
        private Entities model = new Entities();

        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Login(Employee_Master user, bool Check = false)
        {
            if (String.IsNullOrEmpty(user.Employee_Code))
                ModelState.AddModelError("Employee_Code", "Please Enter Username!");
            if (String.IsNullOrEmpty(user.Password))
                ModelState.AddModelError("Password", "Please Enter Password!");
            if (ModelState.IsValid)
            {
                var userData = model.Employee_Masters.Where(u => u.Employee_Code.Equals(user.Employee_Code) && u.Password.Equals(user.Password)).FirstOrDefault();
                if (userData != null)
                {
                    //Session["EmpCode"] = userData.Employee_Code.ToString();
                    Session["EmpName"] = userData.First_Name.ToString() + " " + userData.Last_Name.ToString();
                    Session["UserId"] = userData.Employee_Id;
                    return RedirectToAction("Dashboard", "Home");

                }
                else
                {
                    ModelState.AddModelError("Password", "Please Enter valid Username/Password!");
                    //return Content("<script language='javascript' type='text/javascript'>alert('Please Enter Valid Username/Password!');window.location.replace('');</script>");
                }

            }
            return View(user);

        }
        
        //[HttpPost, ActionName("Login")]
        //public ActionResult Login(HRAndPayroll.cls_Formulae, bool Check = false)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("CheckUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //    }
        //    return View(user);

        //}
        //
        // GET: /Account/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee_Master employee_master = model.Employee_Masters.Find(id);
            if (employee_master == null)
            {
                return HttpNotFound();
            }
            return View(employee_master);
        }

        //
        // GET: /Account/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee_Master employee_master)
        {
            if (ModelState.IsValid)
            {
                model.Employee_Masters.Add(employee_master);
                model.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee_master);
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee_Master employee_master = model.Employee_Masters.Find(id);
            if (employee_master == null)
            {
                return HttpNotFound();
            }
            return View(employee_master);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee_Master employee_master)
        {
            if (ModelState.IsValid)
            {
                model.Entry(employee_master).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee_master);
        }

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employee_Master employee_master = model.Employee_Masters.Find(id);
            if (employee_master == null)
            {
                return HttpNotFound();
            }
            return View(employee_master);
        }

        //
        // POST: /Account/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee_Master employee_master = model.Employee_Masters.Find(id);
            model.Employee_Masters.Remove(employee_master);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            model.Dispose();
            base.Dispose(disposing);
        }
    }
}