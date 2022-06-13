using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_PRAC.Controllers
{
    public class AllowCrossSiteAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            base.OnResultExecuting(context);
        }
    }
}