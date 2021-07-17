using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;

namespace APIService.Provider
{
    public class AcceptTypeFilter : ActionFilterAttribute
    {
        private readonly string type;
        public AcceptTypeFilter(string type)
        {
            this.type = type;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            if (!actionContext.Request.Method.Method.Equals(HttpMethod.Get))
            {
                if (!actionContext.Request.Headers.Accept.Contains(MediaTypeWithQualityHeaderValue.Parse(type)))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid client!");
                }
            }
        }
    }
}