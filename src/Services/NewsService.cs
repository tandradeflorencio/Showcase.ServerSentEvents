using Showcase.ServerSentEvents.Domain;
using Showcase.ServerSentEvents.Services.Interfaces;

namespace Showcase.ServerSentEvents.Services
{
    public class NewsService : INewsService
    {
        private const int MinSecondsToDelay = 0;
        private const int MaxSecondsToDelay = 15;

        public async Task<MessageResponse> GetAsync()
        {
            var randomSecond = Random.Shared.Next(MinSecondsToDelay, MaxSecondsToDelay);

            await Task.Delay(TimeSpan.FromSeconds(randomSecond));

            return new MessageResponse($"Item generated in ({randomSecond}) second(s).", DateTime.Now);
        }
    }
}