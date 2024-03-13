using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BoxBox.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute
        , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            //PREGUNTAMOS SI EL USER YA ESTA AUTENTIFICADO
            if (user.Identity.IsAuthenticated == false)
            {
                //CREAMOS LA RUTA A NUESTRA DIRECCION
                RouteValueDictionary rutaLogin =
                    new RouteValueDictionary
                    (
                        new { controller = "Auth", action = "Login" }
                    );
                //LLEVAMOS AL USUARIO A Login
                context.Result =
                    new RedirectToRouteResult(rutaLogin);
            }
        }
    }
}
