using PROWeb.ExternalResources.TCDResources.Contracts;

namespace PROWeb.ExternalResources.TCDResources
{
    public interface ITCDClient
    {
        Task<TCDPhotoResponse> GetPhotoAsync(TCDPhotoRequest request);

        Task<TCDPhotoResponse> GetPhotoAsync(string personId);
    }
}