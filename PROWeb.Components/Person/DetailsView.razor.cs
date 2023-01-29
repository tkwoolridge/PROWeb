using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Contexts;
using System.Linq.Expressions;
using Telerik.Blazor.Components;

namespace PROWeb.Components.Person
{
    public abstract class DetailsViewBase<TPersonViewModel> : PROView<TPersonViewModel> where TPersonViewModel : SlimViewModelBase
    {
    }

    public abstract partial class DetailsView<TPersonViewModel> : DetailsViewBase<TPersonViewModel>
        where TPersonViewModel : SlimViewModelBase
    {
        [Parameter]
        public bool ShowMaidenName { get; set; } = true;

        [Parameter]
        public int RowCount { get; set; } = 7;

        internal DetailsContext<TPersonViewModel> Context { get; set; } = new();

        private readonly Expression<Func<TPersonViewModel, int?>>? _personIdPath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _titlePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _firstNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _lastNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _middleNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _maidenNamePath;
        private readonly Expression<Func<TPersonViewModel, char?>>? _genderPath;
        private readonly Expression<Func<TPersonViewModel, DateTime?>>? _dateOfBirthPath;

        protected DetailsView(
            Expression<Func<TPersonViewModel, int?>>? personIdPath = null,
            Expression<Func<TPersonViewModel, string?>>? titlePath = null,
            Expression<Func<TPersonViewModel, string?>>? firstNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? lastNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? middleNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? maidenNamePath = null,
            Expression<Func<TPersonViewModel, char?>>? genderPath = null,
            Expression<Func<TPersonViewModel, DateTime?>>? dateOfBirthPath = null)
        {
            _personIdPath = personIdPath;
            _titlePath = titlePath;
            _firstNamePath = firstNamePath;
            _lastNamePath = lastNamePath;
            _middleNamePath = middleNamePath;
            _maidenNamePath = maidenNamePath;
            _genderPath = genderPath;
            _dateOfBirthPath = dateOfBirthPath;
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(Context);
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            Context.UnBind();
            if (Model is { } model)
            {
                Context.Bind
                (
                model,
                _personIdPath,
                _titlePath,
                _firstNamePath,
                _lastNamePath,
                _middleNamePath,
                _maidenNamePath,
                _genderPath,
                _dateOfBirthPath
                );
            }
        }
    }
}
