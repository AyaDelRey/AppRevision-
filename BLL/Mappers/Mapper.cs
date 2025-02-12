using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using D = DAL.Entities;

namespace BLL.Mappers
{
    internal static class Mapper
    {
        #region Users
        public static User ToBLL(this D.User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new User(
                user.User_Id,
                user.First_Name,
                user.Last_Name,
                user.Email,
                user.Password,
                user.CreatedAt,
                user.DisabledAt,
                user.Role);
        }

        public static D.User ToDAL(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new D.User()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                DisabledAt = (user.IsDisabled) ? new DateTime() : null,
                Role = user.Role.ToString()
            };
        }

        #endregion

        #region Recettes
        public static Recette ToBLL(this D.Recette recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new Recette(
                recette.Recette_Id,
                recette.Titre,
                recette.Description,
                recette.Instructions,
                DateOnly.FromDateTime(recette.CreatedAt),
                recette.CreatedBy
                );
        }

        public static D.Recette ToDAL(this Recette recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new D.Recette()
            {
                Recette_Id = recette.Recette_Id,
                Titre = recette.Titre,
                Description = recette.Description,
                Instructions = recette.Instructions,
                CreatedAt = recette.CreatedAt.ToDateTime(new TimeOnly()),
                CreatedBy = recette.CreatedBy
            };
        }
        #endregion
    }
}
