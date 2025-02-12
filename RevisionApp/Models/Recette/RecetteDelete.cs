using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Recette
{
    public class RecetteDelete
    {
        [DisplayName("Recette : ")]
        public string Titre { get; set; }
        [DisplayName("Description : ")]
        public string? Description { get; set; }
        [DisplayName("Créé par : ")]
        public Guid? CreatedBy { get; set; }
    }
}
