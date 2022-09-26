using EmptyProject.Models;
using EmptyProject.Models.Repositories;
using EmptyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyProject.Controllers
{
    //[Route("[Controller]")]
    public class EmployeeController : Controller
    {
        private ICompanyRepository<Employee> _companyRepository;
        private IAnimleReposiroty<Cat> _animleReposiroty;
        public EmployeeController(ICompanyRepository<Employee> companyRepository, IAnimleReposiroty<Cat> animleReposiroty)
        {
            _companyRepository = companyRepository;
            _animleReposiroty = animleReposiroty;
        }
        // video 33 Attribute Routing
        //[Route("TestAttribute")]
        ////[Route("~/")]
        //[Route("")]
        //[Route("employee/TestAttribute")]
        //[Route("employee/TestAttribute/{id?}")]
        public string RouteAttribute(string id = "")
        {
            return "Test Route Attribute " + id; 
        }

        //[Route("[action]")]
        public ViewResult findme()
        {
            return View();
        }

        // videos 18
        #region
        public JsonResult JsonResult(int id)
        {
            return Json(_companyRepository.get(id));
        }

        public ObjectResult ObjectResult(int id)
        {
            return new ObjectResult(_companyRepository.get(id));
        }

        public string OldIndex(int id)
        {
            return _companyRepository.get(id).Name;
        }
        #endregion
        public ViewResult Index()
        {
            ViewData["name"] = "BADAH Bilal";
            ViewData["age"] = 19;

            ViewBag.user = "admin";
            return View();
        }

        public ViewResult Details()
        {
           
            ViewBag.index = "details action controller";
            ViewBag.user = "simple";
            return View();
        }

        public ViewResult PassingViewData()
        {
            ViewData["MyName"] = "BADAH Bilal";
            ViewData["Age"] = 25;

            ViewBag.age = 15;

            ViewBag.employee = _companyRepository.get(1);
            return View();
        }

        public ViewResult sendModel()
        {

            Employee employee = _companyRepository.get(2);
            return View(employee);
        }

        public ViewResult testViewModel()
        {
            TestViewModelEmployeeViewModels modelView = new TestViewModelEmployeeViewModels()
            {
                cat = _animleReposiroty.GetById(1),
                employee = _companyRepository.get(1)
            };

            
            return View(modelView);
        }

        public ViewResult ListView()
        {
            IEnumerable<Employee> employees = _companyRepository.GetEntities();
            return View(employees);
        }

        /*

        //videos 21 - 22
        #region
        public ViewResult RelativePath()
        {
            return View("../RelativePath/TestV");
        }
        public ViewResult SpecificView()
        {
            return View("MYspesificView");
        }

        public ViewResult AbsolutePath()
        {
            return View("AbsolutePath/TestV.cshtml");
        }
        #endregion


        //videos 23
        #region
        public ViewResult PassingViewData()
        {
            ViewData["MyName"] = "BADAH Bilal";
            ViewData["Age"] = 25;
            return View();
        }
        #endregion

        // Video 24
        #region
        public ViewResult PassingViewBag()
        {
            ViewBag.emp = _companyRepository.get(1);
            return View();
        }
        #endregion
        

        // Video 25
        #region
        public ViewResult PassingModel()
        {
            return View(_companyRepository.get(2));
        }
        #endregion


        // video 26. ViewModel in ASP.NET Core MVC in Darija Arabic

        public ViewResult Details26()
        { 
            //ViewBag.Title = "details Pages | welcome to my details page";
            //return View(_companyRepository.get(2));

            EmployeeDetails26ViewModel viewModel = new EmployeeDetails26ViewModel()
            {
                Employee = _companyRepository.get(2),
                Title = "Details Pages | welcome to my details page"


            };
            return View(viewModel);

        }
        */
    }
}
