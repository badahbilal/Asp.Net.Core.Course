using EmptyProject.Models;
using EmptyProject.Models.Repositories;
using EmptyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmptyProject.Controllers
{
    //cet attribut fait blocakge d'accées si vous etes pas authentifie 
    [Authorize]
    public class Employee2Controller : Controller
    {

        private ICompanyRepository<Employee> _companyRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Employee2Controller(ICompanyRepository<Employee> companyRepository, IWebHostEnvironment webHostEnvironment)
        {
            _companyRepository = companyRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        // donner l'accées à tous les utilisateurs
        [AllowAnonymous]
        public ViewResult Index()
        {
            IEnumerable<Employee> employees = _companyRepository.GetEntities();
            return View(employees);
        }

        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //throw new Exception("Details error");
            Employee employee = _companyRepository.get(id ?? 1);

            if (employee is null)
            {
                return View("NotFoundPage", id);
            }
            return View(employee);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {


                string uniqueFile = null;
                string imagePathServer = "/images/";
                if (model.Image != null)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
                    var fileExt = Path.GetExtension(model.Image.FileName).ToLower();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("", "Invalid Extension file");
                        return View(model);

                    }
                    string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFile = Guid.NewGuid() + "_" + model.Image.FileName;
                    imagePathServer += uniqueFile;
                    string path = Path.Combine(uploadFolderPath, uniqueFile);
                    //model.Image.CopyTo(new FileStream(path, FileMode.Create));

                    using var fileStream = new FileStream(path, FileMode.Create);
                    model.Image.CopyTo(fileStream);

                }

                Employee employee = new Employee()
                {
                    Department = model.Department,
                    Email = model.Email,
                    Name = model.Name,
                    ImagePath = imagePathServer,

                };
                _companyRepository.Add(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }
            ViewBag.Error = "Please Fill All Information";

            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Employee employee = _companyRepository.get(id);

            if (employee is null)
            {
                return View("NotFoundPage", id);
            }

            EmployeeEditViewModel model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OldImagePath = employee.ImagePath,
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Update(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _companyRepository.get(model.Id);
                employee.Name = model.Name;
                employee.Department = model.Department;
                employee.Email = model.Email;

                string uniqueFile = null;
                string imagePathServer = "/images/";
                if (model.Image != null)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
                    var fileExt = Path.GetExtension(model.Image.FileName).ToLower();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("", "Invalid Extension file");
                        return View(model);

                    }
                    string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFile = Guid.NewGuid() + "_" + model.Image.FileName;
                    imagePathServer += uniqueFile;
                    string path = Path.Combine(uploadFolderPath, uniqueFile);
                    //model.Image.CopyTo(new FileStream(path, FileMode.Create));
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }

                    var parm1 = model.OldImagePath.Split("/")[1].ToString();
                    var parm2 = model.OldImagePath.Split("/")[2].ToString();
                    string oldImageEmployee = Path.Combine(_webHostEnvironment.WebRootPath, parm1, parm2);

                    System.IO.File.Delete(oldImageEmployee);





                    employee.ImagePath = imagePathServer;


                }


                _companyRepository.Update(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }
            ViewBag.Error = "Please Fill All Information";

            return View(model);
        }
    }
}
