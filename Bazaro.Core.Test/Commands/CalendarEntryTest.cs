using Bazaro.Test.Base;
using Bazaro.Web.Models;
using Bazaro.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bazaro.Core.Test.Commands
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

        [Fact]
        public async Task InsertCalendarEntryPassingTest()
        {
            var oldData = await _context.Set<CalendarEntry>().ToListAsync();

            _context.RemoveRange(_context.Set<CalendarEntry>());
            await _context.SaveChangesAsync();

            Assert.Empty(_context.Set<CalendarEntry>());

            await _service.AddCalendarEntry(new Web.Services.Commands.CalendarEntries.InsertCalendarEntry.Command
            {
                EntryId = oldData.First().EntryId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            });

            var data = await _context.Set<CalendarEntry>().ToListAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);

            var item = data.First();

            Assert.NotNull(item);

            _context.RemoveRange(_context.Set<CalendarEntry>());
            _context.AddRange(oldData);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateCalendarEntryPassingTest()
        {
            var data = _context.Set<CalendarEntry>().First();

            Assert.NotEmpty(_context.Set<CalendarEntry>());
            Assert.Single(_context.Set<CalendarEntry>());

            await _service.UpdateCalendarEntry(new Web.Services.Commands.CalendarEntries.UpdateCalendarEntry.Command
            {
                CalendarEntryId = data.Id,
                EntryId = data.EntryId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            });

            var testData = await _context.Set<CalendarEntry>().ToListAsync();

            Assert.NotNull(testData);
            Assert.NotEmpty(testData);
            Assert.Single(testData);

            var item = testData.First();

            Assert.NotNull(item);
#pragma warning disable xUnit2002 // Do not use null check on value type
            Assert.NotNull(item.StartDate);
            Assert.NotNull(item.EndDate);
#pragma warning restore xUnit2002 // Do not use null check on value type
        }

        [Fact]
        public async Task DeleteCalendarEntryPassingTest()
        {
            var data = _context.Set<CalendarEntry>().First();
            var oldData = data;

            Assert.NotEmpty(_context.Set<CalendarEntry>());

            await _service.DeleteCalendarEntry(new Web.Services.Commands.CalendarEntries.DeleteCalendarEntryById.Command
            {
                CalendarEntryId = data.Id
            });

            Assert.Empty(_context.Set<CalendarEntry>());

            _context.AddRange(oldData);
            await _context.SaveChangesAsync();
        }
    }
}
