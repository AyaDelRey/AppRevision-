using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Recette
{
    public class RecetteListItem
    {
        [ScaffoldColumn(false)]
        public Guid Recette_Id { get; set; }
        [DisplayName("Recette : ")]
        public string Titre { get; set; }
        [DisplayName("Description : ")]
        public string? Description { get; set; }
    }
}
