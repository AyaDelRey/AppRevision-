using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Recette
{
    public class RecetteListItemMin
    {
        [ScaffoldColumn(false)]
        public Guid Recette_Id { get; set; }
        [DisplayName("Recette : ")]
        public string Titre { get; set; }
    }
}
