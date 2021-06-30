using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                //var myUser = _context.Projects.Where(e => e.DeveloperID == DeveloperID);
                // var myUser = _context.project.Include(cu => cu.customers).ToList();

                var myUser = _context.Project.Include(ap => ap.Types).Include(ap => ap.Customers)
               .Include(ap => ap.Products).Include(ap => ap.Employees).Include(ap => ap.Employees)
               .Include(ap => ap.notes).ToList();

                //var myUser = _context.Projects
                //             .Include(c => c.Customer)
                //             .Include(c => c.Products)
                //             .Include(c => c.Employees)
                //             //.Include(c=>c.Manager)
                //             .Include(c => c.Types)
                //             .ToList();




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

        public IActionResult Attachment(Attachments ath)
        {
            _context.Add(ath);
            _context.SaveChanges();
            return View();
        }
    }
}
