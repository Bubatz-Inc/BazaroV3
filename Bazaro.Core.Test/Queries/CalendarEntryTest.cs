using Bazaro.Test.Base;
using Bazaro.Web.Models;
using Bazaro.Web.Services;
using System;
using Xunit;

namespace Bazaro.Core.Test.Queries
{
    public class CalendarEntryTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly CalendarService _service;

        public CalendarEntryTest(DatabaseBaseFixture fixture) : base(fixture)
        {
            _service = _scope.GetInstance<CalendarService>();

            CreateDatabaseOnTimeData();
        }

        protected override async void DatabaseOneTimeData()
        {
            var entry = new Entry
            {
                Title = "test",
                Description = "test",
                Created = DateTime.Now,
                StartItemId = null
            };

            _context.Add(entry);

            await _context.SaveChangesAsync();

            _context.Add(new CalendarEntry
            {
                EntryId = entry.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Created = DateTime.Now,
            });

            await _context.SaveChangesAsync();
        }
    }
}
