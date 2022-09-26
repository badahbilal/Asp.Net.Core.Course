using Microsoft.AspNetCore.Mvc;

namespace EmptyProject.Controllers
{
    public class HomeController : Controller
    {

        public string Details()
        {
            return "hello from details actions";
        }

        public string About()
        {
            return "hello from About actions";
        }   


        public string OldIndex()
        {
            return "hello from Index actions"; 
        }
    }
}
