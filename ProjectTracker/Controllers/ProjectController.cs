using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        public IActionResult ProjectList()
        {
            int DeveloperID = Convert.ToInt32(HttpContext.Session.GetString("EmployeeID"));
            if (DeveloperID >= 1)
            {
      
                var myUser = _context.Project
                    .Include(ap => ap.Types)
                    .Include(ap => ap.Customers)
                    .Include(ap => ap.Products)
                    //.Include(ap => ap.Employees)
                    .Include(ap => ap.notes)
                    .ToList();
                var emp = _context.Employee.ToList();
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



            var Dev = _context.Employee.Where(e => e.PositionID == 1).ToList();
            var man = _context.Employee.Where(e => e.PositionID == 2).ToList();

            ViewBag.Employee= new SelectList(Dev, "EmployeeID", "Name");
            ViewBag.Manager = new SelectList(man, "EmployeeID", "Name");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Projects ec)
        {
            if (ModelState.IsValid)
            {
                var em = Request.Form["DeveloperID"];
                var ma = Request.Form["ProjectManagerID"];
                var rf = Request.Form["RequestFrom"];
                var rb = Request.Form["ReviewedBy"];
                var scb = Request.Form["SOWCreatedBy"];
                var srb = Request.Form["SOWReviewedBy"];

                int nots = _context.Notes.Max(u => u.NoteID);


                var product = Request.Form["ProductID"];
                var ty = Request.Form["TypeID"];
              

                ec.DeveloperID=Convert.ToInt32(em);
                ec.ProjectManagerID = Convert.ToInt32(ma);
                ec.RequestFrom =Convert.ToInt32(rf);
                ec.ReviewedBy = Convert.ToInt32(rb);
                ec.SOWReviewedBy = Convert.ToInt32(scb);
                ec.SOWReviewedBy = Convert.ToInt32(srb);

                ec.NoteID = Convert.ToInt32(nots);


                ec.ProductID = Convert.ToInt32(product);
                ec.Type = Convert.ToInt32(ty);
                if (ec.Type == 1)
                {
                    ec.SDFRNo = "SDR1";

                }
                else
                {
                    ec.SDFRNo = "SDR2";
                }

                _context.Add(ec);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjectList");
            }
            return View(ec);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProjectList");

            }

            var getprojectdetails = await _context.Project.FindAsync(id);
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

        public IActionResult Popup()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Popup(Note not)
        {
           

            _context.Add(not);
            _context.SaveChangesAsync();
            
            return View("Close");
          
        }
        //public IActionResult SaveNote(Note nt)
        //{
        //   // item.Requestfrom = emp.Where(e => e.EmployeeID == item.RequestFrom).Select(f => f.Name).FirstOrDefault();

        //    var nts = _context.Notes.Where(e=>e.ProjectID==0).ToList();
        //    int project = _context.Project.Max(u => u.ProjectID);

        //    //if(!_context.Notes.Any())
        //    foreach (var not in nts)
        //    {
        //        nt.ProjectID = project;
        //        //_context.Add(project);
        //        _context.Update(nt);
        //        _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("ProjectList");
        //}



            public IActionResult Attachment(Attachments ath)
        {
            foreach (var file in Request.Form.Files)
            {
                Attachments img = new Attachments();
                img.ImageTitle = file.FileName;

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.Files = ms.ToArray();

                ms.Close();
                ms.Dispose();

                _context.Attachment.Add(img);
                _context.SaveChanges();
            }           
            return RedirectToAction("ProjectList");


        }
    }
}
