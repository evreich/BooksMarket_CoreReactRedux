using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.EF.SeedDbHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksMarket_CoreReactRedux.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles =
          StandartIdentityDataConstants.ADMIN_ROLE + ","
        + StandartIdentityDataConstants.BOOKKEEPER_ROLE + ","
        + StandartIdentityDataConstants.USER_ROLE)]
    public class StoreController : Controller
    {
    }
}