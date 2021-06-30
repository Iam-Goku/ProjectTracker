using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Context _context;

        public CustomerController(Context context)
        {
            _context = context;
        }
        public IActionResult CustomerList()
        {
            var displaydata = _context.Customer.ToList();
            return View(displaydata);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customers c)
        {
            if (ModelState.IsValid)
            {
                _context.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction("CustomerList");
            }
            return View(c);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("CustomerList");

            }

            var getcustomerdetails = await _context.Customer.FindAsync(id);
            return View(getcustomerdetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Customers c)

        {
            if (ModelState.IsValid)
            {
                _context.Update(c);
                await _context.SaveChangesAsync();
                return RedirectToAction("CustomerList");
            }
            return View(c);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("CustomerList");

            }
            var getcustomerdetails = await _context.Customer.FindAsync(id);
            return View(getcustomerdetails);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var getcusdetails = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(getcusdetails);
            await _context.SaveChangesAsync();
            return RedirectToAction("CustomerList");
        }
    }
}
