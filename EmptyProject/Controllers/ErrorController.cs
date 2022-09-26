using EmptyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProject.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult Index(int StatusCode)
        {
            StatusResult model = new StatusResult();
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (StatusCode)
            {
                case 404:

                    model.Message = "Sorry page not found";
                    model.Path = statusCodeResult.OriginalPath;
                    model.QS = statusCodeResult.OriginalQueryString;
                    break;
            }
            return View("NotFound", model);
        }

        [Route("Error")]
        public ActionResult Error()
        {
            var execptionStatus = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Message = execptionStatus.Error.Message;
            ViewBag.StackTrace = execptionStatus.Error.StackTrace;
            ViewBag.Path = execptionStatus.Path;
            return View();
        }
    }
}
