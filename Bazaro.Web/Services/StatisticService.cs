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

        public Task<List<MonthlyStateModel>> GetMonthlyStatsByUserId(GetMonthlyNotesCount.Query request) => GetMonthlyNotesCount.Handle(_context, request);
        public Task<List<EntryModel>> GetRecentlyUpdatedEntries(GetRecentEntriesByUserId.Query request) => GetRecentEntriesByUserId.Handle(_context, request);
    }
}
