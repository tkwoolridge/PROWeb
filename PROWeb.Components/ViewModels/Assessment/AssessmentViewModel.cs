using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Assessment
{
    public class AssessmentViewModel : SlimViewModelBase, IEquatable<AssessmentViewModel>
    {
        public int AssessmentNo { get; set; }

        public string? Address1 { get; set; }

        public string? HouseNo { get; set; }

        public string? Address2 { get; set; }

        public string? PostalCode { get; set; }

        public int ConstituencyNo { get; set; }

        public string? ConstituencyName { get; set; }

        public string? Constituency => $"{ConstituencyNo} - {ConstituencyName}";

        public int ParishNo { get; set; }

        public string? ParishName { get; set; }

        public bool IsBogus { get; set; }

        public bool Equals(AssessmentViewModel? other)
        {
            return AssessmentNo == other?.AssessmentNo;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AssessmentViewModel);
        }

        public override int GetHashCode()
        {
            return AssessmentNo.GetHashCode();
        }
    }
}
