using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Controllers
{
    public class ProductController : Controller
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        public IActionResult ProductList()
        {
            var displaydata = _context.Product.ToList();
            return View(displaydata);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products p)
        {
            if (ModelState.IsValid)
            {
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductList");
            }
            return View(p);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProductList");

            }

            var getproductdetails = await _context.Product.FindAsync(id);
            return View(getproductdetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Products p)

        {
            if (ModelState.IsValid)
            {
                _context.Update(p);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductList");
            }
            return View(p);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProductList");

            }
            var getproductdetails = await _context.Product.FindAsync(id);
            return View(getproductdetails);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var getproductdetails = await _context.Product.FindAsync(id);
            _context.Product.Remove(getproductdetails);
            await _context.SaveChangesAsync();
            return RedirectToAction("ProductList");
        }
    }
}
