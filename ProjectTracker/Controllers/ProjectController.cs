using DevExpress.Utils.CommonDialogs.Internal;
using EO.WebBrowser.DOM;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectTracker.Controllers
{
    public class ProjectController : Controller
    {
        private readonly Context _context;
        public ProjectController(Context context)
        {
            _context = context;

        }
      
        public IActionResult ProjectList(string SearchString)
        {

            int DeveloperID = Convert.ToInt32(HttpContext.Session.GetString("EmployeeID"));
            if (DeveloperID >= 1)
            {
      
                var myUser = _context.Project
                    .Include(ap => ap.Types)
                    .Include(ap => ap.Customers)
                    .Include(ap => ap.Products).ToList();
                //.Include(ap => ap.Employees)
                //.Include(ap => ap.notes)

                var emp = _context.Employee.ToList();
                if (!String.IsNullOrEmpty(SearchString))
                {
                    myUser = myUser.Where(s => s.ProjectName.Contains(SearchString)).ToList();
                }

                foreach(var item in myUser)
                    {
                    item.Requestfrom = emp.Where(e => e.EmployeeID == item.RequestFrom).Select(f => f.Name).FirstOrDefault();
                    item.Reviewedby = emp.Where(e => e.EmployeeID == item.ReviewedBy).Select(f => f.Name).FirstOrDefault();
                    item.SOWCreatedby = emp.Where(e => e.EmployeeID == item.SOWCreatedBy).Select(f => f.Name).FirstOrDefault();
                    item.SOWReviewedby = emp.Where(e => e.EmployeeID == item.SOWCreatedBy).Select(f => f.Name).FirstOrDefault();
                    item.DeveloperName = emp.Where(e => e.EmployeeID == item.DeveloperID).Select(f=>f.Name).FirstOrDefault();
                    item.ProjectManagerName = emp.Where(e => e.EmployeeID == item.ProjectManagerID).Select(f => f.Name).FirstOrDefault();
                    }
                return View(myUser);
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
            //var displaydata = _context.project.ToList();
            //return View(displaydata);
        }

        public IActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(_context.Customer, "CustomerID", "CustomerName");
            ViewBag.ProductID = new SelectList(_context.Product, "ProductID", "Name");
            ViewBag.AllEmployee=new SelectList(_context.Employee, "EmployeeID", "Name");
            ViewBag.TypeID = new SelectList(_context.Types, "TypeID", "Type");

            ViewBag.ProjectStatus = new SelectList(_context.ProjectStatus, "ProjectStatusID", "Status");
            ViewBag.DevelopmentStatus = new SelectList(_context.DevelopmentStatus, "DevelopmentStatusID", "Status");




            var Dev = _context.Employee.Where(e => e.PositionID == 1).ToList();
            var man = _context.Employee.Where(e => e.PositionID == 2).ToList();

            ViewBag.Employee= new SelectList(Dev, "EmployeeID", "Name");
            ViewBag.Manager = new SelectList(man, "EmployeeID", "Name");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Projects ec)
        {
            var Note = new Note();

            if (ModelState.IsValid)
            {
                var em = Request.Form["DeveloperID"];
                var ma = Request.Form["ProjectManagerID"];
                var rf = Request.Form["RequestFrom"];
                var rb = Request.Form["ReviewedBy"];
                var scb = Request.Form["SOWCreatedBy"];
                var srb = Request.Form["SOWReviewedBy"];

                // TempData["notecret"] = _context.Notes.Max(u => u.NoteID);
                // var nottt = Request.Form[""];
                // TempData["notecret"] = Request.Form["Notecreate"];
                //var filess = Request.Form.Files;



                var product = Request.Form["ProductID"];
                var ty = Request.Form["TypeID"];


                ec.DeveloperID=Convert.ToInt32(em);
                ec.ProjectManagerID = Convert.ToInt32(ma);
                ec.RequestFrom =Convert.ToInt32(rf);
                ec.ReviewedBy = Convert.ToInt32(rb);
                ec.SOWReviewedBy = Convert.ToInt32(scb);
                ec.SOWReviewedBy = Convert.ToInt32(srb);

                //ec.NoteID = Convert.ToInt32(nots);


                ec.ProductID = Convert.ToInt32(product);
                ec.TypeID = Convert.ToInt32(ty);
                if (ec.TypeID == 1)
                {
                    var sdsd = _context.Project.FromSqlRaw("select Top 1 * from Project where TypeID=1 order by  ProjectID desc").ToList();
                    if (sdsd.Count != 0)
                    {
                        foreach (var item in sdsd)
                        {
                            item.SDFRNo = sdsd.Where(e => e.TypeID == 1).Select(f => f.SDFRNo).FirstOrDefault();
                            string str1 = item.SDFRNo;
                            // string str = Convert.ToString(sdsd);
                            var prefix = Regex.Match(str1, "^\\D+").Value;
                            var number = Regex.Replace(str1, "^\\D+", "");
                            var i = int.Parse(number) + 1;
                            var newString = prefix + i.ToString(new string('0', number.Length));
                            ec.SDFRNo = newString;
                        }
                    }
                    else
                    {
                        ec.SDFRNo = "DEV001";

                    }
                }
                else
                {

                    var Sup = _context.Project.FromSqlRaw("select Top 1 * from Project where TypeID=2 order by  ProjectID desc").ToList();

                    if (Sup.Count != 0)
                    {
                        foreach (var item in Sup)
                        {
                            item.SDFRNo = Sup.Where(e => e.TypeID == 2).Select(f => f.SDFRNo).FirstOrDefault();


                            string str = item.SDFRNo;
                            // string str = Convert.ToString(sdsd);
                            var prefix = Regex.Match(str, "^\\D+").Value;
                            var number = Regex.Replace(str, "^\\D+", "");
                            var i = int.Parse(number) + 1;
                            var newString = prefix + i.ToString(new string('0', number.Length));
                            ec.SDFRNo = newString;
                        }
                    }
                    else
                    {
                        ec.SDFRNo = "SUP001";
                    }
                }

                _context.Add(ec);
                await _context.SaveChangesAsync();

                Note.ProjectID = _context.Project.Max(e => e.ProjectID);
                Note.Notes= Request.Form["Notecreate"];
                Note.Date = DateTime.Now;
                _context.Notes.Add(Note);
                _context.SaveChanges();

                foreach (var file in Request.Form.Files)
                {
                    Attachments img = new Attachments();
                    img.ImageTitle = file.FileName;
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    img.Files = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                    img.ProjectID = _context.Project.Max(e => e.ProjectID);
                    _context.Attachment.Add(img);
                    _context.SaveChanges();
                }
                return RedirectToAction("ProjectList");
            }
            return View(ec);
        }




        public async Task<IActionResult> Edit(int? id)
        {

            var prdt = _context.Product.ToList();
            if (id == null)
            {
                return RedirectToAction("ProjectList");

            }
            var getprojectdetails = await _context.Project.FindAsync(id);
            //TempData["ProjectID"] = getprojectdetails.ProjectID;

            int prjid = getprojectdetails.ProjectID;
            HttpContext.Session.SetInt32("ProjectID", prjid);

            ViewBag.TypeID = new SelectList(_context.Types, "TypeID", "Type", getprojectdetails.TypeID);
            ViewBag.CustomerID = new SelectList(_context.Customer, "CustomerID", "CustomerName", getprojectdetails.CustomerID);
            ViewBag.ProductID = new SelectList(_context.Product, "ProductID", "Name", getprojectdetails.ProductID);
            ViewBag.RequestFrom = new SelectList(_context.Employee, "EmployeeID", "Name", getprojectdetails.RequestFrom);
            ViewBag.ReviewedBy= new SelectList(_context.Employee, "EmployeeID", "Name", getprojectdetails.ReviewedBy);
            ViewBag.SOWCreatedBy = new SelectList(_context.Employee, "EmployeeID", "Name", getprojectdetails.SOWCreatedBy);
            ViewBag.SOWReviewedBy= new SelectList(_context.Employee, "EmployeeID", "Name", getprojectdetails.SOWReviewedBy);
            ViewBag.DeveloperID = new SelectList(_context.Employee, "EmployeeID", "Name", getprojectdetails.DeveloperID);
            ViewBag.ProjectManagerID = new SelectList(_context.Employee, "EmployeeID", "Name", getprojectdetails.ProjectManagerID);

   
            return View(getprojectdetails);

        }
        [HttpPost]

        public async Task<IActionResult> Edit(Projects pc)
        {
            if (ModelState.IsValid)
            {
                _context.Update(pc);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjectList");

            }
            return View(pc);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProjectList");

            }

            var getprojectdetails = await _context.Project.FindAsync(id);
            return View(getprojectdetails);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProjectList");

            }

            var getprojectdetails = await _context.Project.FindAsync(id);
            return View(getprojectdetails);

        }

        [HttpPost]


        public async Task<IActionResult> Delete(int id)
        {
            var getprojectdetails = await _context.Project.FindAsync(id);
            _context.Project.Remove(getprojectdetails);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProjectList");
        }


        public IActionResult Notes()
        {
            //int projectid =Convert.ToInt32(TempData["ProjectID"]);
            int projectid = Convert.ToInt32(HttpContext.Session.GetInt32("ProjectID"));
            ViewBag.DATE = DateTime.Now.ToString("MM/dd/yyyy");

            var notlist = _context.Notes.Where(e => e.ProjectID == projectid).ToList();
            return View(notlist);
        }

   
        [HttpPost]
        public IActionResult NoteEdit(int? id,DateTime Date,string Notes)
        {
            var note = _context.Notes.Find(id);
            note.Date = Date;
            note.Notes = Notes;
            if (ModelState.IsValid)
            {
                _context.Update(note);
                _context.SaveChangesAsync();
                return RedirectToAction("Notes");

            }
            return View(note);

        }

        public async Task<IActionResult> NoteDelete(int id)
        {
            var getnotes = _context.Notes.Find(id);
            _context.Notes.Remove(getnotes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Notes");
        }



        [HttpPost]
        public IActionResult Popup(DateTime Date, string Notes)
        {
            Note not = new Note();
            not.Date = Date;
            not.Notes = Notes;
           // not.ProjectID=Convert.ToInt32(TempData["IDProject"]);
            not.ProjectID= Convert.ToInt32(HttpContext.Session.GetInt32("ProjectID"));

            _context.Add(not);
            _context.SaveChangesAsync();          
            return RedirectToAction("Notes");

        }


        public IActionResult Attachment()
        {

            int projectid = Convert.ToInt32(HttpContext.Session.GetInt32("ProjectID"));
            var attach = _context.Attachment.Where(e => e.ProjectID == projectid).ToList();
            ViewBag.AttachProjectID = projectid;
            return View(attach);
            //ViewBag.ProjectID = TempData["IDProject"];
            //return View();
        }

        [HttpPost]
            public IActionResult AttachmentAdd()
        {
            foreach (var file in Request.Form.Files)
            {
                Attachments img = new Attachments();
                img.ImageTitle = file.FileName;

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.Files = ms.ToArray();
                img.ProjectID = Convert.ToInt32(HttpContext.Session.GetInt32("ProjectID"));
                ms.Close();
                ms.Dispose();

                _context.Attachment.Add(img);
                _context.SaveChanges();
            }           
            return RedirectToAction("Attachment");


        }

        public FileResult DownloadFile(int id, string filename)
        {
            var myFile = _context.Attachment.Find(id);
            if (myFile != null)
            {
                byte[] fileBytes = myFile.Files;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, myFile.ImageTitle);
            }
            return null;
        }
 
        public FileResult ViewFile(int id, string filename)
        {
            var myFile = _context.Attachment.Find(id);

            //if (myFile != null)
            //{
            //    byte[] fileBytes = myFile.Files;
            //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, myFile.ImageTitle);
            //}
            return null;
        }
    }
}
