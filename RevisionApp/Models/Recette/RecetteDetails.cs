using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Recette
{
    public class RecetteDetails
    {
        [ScaffoldColumn(false)]
        public Guid Recette_Id { get; set; }
        [DisplayName("Recette : ")]
        public string Titre { get; set; }
        [DisplayName("Description : ")]
        public string? Description { get; set; }
        [DisplayName("Instructions : ")]
        [DataType(DataType.MultilineText)]
        public string Instructions { get; set; }
        [DisplayName("Créé par : ")]
        public string? Creator { get; set; }
        [ScaffoldColumn(false)]
        public Guid? CreatedBy { get; set; }
        [DisplayName("Créé le : ")]
        [DataType(DataType.Date)]
        public DateOnly CreatedAt { get; set; }
    }
}