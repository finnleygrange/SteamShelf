using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SteamShelf.Model;
using SteamShelf.Services;
using System.Security.Claims;

namespace SteamShelf.Pages
{
    public class RandomGameModel : PageModel
    {
        public SteamPlayer? SteamPlayer { get; set; }
        public List<SteamGame>? SteamOwnedGames { get; set; }

        private readonly SteamApiService _steamService;
        private readonly ILogger<RandomGameModel> _logger;

        public RandomGameModel(SteamApiService steamService, ILogger<RandomGameModel> logger)
        {
            _logger = logger;
            _steamService = steamService;
        }

        public async Task OnGetAsync()
        {
            var openId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(openId))
            {
                SteamPlayer = await _steamService.GetPlayerAsync(openId);
                SteamOwnedGames = await _steamService.GetOwnedGamesAsync(openId);

                ViewData["SteamPlayer"] = SteamPlayer;

                Random rnd = new Random();
                var unplayedGames = SteamOwnedGames?.ToList();
                var randomGame = unplayedGames?[rnd.Next(unplayedGames.Count)];
            }
        }
    }
}
