using DemoApi.Clients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("undecorated")]
    [ApiController]
    public class UndecoratedClientController : ControllerBase
    {
        private readonly UndecoratedClient _client;

        public UndecoratedClientController(UndecoratedClient client)
        {
            _client = client;
        }

        [HttpPost]
        public void Post()
        {
            // Fire-and-forget
            //MetricFactory.MeasureAsync(() => _client.PostAsync("events/undecorated"));
        }
    }
}
