using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class PhotosContext<TPhotosViewModel> : ViewModelContext<TPhotosViewModel> where TPhotosViewModel : SlimViewModelBase
    {
        private string? _proPhoto;

        public string? PROPhoto
        {
            get => _proPhoto;
            set => RaiseAndSetIfChanged(ref _proPhoto, value.ToNullIfWhiteSpace());
        }

        private string? _tcdPhoto;

        public string? TCDPhoto
        {
            get => _tcdPhoto;
            set => RaiseAndSetIfChanged(ref _tcdPhoto, value.ToNullIfWhiteSpace());
        }

        public void Bind(
            TPhotosViewModel model,
            Expression<Func<TPhotosViewModel, string?>>? proPhotosPathPath = null,
            Expression<Func<TPhotosViewModel, string?>>? tcdPhotosPathPath = null)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model?.Bind(this, proPhotosPathPath, c => c.PROPhoto, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, tcdPhotosPathPath, c => c.TCDPhoto, StrongBindingMode.TwoWay));
            }
        }
    }
}
