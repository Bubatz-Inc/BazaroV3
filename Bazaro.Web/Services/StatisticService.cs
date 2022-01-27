using Bazaro.Web.Services.Queries.Statistics;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services
{
    public class StatisticService
    {
        private readonly BazaroContext _context;

        public StatisticService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<MonthlyStatViewModel>> GetMonthlyStatsByUserId(string userId) => GetMonthlyNotesCount.Handle(_context, new GetMonthlyNotesCount.Query { UserId = userId });
    }
}
