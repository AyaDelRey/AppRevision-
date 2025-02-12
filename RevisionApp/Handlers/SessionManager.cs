using ASP_MVC.Models.Recette;
using System.Text.Json;
using System.Xml.Linq;

namespace ASP_MVC.Handlers
{
    public class SessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor accessor)
        {
            _session = accessor.HttpContext.Session;
        }

        public int CountVisitedPage
        {
            get { return _session.GetInt32(nameof(CountVisitedPage)) ?? 0; }
            set { _session.SetInt32(nameof(CountVisitedPage), value); }
        }

        public ConnectedUser? ConnectedUser
        {
            get { return JsonSerializer.Deserialize<ConnectedUser?>(_session.GetString(nameof(ConnectedUser)) ?? "null"); }
            set
            {
                if (value is null)
                {
                    _session.Remove(nameof(ConnectedUser));
                }
                else
                {
                    _session.SetString(nameof(ConnectedUser), JsonSerializer.Serialize(value));
                }
            }
        }

        public IEnumerable<RecetteListItemMin> RecentlyVisitedRecettes
        {
            get
            {
                string? json = _session.GetString(nameof(RecentlyVisitedRecettes));
                if (json is null) return new RecetteListItemMin[0];
                return JsonSerializer.Deserialize<RecetteListItemMin[]>(json);
            }
            private set
            {
                string json = JsonSerializer.Serialize(value);
                _session.SetString(nameof(RecentlyVisitedRecettes), json);
            }
        }

        public void Login(ConnectedUser user)
        {
            ConnectedUser = user;
        }

        public void Logout()
        {
            ConnectedUser = null;
        }

        public void AddVisitedRecette(RecetteListItemMin recette)
        {
            // Ligne permettant d'insérer la recette
            List<RecetteListItemMin> recettes = new List<RecetteListItemMin>(RecentlyVisitedRecettes);
            RecetteListItemMin? recetteInList = recettes.Where(r => r.Recette_Id == recette.Recette_Id).SingleOrDefault();
            if (recetteInList is not null)
            {
                recettes.Remove(recetteInList);
            }
            if (recettes.Count == 5)
            {
                recettes.Remove(recettes[4]);
            }
            recettes.Insert(0, recette);
            RecentlyVisitedRecettes = recettes;
        }

        public void AddVisitedRecette(Guid recette_id, string recette_titre)
        {
            RecetteListItemMin recette = new RecetteListItemMin()
            {
                Recette_Id = recette_id,
                Titre = recette_titre
            };
            AddVisitedRecette(recette);
        }
    }
}
