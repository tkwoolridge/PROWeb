using Humanizer;
using PROWeb.Common.ViewModels;
using PROWeb.Data.Models;

namespace PROWeb.Components.Voters.ViewModels
{
    public class VoterHistoryFieldViewModel : SlimViewModelBase, IEquatable<VoterHistoryFieldViewModel>
    {
        private int _id;
        private int _voterId;
        private int _registryYear;
        private DateTime? _created;
        private string _field = default!;
        private string? _oldValue;
        private string? _newValue;
        private List<VoterHistory>? _voterHistories;

        public int Id
        {
            get => _id;
            set => RaiseAndSetIfChanged(ref _id, value);
        }

        public int VoterId
        {
            get => _voterId;
            set => RaiseAndSetIfChanged(ref _voterId, value);
        }

        public int RegistryYear
        {
            get => _registryYear;
            set => RaiseAndSetIfChanged(ref _registryYear, value);
        }

        public DateTime? Created
        {
            get => _created;
            set => RaiseAndSetIfChanged(ref _created, value);
        }

        public string Field
        {
            get => _field;
            set => RaiseAndSetIfChanged(ref _field, value);
        }

        public string FieldDescription => Field.Humanize().Transform(To.TitleCase);

        public string? OldValue
        {
            get => _oldValue;
            set => RaiseAndSetIfChanged(ref _oldValue, value);
        }

        public string? NewValue
        {
            get => _newValue;
            set => RaiseAndSetIfChanged(ref _newValue, value);
        }

        public List<VoterHistory>? VoterHistories
        {
            get => _voterHistories;
            set => RaiseAndSetIfChanged(ref _voterHistories, value);
        }

        public bool Equals(VoterHistoryFieldViewModel? other)
        {
            return Id == other?.Id;
        }

        public override bool Equals(object? other)
        {
            if (other is VoterHistoryFieldViewModel voterHistory)
            {
                return Equals(voterHistory);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
