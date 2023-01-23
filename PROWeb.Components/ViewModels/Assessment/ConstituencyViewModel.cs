using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Assessment
{
    public class ConstituencyViewModel : SlimViewModelBase
    {
        public int ConstituencyNo { get; set; }

        public string? BogusNo { get; set; }

        public string? ConstituencyName { get; set; }

        public string Constituency
        {
            get
            {
                var space = string.Concat(Enumerable.Repeat("\u0020", 3 - ConstituencyNo.ToString().Length));

                return $"{ConstituencyNo}.{space}{ConstituencyName}";
            }
        }
    }
}
