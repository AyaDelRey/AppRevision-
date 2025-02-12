using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Recette
    {
        public Guid Recette_Id { get; set; }
        public string Titre { get; set; }
        public string? Description { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
