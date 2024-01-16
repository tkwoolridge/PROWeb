using PROWeb.Common.ViewModels;
using PROWeb.Data.Models;

namespace PROWeb.Components.Voters.ViewModels
{
    public class VoterHistoryViewModel : SlimViewModelBase
    {
        private int _registryYear;
        private int _voterId;
        private DateTime _created;
        private string _userName = default!;
        private int? _registrationId;
        private Voter? _voter;
        private ICollection<VoterHistoryField>? _fields;

        public int  Id => HashCode.Combine(RegistryYear, VoterId, Created);

        public int RegistryYear 
        { 
            get => _registryYear; 
            set => RaiseAndSetIfChanged(ref _registryYear, value); 
        }

        public int VoterId 
        { 
            get => _voterId; 
            set => RaiseAndSetIfChanged(ref _voterId, value); 
        }

        public DateTime Created 
        { 
            get => _created; 
            set => RaiseAndSetIfChanged(ref _created, value); 
        }

        public string UserName 
        { 
            get => _userName; 
            set => RaiseAndSetIfChanged(ref _userName, value); 
        }

        public int? RegistrationId 
        { 
            get => _registrationId; 
            set => RaiseAndSetIfChanged(ref _registrationId, value); 
        }

        public Voter? Voter 
        { 
            get => _voter; 
            set => RaiseAndSetIfChanged(ref _voter, value); 
        }

        public ICollection<VoterHistoryField>? Fields 
        { 
            get => _fields; 
            set => RaiseAndSetIfChanged(ref _fields, value); 
        }

        public string Description => $"Update by {UserName} on {Created.ToString("f")}";

        public bool Equals(VoterHistoryViewModel? other)
        {
            return VoterId == other?.VoterId &&
                Created == other?.Created &&
                RegistryYear == other?.RegistryYear;
        }

        public override bool Equals(object? other)
        {
            if (other is VoterHistoryViewModel voterHistory)
            {
                return Equals(voterHistory);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VoterId, RegistryYear, Created);
        }
    }
}
