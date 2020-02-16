using System;
using System.Collections.Generic;
using System.IO;
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
        IWebHostEnvironment _env;
        public FacultyController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            if (ModelState.IsValid)  // required or other validation success or passs --- true
            {
                if (data.FacultyImage != null)
                {
                    string rootPath = _env.WebRootPath;              // C:user/timroname/straup/wwwroot/
                    string uniqueName = Guid.NewGuid().ToString(); //efdfjde432423423454
                    string imageName = uniqueName + data.FacultyImage.FileName;  // bererewfesomething.jpg
                    string uploadPath = rootPath + "/Uploads/" + imageName;  // C:users/name/str/wwroo/uploads/imagename.jpg
                    data.ImageName = imageName;                      // to save image name in database 

                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        data.FacultyImage.CopyTo(fileStream);
                    }
                }
                _context.Add(data);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View("Add",data);        
        }

    //    string rootPath = env.WebRootPath;                    // get the root directory i.e. /wwwroot/
    //    string uniqueName = Guid.NewGuid().ToString();

    //    string fileName = uniqueName + bike.BikeImage.FileName;      // file uploaded name
    //    string uploadPath = rootPath + "/Images/" + fileName;       // creating upload path
    //    bike.ImageName = fileName;                                 // assing file name to bike>imagename                                                                                    
    //                using (var filestream = new FileStream(uploadPath, FileMode.Create))
    //                {
    //                    await bike.BikeImage.CopyToAsync(filestream);
    //}

    public IActionResult Delete(int id)
        {
            var data = _context.Faculty.Find(id);
            _context.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
