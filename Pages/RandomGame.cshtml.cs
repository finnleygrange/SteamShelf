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
        public SteamGame? RandomGame {  get; set; }

        [BindProperty]
        public bool OnlyUnplayedGames { get; set; } = false;

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

                if (!OnlyUnplayedGames)
                {
                    RandomGame = SteamOwnedGames?[rnd.Next(SteamOwnedGames.Count)];
                }
                else
                {
                    var unplayedGames = SteamOwnedGames?.Where(g => g.PlaytimeForever == 0).ToList();
                    RandomGame = unplayedGames?[rnd.Next(unplayedGames.Count)];
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var openId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(openId))
            {
                SteamPlayer = await _steamService.GetPlayerAsync(openId);
                SteamOwnedGames = await _steamService.GetOwnedGamesAsync(openId);

                var rnd = new Random();

                if (OnlyUnplayedGames)
                {
                    var unplayed = SteamOwnedGames?.Where(g => g.PlaytimeForever == 0).ToList();
                    if (unplayed != null && unplayed.Count > 0)
                        RandomGame = unplayed[rnd.Next(unplayed.Count)];
                }
                else
                {
                    if (SteamOwnedGames != null && SteamOwnedGames.Count > 0)
                        RandomGame = SteamOwnedGames[rnd.Next(SteamOwnedGames.Count)];
                }
            }

            return Page();
        }
    }
}
