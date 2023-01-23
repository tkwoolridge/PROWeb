using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Data.Services.Candidates
{
    public interface ICandidatesServiceFactory : IDbContextServiceFactory<CandidatesService, DataContext>
    {
    }
}
