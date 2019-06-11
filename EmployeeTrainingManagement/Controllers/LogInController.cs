using EmployeeTrainingManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeTrainingManagement.Controllers
{
    public class LogInController : Controller
    {
        ETMModel db = new ETMModel();
        public ActionResult Index()
        { return View(); }
        [HttpPost]
        public ActionResult CheckLogin(string username, string pwd)
        {
            int role;
            TMS_UserMaster userObj = db.TMS_UserMaster.Where(x => x.UserMail == username.Trim() && x.Password == pwd.Trim() && x.IsActive == true).FirstOrDefault();
            if (userObj != null)
            {
                TempData["name"] = userObj.Name;       
                Session["RoleId"] = userObj.RoleId;
                Session["UserDetails"] = userObj;
                role = userObj.RoleId;
                FormsAuthentication.SetAuthCookie(username, true);
                switch (role)
                {
                    case 1:
                        return RedirectToAction("Index", "Admin");             
                    case 2:
                        return RedirectToAction("Index", "Manager");
                    case 3:
                        return RedirectToAction("Index", "Employee");
                }
            }
            else { TempData["InvalidLoginMessage"] = "!! Invalid Username Or Password !!"; }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View("Index");
        }        
        public ActionResult UserDetails()
        {
            int UserId = ((TMS_UserMaster)Session["UserDetails"]).UserId;
            ViewModelClass.ModelUserDetails tms_details = (from a in db.TMS_UserMaster
                                                           join b in db.tMS_SkillMaster on a.PrimarySkillId equals b.SkillId
                                                           join c in db.TMS_RoleMaster on a.RoleId equals c.RoleId
                                                           join d in db.TMS_Designations on a.DesignationID equals d.DesignationID
                                                           join e in db.TMS_UserMaster on a.ManagerID equals e.ManagerID

                                                           where a.UserId == UserId && a.IsActive == true
                                                           select new ViewModelClass.ModelUserDetails
                                                           {
                                                               ManagerName = e.Name,
                                                               Designation = d.Name,
                                                               Role = c.RoleName,
                                                               Name = a.Name,
                                                               CurrentProject = "give a project here",
                                                               ReleaseDate = System.DateTime.Today,
                                                               PrimarySkill = b.Name,
                                                               DOJ = a.DOJ,
                                                               DOB = a.DOB,
                                                               RegisteredEmail = a.UserMail,
                                                               MobileNo = a.MobNo,
                                                               PAN = a.PAN,
                                                               Address = a.Address,
                                                               Experience = a.Experience,
                                                               PassportNumber = a.PassportNumber,
                                                               PassportExpiryDate = a.PassportExpiryDate }                                         ).FirstOrDefault();
            ViewBag.SingleUserDetails = tms_details;
            return View();
        }        
        public ActionResult ChangePassword()
        { return View(); }
        [HttpPost]
        public ActionResult ChangePassword(string NewPassword)
        {
            int id = ((TMS_UserMaster)Session["UserDetails"]).UserId;
            TMS_UserMaster tMS_UserMaster = (from a in db.TMS_UserMaster
                                             where a.UserId == id
                                             select a).FirstOrDefault();
            tMS_UserMaster.Password = NewPassword;
            db.SaveChanges();
            Session.Abandon();
            return RedirectToAction("Index");
        }}}