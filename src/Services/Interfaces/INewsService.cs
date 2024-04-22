using Showcase.ServerSentEvents.Domain;

namespace Showcase.ServerSentEvents.Services.Interfaces
{
    public interface INewsService
    {
        Task<MessageResponse> GetAsync();
    }
}