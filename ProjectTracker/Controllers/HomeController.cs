using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }


        public async Task<IActionResult> Home()
        {
            return View();
        }
        
  
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Employees emp)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var myUser = _context.Employee
                                   .Single(u => u.Name == emp.Name
                                   && u.Password == emp.Password);
                    if (myUser != null)
                    {
                        string employeeid =Convert.ToString(myUser.EmployeeID);
                        //TempData["EmployeeID"] = employeeid;

                        HttpContext.Session.SetString("EmployeeID", employeeid);
                       // return RedirectToAction("EmployeeDetails");
                        return RedirectToAction("ProjectList", "Project");
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            return View();
        }

        public IActionResult SignUp()
        {
         ViewBag.PositionID = new SelectList(_context.Position, "PositionID", "Position");
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(Employees ec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ec);
                await _context.SaveChangesAsync();
                return RedirectToAction("AllEmployeeDetails");
            }
            return View(ec);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.PositionID = new SelectList(_context.Position, "PositionID", "Position");

            if (id == null)
            {
                return RedirectToAction("AllEmployeeDetails");

            }

            var getempdetails = await _context.Employee.FindAsync(id);
            return View(getempdetails);

        }
        [HttpPost]

        public async Task<IActionResult> Edit(Employees ec)
        {
            if (ModelState.IsValid)
            {
                _context.Update(ec);
                await _context.SaveChangesAsync();
                return RedirectToAction("AllEmployeeDetails");

            }
            return View(ec);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllEmployeeDetails");

            }

            var getempdetails = await _context.Employee.FindAsync(id);
            return View(getempdetails);

        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllEmployeeDetails");

            }

            var getempdetails = await _context.Employee.FindAsync(id);
            return View(getempdetails);

        }

        [HttpPost]


        public async Task<IActionResult> Delete(int id)
        {


            var getempdetails = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(getempdetails);
            await _context.SaveChangesAsync();

            return RedirectToAction("AllEmployeeDetails");

        }

  
        //    public async Task<IActionResult> EmployeeDetails(Employees emp)
        //{
        //    // int EmployeeID = Convert.ToInt32(TempData["EmployeeID"]);

        //    int EmployeeID =Convert.ToInt32(HttpContext.Session.GetString("EmployeeID"));

        //    if (EmployeeID >= 1)
        //    {            
        //        var myUser = _context.Employee
        //                     .Where(e => e.EmployeeID == EmployeeID).ToList();
                           
        //        return View(myUser);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        public IActionResult AllEmployeeDetails()
        {
            var displaydata = _context.Employee.Include(po=>po.postion).ToList();
            return View(displaydata);
        }
    }
}
