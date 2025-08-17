using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Components.Configurations;
using PROWeb.Components.Person.Contexts;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class PhotosViewBase<TPhotosViewModel> : PROEditableView<TPhotosViewModel> where TPhotosViewModel : SlimViewModelBase
    {
    }

    public abstract partial class PhotosView<TPhotosViewModel> : PhotosViewBase<TPhotosViewModel> where TPhotosViewModel : SlimViewModelBase
    {
        private readonly Expression<Func<TPhotosViewModel, string?>>? _tcdPhoto;
        private readonly Expression<Func<TPhotosViewModel, string?>>? _proPhoto;

        [Parameter]
        public bool ShowTCDPhoto { get; set; } = true;
        
        [Parameter]
        public bool ShowPROPhoto { get; set; } = true;

        public string? TCDPhotoPath => _options.Value.TCDPhotosPath;

        public string? PROPhotoPath => "";

        [Parameter]
        public string? TCDPhotoTitle { get; set; } = "TCD";

        [Parameter]
        public string? PROPhotoTitle { get; set; } = "PRO";


        [Inject]
        private IOptions<TCDSettings> _options { get; set; } = default!;

        internal PhotosContext<TPhotosViewModel> PhotosContext { get; set; } = new();


        public PhotosView
            (
             Expression<Func<TPhotosViewModel, string?>>? tcdPhoto = null,
             Expression<Func<TPhotosViewModel, string?>>? proPhoto = null
            )
        {
            _tcdPhoto = tcdPhoto;
            _proPhoto = proPhoto;
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(PhotosContext);
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            PhotosContext.UnBind();
            if (Model is { } model)
            {
                PhotosContext.Bind
                (
                model,
                 _proPhoto,
                _tcdPhoto
                );
            }
        }
    }
}
