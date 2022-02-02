using Bazaro.Web.Services.ViewModels;
using Blazorise.Charts;

namespace Bazaro.Web.Pages.Main
{
    public partial class Dashboard
    {
        List<EntryModel> _last5Entries;

        protected override async Task OnInitializedAsync()
        {
            await LoadThisWeeksEvents();
        }

        // Recently Opened
        private async Task Load5LastNotes()
        {

            // Load _last5Entries with entryService
        }

        private List<CalendarEntryModel> _calendarEntries { get; set; } = new();
        private DateTime PanelDate = DateTime.Now;

        private async Task LoadThisWeeksEvents()
        {
            var firstDay = new DateTime(PanelDate.Year, PanelDate.Month, 1);
            var username = httpContext.HttpContext.User.Identity.Name;
            string userId = (await userManager.FindByNameAsync(username)).Id;
            if (userId is null) return;
            _calendarEntries = (await calendarService.GetCalendarEntries(new Services.Queries.CalendarEntries.GetCalenderEntriesByUserId.Query
            {
                UserId = userId,
                StartDate = firstDay,
                EndDate = firstDay.AddDays(7)
            })).Take(10)
            .ToList();

            StateHasChanged();
        }

    }
}