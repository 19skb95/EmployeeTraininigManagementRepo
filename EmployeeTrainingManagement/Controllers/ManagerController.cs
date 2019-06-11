using EmployeeTrainingManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeTrainingManagement.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        ETMModel db = new ETMModel();
        // GET: Manager
        public ActionResult Index()
        { return View(); }
        #region MyTeam
        public ActionResult MyTeam()
        {
            int manager_id = ((TMS_UserMaster)Session["UserDetails"]).UserId;
            List<ViewModelClass.ModelMyTeam> MyTeamListt = (from a in db.TMS_UserMaster
                                                           where a.IsActive == true && a.ManagerID == manager_id
                                                           select new ViewModelClass.ModelMyTeam
                                                           {
                                                               UserId = a.UserId,
                                                               UserName = a.Name,
                                                               CurrentProject = "noproj",
                                                               ReleaseDate = System.DateTime.Today,    
                                                           }).ToList();
            ViewBag.MyTeamList = MyTeamListt;
            return View(); }
        #endregion
        public ActionResult BatchDetails()
        {
            if (Session["UserDetails"] != null)
            {
                TMS_UserMaster uObj = (TMS_UserMaster)Session["UserDetails"];
                var RequestList = GetRequestList(uObj.UserId);
                if (RequestList.Count != 0)
                { ViewBag.List = RequestList; }}
            return View();
        }
        public List<ViewModelClass.ViewRequestModel> GetRequestList(int mgrid)
        {   List<ViewModelClass.ViewRequestModel> Rlist = (from a in db.TMS_Request
                                                           join b in db.TMS_BatchMaster on a.BatchId equals b.BatchId
                                                           join c in db.TMS_CourseMaster on b.CourseId equals c.CourseId
                                                           join d in db.TMS_UserMaster on a.UserId equals d.UserId
                                                           join e in db.TMS_UserMaster on a.ManagerId equals e.UserId
                                                           where a.IsActive == true && a.ManagerId == mgrid && a.Status == "P"
                                                           select new ViewModelClass.ViewRequestModel
                                                           {
                                                               RequestId = a.RequestId,
                                                               Batchname = b.BatchName,
                                                               RequetsedBy = d.Name,
                                                               RequetsedTo = e.Name,
                                                               CourseName = c.CourseName,
                                                               StartDate = b.StartDate,
                                                               EndDate = b.EndDate,
                                                               Status = a.Status,
                                                               CourseDuration = c.Duration,
                                                           }).ToList();
            return Rlist; }
        public ActionResult ApproveRequest(int? id)
        {  if (id != null)
            { ViewBag.rqObj = (from a in db.TMS_Request
                                 join b in db.TMS_BatchMaster on a.BatchId equals b.BatchId
                                 join c in db.TMS_CourseMaster on b.CourseId equals c.CourseId
                                 join d in db.TMS_UserMaster on a.UserId equals d.UserId
                                 join e in db.TMS_UserMaster on a.ManagerId equals e.UserId
                                 where a.IsActive == true && a.RequestId == id
                                 select new ViewModelClass.ViewRequestModel
                                 {
                                     RequestId = a.RequestId,
                                     Batchname = b.BatchName,
                                     RequetsedBy = d.Name,
                                     RequetsedTo = e.Name,
                                     CourseName = c.CourseName,
                                     StartDate = b.StartDate,
                                     EndDate = b.EndDate,
                                     Status = a.Status,
                                     CourseDuration = c.Duration,
                                 }).FirstOrDefault(); }
            return View(); }
        [HttpPost]
        public ActionResult ApproveRequest(int rid)
        {
            try {
                if (rid != 0)
                {
                    TMS_Request rObj = db.TMS_Request.Where(x => x.RequestId == rid).FirstOrDefault();
                    rObj.Status = "A";
                    db.SaveChanges();
                    TempData["Success"] = "Request Approved Successfully.";
                }
            }
            catch (Exception)
            {  TempData["Danger"] = "Invalid Request";  }        
            return View("BatchDetails");
        }
        [HttpPost]
        public ActionResult RejectId(int reqId, string comment)
        {
            try
            {
                if (reqId != 0)// && comment != "")
                {
                    TMS_Request Robj = db.TMS_Request.Where(x => x.RequestId == reqId).FirstOrDefault();
                    Robj.Status = "R";
                    if (comment != null)
                        Robj.Description = comment;
                    db.SaveChanges();
                   TempData["Warning"] = "Request has been Rejected";
                }
            }
            catch (Exception)
            {  TempData["Danger"] = "Invalid Request"; }            
            return View("BatchDetails"); }
        public ActionResult ViewAllBatches()
        {
            if (Session["UserDetails"] != null)
            {
                int mgrId = ((TMS_UserMaster)Session["UserDetails"]).UserId;
                List<ViewModelClass.ViewRequestModel> Rlist = (from a in db.TMS_Request
                                                               join b in db.TMS_BatchMaster on a.BatchId equals b.BatchId
                                                               join c in db.TMS_CourseMaster on b.CourseId equals c.CourseId
                                                               join d in db.TMS_UserMaster on a.UserId equals d.UserId
                                                               join e in db.TMS_UserMaster on a.ManagerId equals e.UserId
                                                               where a.ManagerId == mgrId
                                                               select new ViewModelClass.ViewRequestModel
                                                               {
                                                                   RequestId = a.RequestId,
                                                                   Batchname = b.BatchName,
                                                                   RequetsedBy = d.Name,
                                                                   RequetsedTo = e.Name,
                                                                   CourseName = c.CourseName,
                                                                   StartDate = b.StartDate,
                                                                   EndDate = b.EndDate,
                                                                   Status = a.Status,
                                                                   CourseDuration = c.Duration,
                                                               }).ToList();
                ViewBag.Rlist = Rlist; }
            return View(); }
    }
}