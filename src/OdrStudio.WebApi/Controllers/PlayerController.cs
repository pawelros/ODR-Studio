using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OdrStudio.WebApi.Models.Player;

namespace OdrStudio.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerClient playerClient;

        public PlayerController(IPlayerClient playerClient)
        {
            this.playerClient = playerClient;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IPlayerStatus> Get()
        {
            IPlayerStatus status = await this.playerClient.GetStatus();

            return status;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
