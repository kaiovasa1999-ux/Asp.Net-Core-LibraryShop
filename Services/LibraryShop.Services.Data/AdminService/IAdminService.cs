namespace LibraryShop.Services.Data.AdminService
{
    using System.Threading.Tasks;

    using LibraryShop.Web.ViewModels.Administration.Models;

    public interface IAdminService
    {
        Task CreateNewGener(string generName);

        Task DeleteGener(string generName);

        Task DeleteDealerById(int dealerId);
    }
}
