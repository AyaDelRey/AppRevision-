using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Mappers;
using ASP_MVC.Models.Recette;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class RecetteController : Controller
    {
        private IRecetteRepository<Recette> _recetteRepository;
        private SessionManager _sessionManager;

        public RecetteController(
            IRecetteRepository<Recette> recetteRepository,
            SessionManager sessionManager
            )
        {
            _recetteRepository = recetteRepository;
            _sessionManager = sessionManager;
        }

        // GET: RecetteController
        public ActionResult Index()
        {
            try
            {
                IEnumerable<RecetteListItem> model = _recetteRepository.Get().Select(bll => bll.ToListItem());
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: RecetteController/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                RecetteDetails model = _recetteRepository.Get(id).ToDetails();
                _sessionManager.AddVisitedRecette(model.Recette_Id, model.Titre);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: RecetteController/Create
        [ConnectionNeeded]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecetteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ConnectionNeeded]
        public ActionResult Create(RecetteCreateForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
                Guid id = _recetteRepository.Insert(form.ToBLL());
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RecetteController/Edit/5
        //[ConnectionNeeded("Details", "Recette", true)]
        [IsCreator]
        public ActionResult Edit(Guid id)
        {
            try
            {
                /* Si nous devions vérifier si l'utilisateur connecté est le créateur, nous devrions passez par ces instructions
                 * Mais il est préférable de le définir dans un attribut IAuthorizationFilter
                Recette recette = _recetteRepository.Get(id);
                if (!(_sessionManager.ConnectedUser?.User_Id == recette.CreatedBy)) throw new InvalidOperationException("Vous n'êtes pas l'auteur de ce recette!");
                RecetteEditForm model = recette.ToEditForm();*/
                RecetteEditForm model = _recetteRepository.Get(id).ToEditForm();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RecetteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ConnectionNeeded]
        [IsCreator]
        public ActionResult Edit(Guid id, RecetteEditForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
                _recetteRepository.Update(id, form.ToBLL());
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RecetteController/Delete/5
        //[ConnectionNeeded]
        [IsCreator]
        public ActionResult Delete(Guid id)
        {
            try
            {
                RecetteDelete model = _recetteRepository.Get(id).ToDelete();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RecetteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ConnectionNeeded]
        [IsCreator]
        public ActionResult Delete(Guid id, RecetteDelete form)
        {
            try
            {
                _recetteRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
