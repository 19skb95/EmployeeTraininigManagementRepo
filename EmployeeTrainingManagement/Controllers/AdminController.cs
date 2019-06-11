
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeTrainingManagement.Model;

namespace EmployeeTrainingManagement.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        ETMModel db = new ETMModel();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AllocateResources()
        {

            return View();
        }
        public ActionResult ViewAllResources()
        {

            return View();
        }
        
        public ActionResult ViewSkills()
        {
            int Role = Convert.ToInt32( Session["RoleId"]);
            int manager_id = ((TMS_UserMaster)Session["UserDetails"]).UserId;
            if (Role ==1)
            {
                List<ViewModelClass.ModelSkillDetails> skillList  = (from a in db.TMS_UserMaster
                                                  join b in db.tMS_SkillMaster on a.PrimarySkillId equals b.SkillId
                                                  select new ViewModelClass.ModelSkillDetails
                                                  {
                                                      DOJ = a.DOJ,
                                                      Name = a.Name,
                                                      PrimarySkill = b.Name
                                                  }).ToList();
                ViewBag.SkillList = skillList;
                return View();
            } 
            else if (Role == 2)
            {
                //only view skills of employee under his team
                //button with view all skills
                List<ViewModelClass.ModelSkillDetails> skillList = (from a in db.TMS_UserMaster
                                                                    join b in db.tMS_SkillMaster on a.PrimarySkillId equals b.SkillId
                                                                    where a.ManagerID == manager_id
                                                                    select new ViewModelClass.ModelSkillDetails
                                                                    {
                                                                        DOJ = a.DOJ,
                                                                        Name = a.Name,
                                                                        PrimarySkill = b.Name
                                                                    }).ToList();
                ViewBag.SkillList = skillList;
                return View();
            }
            return View();
        }

        #region Role MAster
        public ActionResult CreateRole(int? id)
        {
            TMS_RoleMaster obj = new TMS_RoleMaster();
            if (id != null)
            {
                obj = db.TMS_RoleMaster.Where(x => x.RoleId == id).FirstOrDefault();
                ViewBag.id = id;
            }
            ViewBag.List = db.TMS_RoleMaster.Where(x => x.IsActive == true).ToList();
            return View(obj);
        }
        [HttpPost]
        public ActionResult CreateRole(TMS_RoleMaster formObj)
        {
            try
            {
                if (formObj.RoleId == 0)
                {
                    TMS_RoleMaster obj = new TMS_RoleMaster();
                    obj.RoleName = formObj.RoleName;
                    obj.IsActive = true;
                    obj.CreatedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.CreatedOn = DateTime.Today;
                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    db.TMS_RoleMaster.Add(obj);
                    db.SaveChanges();
                    TempData["Sussess"] = "Record Inserted Successfully.";
                }
                else
                {
                    TMS_RoleMaster obj = db.TMS_RoleMaster.Where(x => x.RoleId == formObj.RoleId).FirstOrDefault();
                    obj.RoleName = formObj.RoleName;
                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    db.SaveChanges();
                    TempData["Sussess"] = "Role "+ formObj.RoleName.ToString() +" updated Successfully.";
                }   
            }
            catch (Exception)
            {  TempData["Warning"] = "Invalid Request /Validation Error";  }            
            return RedirectToAction("CreateRole");
        }

        public ActionResult DeleteRole(int? id)
        {
            try
            {
                db.TMS_RoleMaster.Where(x => x.RoleId == id).FirstOrDefault().IsActive = false;
                db.SaveChanges();
                TempData["Sussess"] = "Deleted Successfully.";
            }
            catch (Exception)
            {  TempData["Warning"] = "Invalid Request/ Cannot Delete Role."; }
            return RedirectToAction("CreateRole");
        }
        #endregion


        #region User MAster
        public ActionResult CreateUser(int? id)
        {
            TMS_UserMaster obj = new TMS_UserMaster();
            if (id != null)
            {
                obj = db.TMS_UserMaster.Where(x => x.UserId == id).FirstOrDefault();
                ViewBag.id = id;
            }
            List<ViewModelClass.ModelUserView> umList = (from a in db.TMS_UserMaster
                                                         join b in db.TMS_UserMaster on a.ManagerID equals b.UserId
                                                         join c in db.TMS_RoleMaster on a.RoleId equals c.RoleId
                                                         where a.IsActive == true
                                                         select new ViewModelClass.ModelUserView
                                                         {
                                                             id = a.UserId,
                                                             name = a.Name,
                                                             email = a.UserMail,
                                                             rolename = c.RoleName,
                                                             managername = b.Name,
                                                             dob = a.DOB,
                                                             doj = a.DOJ
                                                         }).ToList();

            umList = umList.OrderByDescending(x => x.id).ToList();
            ViewBag.List = umList;
            //Role DropDown
            ViewBag.rolelist = db.TMS_RoleMaster.Where(x => x.IsActive == true && x.RoleId != 1).Select(c => new SelectListItem
            {
                Value = c.RoleId.ToString(),
                Text = c.RoleName
            }).ToList();
            //MAnager DropDown
            ViewBag.mangerlist = GetManagerList();
            //Skill List Drop Down
            ViewBag.SkillList = GetSkillList();
            return View(obj);
        }
        public IList<SelectListItem> GetSkillList()
        {
            var skillList = db.tMS_SkillMaster.Where(x => x.IsActive == true).Select(c => new SelectListItem
            {   Value = c.SkillId.ToString(),
                Text = c.Name
            }).ToList();
            return skillList;
        }
        public IList<SelectListItem> GetManagerList()
        {
            var ManagerList = db.TMS_UserMaster.Where(x => x.RoleId == 2 && x.IsActive == true).Select(c => new SelectListItem
            {
                Value = c.UserId.ToString(),
                Text = c.Name
            }).ToList();
            return ManagerList;
        }
        [HttpPost]
        public ActionResult CreateUser(TMS_UserMaster formObj)
        {
            int EmailAlreadyRegistered = db.TMS_UserMaster.Where(x => x.UserMail == formObj.UserMail).Count();
            if(EmailAlreadyRegistered>0)
            {
                TempData["Warning"] = "Email Already Registered";
                return RedirectToAction("CreateUser");
            }
            try
            {
                if (formObj.UserId == 0)
                {
                    TMS_UserMaster obj = new TMS_UserMaster();
                    obj.RoleId = formObj.RoleId;
                    obj.ManagerID = formObj.ManagerID;
                    obj.UserMail = formObj.UserMail;
                    obj.DOB = Convert.ToDateTime(formObj.DOB);
                    obj.DOJ = Convert.ToDateTime(formObj.DOJ);
                    obj.Password = formObj.Password;
                    obj.Name = formObj.Name;
                    obj.IsActive = true;
                    obj.CreatedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.CreatedOn = DateTime.Today;
                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    obj.SkillId = "1,2,3,4";
                    obj.PrimarySkillId = formObj.PrimarySkillId;
                    obj.MobNo = formObj.MobNo.ToString();
                    obj.PAN = formObj.PAN.ToString();
                    obj.Address = formObj.Address;
                    obj.Experience = formObj.Experience;
                    obj.PassportExpiryDate = System.DateTime.Today;
                    obj.PassportNumber = "";
                    obj.DesignationID = 3;
                    db.TMS_UserMaster.Add(obj);
                    db.SaveChanges();
                    TempData["Success"] ="New User Registered Successfully.";
                }
                else      //Update
                {
                    TMS_UserMaster obj = db.TMS_UserMaster.Where(x => x.UserId == formObj.UserId).FirstOrDefault();
                    obj.RoleId = formObj.RoleId;
                    obj.ManagerID = formObj.ManagerID;
                    obj.UserMail = formObj.UserMail;
                    obj.DOB = Convert.ToDateTime(formObj.DOB);
                    obj.DOJ = Convert.ToDateTime(formObj.DOJ);
                    obj.Name = formObj.Name;
                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    db.SaveChanges();
                    TempData["Success"] = obj.Name.ToLower() +"'s Record Updated Successfully.";
                }
            }
            catch (Exception)
            {  TempData["Warning"] = "Invalid Request /Validation Error";  }               
            return RedirectToAction("CreateUser");
        }

        public ActionResult DeleteUser(int? id)
        {
            if (id != null)
            {
                List<TMS_UserMaster> Ulist = db.TMS_UserMaster.Where(x => x.ManagerID == id && x.IsActive == true).ToList();
                if (Ulist.Count != 0)
                {
                    ViewBag.MgrList = GetManagerList();
                }
                ViewBag.id = id;
            }
            return View(db.TMS_UserMaster.Where(x => x.UserId == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, int? ManagerID)
        {
            try
            {
                if (ManagerID != null)
                {
                    List<TMS_UserMaster> Ulist = db.TMS_UserMaster.Where(x => x.ManagerID == id && x.UserId != ManagerID && x.IsActive == true).ToList();
                    foreach (var uobj in Ulist)
                    {
                        uobj.ManagerID = ManagerID;
                    }
                }
                db.TMS_UserMaster.Where(x => x.UserId == id).FirstOrDefault().IsActive = false;
                db.SaveChanges();
                TempData["Success"] = "User Deleted Successfully.";
            }
            catch (Exception)
            {  TempData["Danger"] = "Invalid Request/ Cannot Delete."; }            
            return RedirectToAction("CreateUser");

        }
        #endregion

        #region CourseMAster

        public ActionResult CreateCourse(int? id)
        {
            TMS_CourseMaster obj = new TMS_CourseMaster();
            if (id != null)
            {
                obj = db.TMS_CourseMaster.Where(x => x.CourseId == id).FirstOrDefault();
                ViewBag.id = id;
            }
            ViewBag.List = db.TMS_CourseMaster.Where(x => x.IsActive == true).ToList().OrderByDescending(x => x.CourseId);
            return View(obj);

        }
        [HttpPost]
        public ActionResult CreateCourse(TMS_CourseMaster formObj)
        {
            try
            {
                if (formObj.CourseId == 0)
                {
                    TMS_CourseMaster obj = new TMS_CourseMaster();
                    obj.CourseName = formObj.CourseName;
                    obj.Duration = formObj.Duration;
                    obj.Description = formObj.Description;
                    obj.IsActive = true;
                    obj.CreatedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.CreatedOn = DateTime.Today;
                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    db.TMS_CourseMaster.Add(obj);
                    db.SaveChanges();
                    TempData["Success"] = formObj.CourseName +" Course Created Successfully.";
                }
                else
                {
                    TMS_CourseMaster obj = db.TMS_CourseMaster.Where(x => x.CourseId == formObj.CourseId).FirstOrDefault();
                    obj.CourseName = formObj.CourseName;
                    obj.Duration = formObj.Duration;
                    obj.Description = formObj.Description;
                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    db.SaveChanges();
                    TempData["Success"] =  formObj.CourseName+" Updated Successfully";
                }
            }
            catch (Exception)
            {   TempData["Danger"] = "Invalid request./ Data Invalid";  }          
            return RedirectToAction("CreateCourse");
        }

        public ActionResult DeleteCourse(int? id)
        {
            db.TMS_CourseMaster.Where(x => x.CourseId == id).FirstOrDefault().IsActive = false;
            db.SaveChanges();
            return RedirectToAction("CreateCourse");
        }
        #endregion

        #region-Batch MAster
        public ActionResult CreateBAtch(int? id)
        {
            TMS_BatchMaster obj = new TMS_BatchMaster();
            if (id != null)
            {
                obj = db.TMS_BatchMaster.Where(x => x.BatchId == id).FirstOrDefault();
                ViewBag.id = id;
            }
            ViewBag.Course = GetCourseList();
            List<ViewModelClass.ModelBatchView> Blist = (from a in db.TMS_BatchMaster
                                                         join b in db.TMS_CourseMaster on a.CourseId equals b.CourseId
                                                         where a.IsActive == true
                                                         select new ViewModelClass.ModelBatchView
                                                         {
                                                             BatchId = a.BatchId,
                                                             BatchName = a.BatchName,
                                                             CourseName = b.CourseName,
                                                             StartDate = a.StartDate,
                                                             EndDate = a.EndDate
                                                         }).ToList();

            ViewBag.List = Blist;
            return View(obj);
        }
        [HttpPost]
        public ActionResult CreateBatch(TMS_BatchMaster formObj)
        {
            try
            {
                if (formObj.BatchId == 0)
                {
                    formObj.BatchCount = null;
                    formObj.IsActive = true;
                    formObj.CreatedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    formObj.CreatedOn = DateTime.Today;
                    formObj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    formObj.ModifiedOn = DateTime.Today;
                    db.TMS_BatchMaster.Add(formObj);
                    db.SaveChanges();
                    TempData["Success"] = "Batch Created Successfully";
                }
                else
                {
                    TMS_BatchMaster obj = db.TMS_BatchMaster.Where(x => x.CourseId == formObj.CourseId).FirstOrDefault();
                    obj.BatchName = formObj.BatchName;
                    obj.BatchCount = formObj.BatchCount;
                    obj.StartDate = formObj.StartDate;
                    obj.EndDate = formObj.EndDate;
                    obj.EndDate = formObj.EndDate;
                    obj.CourseId = formObj.CourseId;

                    obj.ModifiedBy = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                    obj.ModifiedOn = DateTime.Today;
                    db.SaveChanges();
                    TempData["Success"] =  formObj.BatchName.ToLower().ToString()+" Batch Updated Successfully";
                }                
            }
            catch (Exception)
            {
                TempData["Danger"] = "Invalid Request/ Invlalid Data";
            }            
            return RedirectToAction("CreateBatch");
        }
        public IList<SelectListItem> GetCourseList()
        {
            var course = db.TMS_CourseMaster.Where(x => x.IsActive == true).Select(c => new SelectListItem
            {
                Value = c.CourseId.ToString(),
                Text = c.CourseName
            }).ToList();
            return course;
        }
        public ActionResult DeleteBatch(int? id)
        {
            db.TMS_BatchMaster.Where(x => x.BatchId == id).FirstOrDefault().IsActive = false;
            db.SaveChanges();
            return RedirectToAction("CreateBatch");

        }
        #endregion
    }
}