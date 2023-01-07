using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonFlagViewModel
    {
        public int FlagId { get; set; }

        public string? FlagDescription { get; set; }
    }
}
