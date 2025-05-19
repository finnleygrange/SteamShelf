using Microsoft.AspNetCore.Mvc.RazorPages;
using SteamShelf.Model;
using SteamShelf.Services;
using System.Security.Claims;


namespace SteamShelf.Pages
{
    public class IndexModel : PageModel
    {
        public SteamPlayer? SteamPlayer { get; set; }
        public List<SteamGame>? SteamOwnedGames { get; set; }
        public int TotalGames => SteamOwnedGames?.Count ?? 0;
        public int TotalplaytimeHours => (int)((SteamOwnedGames?.Sum(g => g.PlaytimeForever) ?? 0) / 60.0);
        public double PlaytimeLast2WeeksHours => (SteamOwnedGames?.Sum(g => g.Playtime2Weeks) ?? 0) / 60.0;
        public List<SteamGame> RecentlyPlayedGames => SteamOwnedGames?.Where(g => g.Playtime2Weeks > 0)
                    .OrderByDescending(g => g.Playtime2Weeks)
                    .ToList() ?? new List<SteamGame>();


        private readonly SteamApiService _steamService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, SteamApiService steamService)
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
            }
        }
    }
}
