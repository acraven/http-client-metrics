using System.Threading.Tasks;
using DemoApi.Clients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("decorated")]
    [ApiController]
    public class DecoratedClientController : ControllerBase
    {
        private readonly DecoratedClient _client;

        public DecoratedClientController(DecoratedClient client)
        {
            _client = client;
        }

        [HttpPost]
        public void Post()
        {
            // Fire-and-forget
            MetricFactory.MeasureAsync(() => _client.PostAsync("events/decorated"));
        }
    }
}
