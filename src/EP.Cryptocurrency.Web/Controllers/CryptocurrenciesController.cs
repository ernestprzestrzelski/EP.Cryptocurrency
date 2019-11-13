using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EP.Cryptocurrency.Core.Abstractions;

namespace EP.Cryptocurrency.Web.Controllers
{
    public class CryptocurrenciesController : Controller
    {
        private readonly ICryptocurrencyService _cryptocurrencyService;

        public CryptocurrenciesController(ICryptocurrencyService cryptocurrencyService)
        {
            _cryptocurrencyService = cryptocurrencyService;
        }

        // GET: Cryptocurrencies
        public async Task<IActionResult> Index()
        {
            return View(await _cryptocurrencyService.GetList(c => c.Rank).ConfigureAwait(false));
        }

        // GET: Cryptocurrencies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _cryptocurrencyService.GetDetails(id).ConfigureAwait(false));
        }
    }
}
