﻿using Bazaro.Web.Models;
using Bazaro.Web.Services;
using Bazaro.Web.Services.ViewModels;
using Blazorise.Charts;
using System.Text;

namespace Bazaro.Web.Pages.Main
{
    public partial class Dashboard
    {
        List<EntryModel> _last5Entries;

        protected override async Task OnInitializedAsync()
        {
            await Load5LastNotes();
            await LoadThisWeeksEvents();
        }

        // Recently Opened
        private async Task Load5LastNotes()
        {
            string userId = await GetCurrentUserId();

            _last5Entries = (await statisticsService.GetRecentlyUpdatedEntries(new Services.Queries.Statistics.GetRecentEntriesByUserId.Query
            {
                UserId = userId,
                MaxCount = 10,
            }));
        }

        private List<CalendarEntryModel> _calendarEntries { get; set; } = new();
        private DateTime PanelDate = DateTime.Now;

        private async Task LoadThisWeeksEvents()
        {
            var firstDay = new DateTime(PanelDate.Year, PanelDate.Month, 1);
            string userId = await GetCurrentUserId();
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

        private async Task<string> GetCurrentUserId()
        {
            var username = httpContext.HttpContext.User.Identity.Name;
            return (await userManager.FindByNameAsync(username)).Id;
        }

        private async Task OpenEntry(int entryId)
        {
            Nav.NavigateTo($"/noteeditor/{entryId}");
        }

        private async Task CreateNewQucikNote(string content)
        {
            var userId = await GetCurrentUserId();

            var rootfolder = await folderService.GetViewFolderStructureByUserId(new Services.Queries.Folders.GetViewSubfoldersStructureByUserId.Query
            {
                UserId = userId
            });

            await entryService.Insert(new Services.Commands.Entries.InsertEntry.Command
            {
                FolderId = rootfolder.Id,
                Title = "New Quicknote " + DateOnly.FromDateTime(DateTime.Now),
            });

            //await itemService.Insert(new Services.Commands.Items.InsertItem.Command
            //{
            //    ContentTypeId = 1,
            //    Content = Encoding.UTF8.GetBytes(TextAreaValue),
            //    EntryId = EntryId
            //}
            //); 
        
        }
    }
}