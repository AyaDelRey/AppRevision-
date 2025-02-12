using BLL.Entities;
using BLL.Mappers;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RecetteService : IRecetteRepository<Recette>
    {
        private IRecetteRepository<DAL.Entities.Recette> _recetteService;
        private IUserRepository<DAL.Entities.User> _userService;

        public RecetteService(
            IRecetteRepository<DAL.Entities.Recette> recetteService,
            IUserRepository<DAL.Entities.User> userService
            )
        {
            _recetteService = recetteService;
            _userService = userService;
        }

        public void Delete(Guid recette_id)
        {
            _recetteService.Delete(recette_id);
        }

        public IEnumerable<Recette> Get()
        {
            IEnumerable<Recette> recettes = _recetteService.Get().Select(dal => dal.ToBLL());
            foreach (Recette recette in recettes)
            {
                if (recette.CreatedBy is not null)
                {
                    recette.Creator = _userService.Get((Guid)recette.CreatedBy).ToBLL();
                }
            }
            return recettes;
        }

        public Recette Get(Guid recette_id)
        {
            Recette recette = _recetteService.Get(recette_id).ToBLL();
            if (recette.CreatedBy is not null)
            {
                recette.Creator = _userService.Get((Guid)recette.CreatedBy).ToBLL();
            }
            return recette;
        }

        public IEnumerable<Recette> GetFromUser(Guid user_id)
        {
            return _recetteService.GetFromUser(user_id).Select(dal => dal.ToBLL());
        }

        public Guid Insert(Recette recette)
        {
            return _recetteService.Insert(recette.ToDAL());
        }

        public void Update(Guid recette_id, Recette recette)
        {
            _recetteService.Update(recette_id, recette.ToDAL());
        }
    }
}
