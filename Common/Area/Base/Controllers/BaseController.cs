using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Abstraction;
using System.Net;

namespace Common.Area.Base.Controllers
{
    public class BaseController : ControllerBase
    {
        internal readonly IServiceManager _service;
        public BaseController(IServiceManager service)
        {
            _service = service;
        }


    }
}
