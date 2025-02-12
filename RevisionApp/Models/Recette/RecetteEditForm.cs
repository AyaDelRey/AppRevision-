using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Recette
{
    public class RecetteEditForm
    {
        [DisplayName("Nom de la recette : ")]
        [Required(ErrorMessage = "Le champ 'Titre de la recette' est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le champ 'Titre de la recette' doit contenir au minimum 2 caractères.")]
        [MaxLength(64, ErrorMessage = "Le champ 'Titre de la recette' doit contenir au maximum 64 caractères.")]
        public string Titre { get; set; }
        [DisplayName("Description : ")]
        [MinLength(2, ErrorMessage = "Le champ 'Description' doit contenir au minimum 2 caractères.")]
        [MaxLength(512, ErrorMessage = "Le champ 'Description' doit contenir au maximum 512 caractères.")]
        public string? Description { get; set; }
        [DisplayName("Instructions : ")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le champ 'Instructions' est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le champ 'Instructions' doit contenir au minimum 2 caractères.")]
        public string Instructions { get; set; }
    }
}
