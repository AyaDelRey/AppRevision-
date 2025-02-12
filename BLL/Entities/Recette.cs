using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Recette
    {

        public Guid Recette_Id { get; set; }
        public string Titre { get; set; }
        public string? Description { get; set; }
        public string Instructions { get; set; }
        public DateOnly CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public User? Creator { get; set; }


        public Recette(Guid recette_Id, string titre, string? description, string instructions, DateOnly createdAt, Guid? createdBy)
        {
            Recette_Id = recette_Id;
            Titre = titre;
            Description = description;
            Instructions = instructions;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

    }
}
