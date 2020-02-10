using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StartUp.Data;
using StartUp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StartUp.Controllers
{
    public class FacultyController : Controller
    {
        string hello;
        AppDbContext _context;
        IWebHostEnvironment _host;
        public FacultyController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index(int id)
        {
            ViewBag.Hello = hello;
            // select * from faculty where FacultyId = 1
            Faculty data = _context.Faculty.Find(id); // EF CORE 
            return View(data);
        }

        public IActionResult Detail()
        {
            ViewBag.Hello = hello;
            return View();
        }

        public IActionResult List()
        {      
            // select * from Faculty
            List<Faculty> facultyList  = _context.Faculty.ToList();
            return View(facultyList);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Faculty data = _context.Faculty.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Faculty data)
        {
            if (ModelState.IsValid)  // required or other validation success or passs --- true
            {
                _context.Update(data);
                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View("Edit", data);
        }

        [HttpPost]
        public IActionResult Create(Faculty data)
        {

            string rootPath = _host.WebRootPath;

            if (ModelState.IsValid)  // required or other validation success or passs --- true
            {
                _context.Add(data);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View("Add",data);        
        }

        public IActionResult Delete(int id)
        {
            var data = _context.Faculty.Find(id);
            _context.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
