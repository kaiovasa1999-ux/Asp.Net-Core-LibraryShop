namespace LibraryShop.Web.Areas.Administration.Controllers
{
    using LibraryShop.Common;
    using LibraryShop.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.DealerRoleName)]
    [Area("DealerRole")]
    public class DealerControllerArea : BaseController
    {
    }
}
