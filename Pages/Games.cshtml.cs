using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SteamShelf.Model;
using SteamShelf.Services;
using System.Security.Claims;

namespace SteamShelf.Pages
{
    [Authorize]
    public class GamesModel : PageModel
    {
        public string ViewType { get; set; } = "grid";
        public string SearchTerm { get; set; } = string.Empty;
        public SteamPlayer? SteamPlayer { get; set; }
        public List<SteamGame>? SteamOwnedGames { get; set; }
        public List<SteamGame>? FilteredGames { get; set; }

        private readonly SteamApiService _steamService;
        private readonly ILogger<GamesModel> _logger;

        public GamesModel(ILogger<GamesModel> logger, SteamApiService steamService)
        {
            _logger = logger;
            _steamService = steamService;
        }

        public async Task OnGet(string view, string search)
        {
            if (!string.IsNullOrEmpty(view) && (view == "grid" || view == "list"))
            {
                ViewType = view;
            }

            SearchTerm = search ?? "";

            var openId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(openId))
            {
                SteamPlayer = await _steamService.GetPlayerAsync(openId);
                SteamOwnedGames = await _steamService.GetOwnedGamesAsync(openId);

                ViewData["SteamPlayer"] = SteamPlayer;

                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    FilteredGames = SteamOwnedGames
                        ?.Where(g => g.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                else
                {
                    FilteredGames = SteamOwnedGames;
                }
            }
        }
    }
}
