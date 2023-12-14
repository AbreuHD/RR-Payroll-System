using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApp.Middleware
{
    public class ValidateSession
    {
        private readonly IHttpContextAccessor _httpContext;
        public ValidateSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool HasUser()
        {
            UserViewModel user = _httpContext.HttpContext.Session.Id != null ? JsonConvert.DeserializeObject<UserViewModel>(_httpContext.HttpContext.Session.GetString("user")) : null;
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public UserViewModel GetUser()
        {
            UserViewModel user = _httpContext.HttpContext.Session.Id != null ? JsonConvert.DeserializeObject<UserViewModel>(_httpContext.HttpContext.Session.GetString("user")) : null;
            return user;
        }
    }
}
