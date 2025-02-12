using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public interface IRecetteRepository<TRecette> : ICRUDRepository<TRecette, Guid>
    {
        IEnumerable<TRecette> GetFromUser(Guid user_id);
    }
}
