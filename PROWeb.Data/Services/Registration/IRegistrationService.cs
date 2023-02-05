using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Registration
{
    public interface IRegistrationService
    {
        IQueryable<FormType> GetFormTypes();
    }
}