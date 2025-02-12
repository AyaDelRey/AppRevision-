using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ASP_MVC.Handlers.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class IsCreatorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? json = context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser));
            if (json is null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            ConnectedUser connectedUser = JsonSerializer.Deserialize<ConnectedUser>(json);
            Guid recette_id = Guid.Parse(context.RouteData.Values["id"].ToString());
            IRecetteRepository<Recette> recetteRepository = GetRecetteService(context.HttpContext);
            Recette recette = recetteRepository.Get(recette_id);
            if (recette.CreatedBy != connectedUser.User_Id)
            {
                context.Result = new RedirectToActionResult("Details", "Recette", new { id = recette_id });
            }
        }

        private IRecetteRepository<Recette> GetRecetteService(HttpContext httpContext)
        {
            IServiceProvider serviceProvider = httpContext.RequestServices;
            return serviceProvider.GetService<IRecetteRepository<Recette>>();
        }
    }
}
