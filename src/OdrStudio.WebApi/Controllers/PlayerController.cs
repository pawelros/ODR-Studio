using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using OdrStudio.WebApi.Models.Player;

namespace OdrStudio.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerClient playerClient;
        private readonly IMotSlideShowRetriever motSlideShowRetriever;

        public PlayerController(IPlayerClient playerClient, IMotSlideShowRetriever motSlideShowRetriever)
        {
            this.playerClient = playerClient;
            this.motSlideShowRetriever = motSlideShowRetriever;
        }

        // GET: api/player/status
        [HttpGet("status")]
        public async Task<IPlayerStatus> Get()
        {
            IPlayerStatus status = await this.playerClient.GetStatus();

            return status;
        }

        // GET api/player/motslideshow/{path}
        [HttpGet("motslideshow/{*path}")]
        public FileResult Get(string path)
        {
            return this.motSlideShowRetriever.RetrieveImage(path);
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
