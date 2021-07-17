using APIService.Provider;
using APIService.Provider.CustomAPIController;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    public class CrmController : CustomAPIController
    {
        [HttpPost]
        [Route("api/crm/fetch")]
        public HttpResponseMessage Fetch(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Fetch(model.Data, string.Empty, clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }

        [HttpPost]
        [Route("api/crm/associate")]
        public HttpResponseMessage Associate(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Associate(model.Data, clams["UserId"], clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }
        [HttpPost]
        [Route("api/crm/disassociate")]
        public HttpResponseMessage Disassociate(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Disassociate(model.Data, clams["UserId"], clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }

        [HttpPost]
        [Route("api/crm/create")]
        public HttpResponseMessage Create(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Create(model.Data, clams["UserId"], clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }
        [HttpPost]
        [Route("api/crm/update")]
        public HttpResponseMessage Update(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {

                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Update(model.Data, clams["UserId"], clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }
        [HttpPost]
        [Route("api/crm/delete")]
        public HttpResponseMessage Delete(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Delete(model.Data, string.Empty, clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }
        [HttpPost]
        [Route("api/crm/changestate")]
        public HttpResponseMessage ChangeState(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.ChangeState(model.Data, clams["UserId"], clams["UserPortalName"]), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }
        [HttpPost]
        [Route("api/crm/changepassword")]
        public HttpResponseMessage ChangePassword(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                if (!ValidateClams(clams, ref res))
                    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                var data = Extension.DeSerializeObject<ChangePassword>(model.Data);
                res.Content = new StringContent(service.ChangePassword(data.US, data.PW, data.RPW));
                return res;
            });
        }

        [Route("api/crm/booking")]
        public HttpResponseMessage Booking(StringModel model)
        {
            if (model == null)
            {
                var res = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return res;
            }
            return ExecuteRequest(Request, (request) =>
            {
                IOwinContext owincontext = request.GetOwinContext();
                Dictionary<string, string> clams = owincontext.Authentication.User.Claims.ToDictionary(c => c.Type, c => c.Value);
                var res = request.CreateResponse(HttpStatusCode.OK);
                //if (!ValidateClams(clams, ref res))
                //    return res;
                IDataService service = DataServices.CreateDataService(Extension.RandomToken(Token));
                res.Content = new StringContent(service.Booking(model.Data), Encoding.UTF8, MediaType.Json);
                return res;
            });
        }

        [HttpPost]
        [Route("api/crm/gettokencrm")]
        public string GetTokenCRM()
        {
            return Extension.RandomToken(Token).AccessToken.ToString();
        }

        private bool ValidateClams(Dictionary<string, string> clams, ref HttpResponseMessage res)
        {
            if (!clams.ContainsKey("UserPortalName"))
            {
                res.StatusCode = HttpStatusCode.Unauthorized;
                res.Content = new StringContent(string.Format("{{\"error\": {{\"code\": \"{0}\",\"message\": {{\"value\": \"{1}\"}}}}}}", (int)HttpStatusCode.InternalServerError, "Login failed. Please try again later!"), Encoding.UTF8, MediaType.Json);
                return false;
            }
            else
                return true;
        }
    }
}