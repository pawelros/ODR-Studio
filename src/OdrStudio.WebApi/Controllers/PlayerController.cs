using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OdrStudio.WebApi.Models.Player;

namespace OdrStudio.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerClient playerClient;
        private readonly IMotSlideShowRetriever motSlideShowRetriever;
        private readonly IMotSlideshowSender motSlideshowSender;
        
        public PlayerController(IPlayerClient playerClient, IMotSlideShowRetriever motSlideShowRetriever, IMotSlideshowSender motSlideshowSender)
        {
            this.playerClient = playerClient;
            this.motSlideShowRetriever = motSlideShowRetriever;
            this.motSlideshowSender = motSlideshowSender;
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

        // PUT api/player/
        [HttpPut("motslideshow/{*path}")]
        public void Put(string path)
        {
            this.motSlideshowSender.Send(path);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
