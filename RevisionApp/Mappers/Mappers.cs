using ASP_MVC.Models.Recette;
using ASP_MVC.Models.User;
using BLL.Entities;

namespace ASP_MVC.Mappers
{
    internal static class Mapper
    {
        #region Users
        public static UserListItem ToListItem(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserListItem()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name
            };
        }

        public static UserDetails ToDetails(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserDetails()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                CreatedAt = DateOnly.FromDateTime(user.CreatedAt),
                Recettes = user.Recettes.Select(bll => bll.ToListItem())
            };
        }

        public static User ToBLL(this UserCreateForm user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new User(
            Guid.Empty,
            user.First_Name,
            user.Last_Name,
            user.Email,
            user.Password,
            DateTime.Now,
            null,
            "User"
             );
            /*return new User(
                user.First_Name,
                user.Last_Name,
                user.Email,
                user.Password);*/
        }

        public static UserEditForm ToEditForm(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserEditForm()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email
            };
        }

        public static User ToBLL(this UserEditForm user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            /*return new User(
                Guid.Empty,
                user.First_Name,
                user.Last_Name,
                user.Email,
                "********",
                DateTime.Now,
                null,
                "User"
                );*/
            return new User(
                user.First_Name,
                user.Last_Name,
                user.Email);
        }

        public static UserDelete ToDelete(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserDelete()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email
            };
        }
        #endregion
        #region Recettes
        public static RecetteListItem ToListItem(this Recette recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new RecetteListItem()
            {
                Recette_Id = recette.Recette_Id,
                Titre = recette.Titre,
                Description = recette.Description
            };
        }

        public static RecetteDetails ToDetails(this Recette recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new RecetteDetails()
            {
                Recette_Id = recette.Recette_Id,
                Titre = recette.Titre,
                Description = recette.Description,
                Instructions = recette.Instructions,
                CreatedAt = recette.CreatedAt,
                Creator = (recette.Creator is null) ? null : $"{recette.Creator.First_Name} {recette.Creator.Last_Name}",
                CreatedBy = recette.CreatedBy
            };
        }

        public static Recette ToBLL(this RecetteCreateForm recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new Recette(
                Guid.Empty,
                recette.Titre,
                recette.Description,
                recette.Instructions,
                DateOnly.FromDateTime(DateTime.Now),
                recette.CreatedBy
                );
        }

        public static RecetteEditForm ToEditForm(this Recette recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new RecetteEditForm()
            {
                Titre = recette.Titre,
                Description = recette.Description,
                Instructions = recette.Instructions
            };
        }

        public static Recette ToBLL(this RecetteEditForm recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new Recette(
                Guid.Empty,
                recette.Titre,
                recette.Description,
                recette.Instructions,
                DateOnly.FromDateTime(DateTime.Now),
                Guid.Empty
                );
        }

        public static RecetteDelete ToDelete(this Recette recette)
        {
            if (recette is null) throw new ArgumentNullException(nameof(recette));
            return new RecetteDelete()
            {
                Titre = recette.Titre,
                Description = recette.Description,
                CreatedBy = recette.CreatedBy
            };
        }
        #endregion
    }
}
