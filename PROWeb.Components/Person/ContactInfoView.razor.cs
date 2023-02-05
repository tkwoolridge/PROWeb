using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Contexts;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class ContactInfoViewBase<TContactInfoViewModel> : PROEditableView<TContactInfoViewModel> where TContactInfoViewModel : SlimViewModelBase
    {
    }

    public abstract partial class ContactInfoView<TContactInfoViewModel> : ContactInfoViewBase<TContactInfoViewModel> where TContactInfoViewModel : SlimViewModelBase
    {
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _emailPath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _contactPhonePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _phoneHomePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _phoneWorkPath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _phoneMobilePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _driverLicensePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _commentPath;

        internal ContactInfoContext<TContactInfoViewModel> Context { get; set; } = new();

        protected ContactInfoView(
            Expression<Func<TContactInfoViewModel, string?>>? emailPath = null,
            Expression<Func<TContactInfoViewModel, string?>>? contactPhonePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneHomePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneWorkPath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneMobilePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? driverLicensePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? commentPath = null)
        {
            _emailPath = emailPath;
            _contactPhonePath = contactPhonePath;
            _phoneHomePath = phoneHomePath;
            _phoneWorkPath = phoneWorkPath;
            _phoneMobilePath = phoneMobilePath;
            _driverLicensePath = driverLicensePath;
            _commentPath = commentPath;
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
                 _emailPath,
                _contactPhonePath,
                _phoneHomePath,
                _phoneWorkPath,
                _phoneMobilePath,
                _driverLicensePath,
                _commentPath
                );
            }
        }
    }
}
