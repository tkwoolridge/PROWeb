using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROWeb.Components.ViewModels.Voter;

namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonDocumentsViewModel<TDocumentViewModel>
         where TDocumentViewModel : class, IDocumentViewModel
    {
        List<TDocumentViewModel> Documents { get; set; }
    }
}
