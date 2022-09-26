using EmptyProject.Models;
using EmptyProject.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProject.Controllers
{
    public class CatController : Controller
    {

        private IAnimleReposiroty<Cat> _catRepository;
        private ICompanyRepository<Employee> _employeeRepository;

        //public CatController(IAnimleReposiroty<Cat> catRepository)
        //{
        //    _catRepository = catRepository;
        //}

        public CatController(IAnimleReposiroty<Cat> catRepository,ICompanyRepository<Employee> employeeRepository)
        {
            _catRepository = catRepository;
            _employeeRepository = employeeRepository;
            
        }


        public string getNameCat(int id)
        {
            return _catRepository.GetById(id).name;
        }

        public string getEmployee()
        {
            return _employeeRepository.get(2).Name;
        }


        public JsonResult getCatAsJson()
        {
            Cat cat = new Cat() { id = 1, name = "Halalou", age = 3 };
            return Json(cat);
        }


        public ObjectResult GetCatAsXmlAndJson()
        {
            //cette methode donne à l'utilisateur la posibilité de recevoir les données soit en json soit en xml
            Cat cat = new Cat() { id = 1, name = "Halalou", age = 3 };
            return new ObjectResult(cat);
        }

        public ViewResult Index()
        {
            
            return View();
        }
    }
}
