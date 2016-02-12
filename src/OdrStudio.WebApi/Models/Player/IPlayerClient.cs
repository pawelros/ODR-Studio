using System.Threading.Tasks;

namespace OdrStudio.WebApi.Models.Player
{
    public interface IPlayerClient
    {
        Task<IPlayerStatus> GetStatus();
    }
}