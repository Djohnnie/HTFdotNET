using System.Diagnostics;
using System.Web.Http;
using HTF.Mars.StreamSoure.Web.Api.Models;

namespace HTF.Mars.StreamSoure.Web.Api.Controllers
{
    public class SamplesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(Sample sample)
        {
            Debug.WriteLine(sample);
            return Ok();
        }

        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }
    }
}