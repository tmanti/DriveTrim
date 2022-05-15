using System.Collections;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveTrimBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class trimController : ControllerBase
    {
        private readonly ILogger<trimController> _logger;
        private readonly GoogleAPI _googleApi;
        
        public trimController(ILogger<trimController> logger)
        {
            _logger = logger;
            _googleApi = new GoogleAPI();
        }


        [HttpPost]
        public ActionResult Post(TrimRequest request)
        {
            Thread thread = new Thread(()=>ImageComparison.Compare(_googleApi, request));
            thread.Start();

            return new OkResult();
        }
    }
}