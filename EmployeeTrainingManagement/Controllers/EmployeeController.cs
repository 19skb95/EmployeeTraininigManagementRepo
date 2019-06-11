using EmployeeTrainingManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeTrainingManagement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        ETMModel db = new ETMModel();
        // GET: Employee
        public ActionResult Index()
        {  return View(); }

        #region Request
        public ActionResult CreateRequest(int? id)
        {
          int   userID = ((TMS_UserMaster)Session["UserDetails"]).UserId;
            List<ViewModelClass.ModelBatchView> Blist = (from a in db.TMS_BatchMaster
                                                          join b in db.TMS_CourseMaster on a.CourseId equals b.CourseId
                                                          //where a.IsActive == true
                                                          // join c in db.TMS_Request on a.BatchId equals c.BatchId
                                                          where a.IsActive == true
                                                         select new ViewModelClass.ModelBatchView
                                                         {
                                                             BatchId = a.BatchId,
                                                             BatchName = a.BatchName,
                                                             CourseDuration = b.Duration,
                                                             CourseName = b.CourseName,
                                                             StartDate = a.StartDate,
                                                             EndDate = a.EndDate
                                                         }).ToList();


            ViewBag.List = Blist;
            return View(); }
        public ActionResult RequestForm(int? id)
        {
            if (id != null)
            {  ViewBag.bobj = getBatch(id); }
            return View();
        }
        public ViewModelClass.ModelBatchView getBatch(int? id)
        {
            ViewModelClass.ModelBatchView Bobj = (from a in db.TMS_BatchMaster
                                                  join  b in db.TMS_CourseMaster on a.CourseId equals b.CourseId
                                                  where a.IsActive == true && a.BatchId == id
                                                  select new ViewModelClass.ModelBatchView
                                                  {
                                                      BatchId = a.BatchId,
                                                      BatchName = a.BatchName,
                                                      CourseDuration = b.Duration,
                                                      CourseName = b.CourseName,
                                                      StartDate = a.StartDate,
                                                      EndDate = a.EndDate,
                                                      BatchCount = a.BatchCount
                                                  }).FirstOrDefault();

            return Bobj; }
        [HttpPost]
        public ActionResult RequestForm(int bid)
        {
            try
            {
                int user_id = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                int q = (from a in db.TMS_Request
                         join b in db.TMS_BatchMaster on a.BatchId equals b.BatchId
                         join c in db.TMS_UserMaster on a.UserId equals user_id
                         where b.BatchId == bid
                         select a.RequestId).Count();
                if (q == 0)
                {
                    if (Session["UserDetails"] != null)
                    {
                        TMS_UserMaster usrObj = (TMS_UserMaster)Session["UserDetails"];
                        int empid = usrObj.UserId;
                        var EmpObj = db.TMS_UserMaster.Where(x => x.IsActive == true && x.UserId == empid).FirstOrDefault();
                        var batch = getBatch(bid);
                        TMS_Request objrqst = new TMS_Request();
                        objrqst.BatchId = batch.BatchId;
                        objrqst.ManagerId = Convert.ToInt32(EmpObj.ManagerID);
                        objrqst.UserId = EmpObj.UserId;
                        objrqst.Status = "P";
                        objrqst.IsActive = true;
                        objrqst.CreatedBy = 12;
                        objrqst.CreatedOn = DateTime.Today;
                        objrqst.ModifiedBy = 1;
                        objrqst.ModifiedOn = DateTime.Today;
                        db.TMS_Request.Add(objrqst);
                        db.SaveChanges();
                        int mgrid = objrqst.ManagerId;
                        TempData["Success"] = "Request has been Forwared to the manager.";
                    }}
                else{
                    TempData["Warning"] = "Already Registered in Batch";
                    return RedirectToAction("CreateRequest"); }}
            catch (Exception)
            { TempData["Danger"] = "Invlaid Request/ Invalid Data"; }
            return RedirectToAction("CreateRequest");
        }
        public ActionResult DeleteCourse(int? id)
        {
            try
            {
                db.TMS_CourseMaster.Where(x => x.CourseId == id).FirstOrDefault().IsActive = false;
                db.SaveChanges();
                TempData["Success"] = "Course Deleted Successfully.";                
            }
            catch (Exception)
            { TempData["Danger"] = "Invalid Request/ Invalid Data"; }
            return RedirectToAction("CreateCourse");
        }
        #endregion
    }
}