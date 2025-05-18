using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SteamShelf.Model;
using SteamShelf.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SteamShelf.Pages
{
    public class IndexModel : PageModel
    {
        public SteamPlayer? SteamPlayer { get; set; }

        private readonly SteamApiService _steamService;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, SteamApiService steamService)
        {
            _logger = logger;
            _steamService = steamService;
        }

        public async Task OnGet()
        {
            var steamId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(steamId))
            {
                SteamPlayer = await _steamService.GetPlayerAsync(steamId);
            }
        }
    }
}
