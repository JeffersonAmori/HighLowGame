using LoggerAdapter;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HighLowGame.Pages
{
    public sealed class IndexModel : PageModel
    {
        private readonly ILoggerAdapter<IndexModel> _logger;

        public IndexModel(ILoggerAdapter<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("New request on Index.");
        }
    }
}