using Microsoft.AspNetCore.Mvc;

namespace EmptyProject.Controllers
{
    public class ProductController : Controller
    {
        public string MyAction()
        {
            return "Hello from Index Action in Product Controllers";
        }
    }
}
