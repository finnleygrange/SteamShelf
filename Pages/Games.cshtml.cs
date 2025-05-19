using Microsoft.AspNetCore.Mvc.RazorPages;
using SteamShelf.Model;
using SteamShelf.Services;
using System.Security.Claims;

namespace SteamShelf.Pages
{
    public class GamesModel : PageModel
    {
        public string ViewType { get; set; } = "grid";
        public SteamPlayer? SteamPlayer { get; set; }
        public List<SteamGame>? SteamOwnedGames { get; set; }

        private readonly SteamApiService _steamService;
        private readonly ILogger<GamesModel> _logger;

        public GamesModel(ILogger<GamesModel> logger, SteamApiService steamService)
        {
            _logger = logger;
            _steamService = steamService;
        }

        public async Task OnGet(string view)
        {
            if (!string.IsNullOrEmpty(view) && (view == "grid" || view == "list"))
            {
                ViewType = view;
            }

            var openId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(openId))
            {
                SteamPlayer = await _steamService.GetPlayerAsync(openId);
                SteamOwnedGames = await _steamService.GetOwnedGamesAsync(openId);

                ViewData["SteamPlayer"] = SteamPlayer;
            }
        }
    }
}
