using Bazaro.Test.Base;
using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bazaro.Core.Test.Commands
{
    public class EntryTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly EntryService _service;

        public EntryTest(DatabaseBaseFixture fixture) : base(fixture)
        {
            _service = _scope.GetInstance<EntryService>();

            CreateDatabaseOnTimeData();
        }

        protected override async void DatabaseOneTimeData()
        {
            _context.Add(new Folder
            {
                Id = 1,
                PreviousFolderId = null,
                Description = "test",
                Created = DateTime.Now,
                Title = "test"
            });

            _context.Add(new Entry
            {
                Title = "Test",
                Description = "test",
                StartItemId = null,
                Created = DateTime.Now,
            });

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task InsertEntryPassingTest()
        {
            var dataOld = await _context.Set<Entry>().ToListAsync();

            _context.RemoveRange(_context.Set<Entry>());
            await _context.SaveChangesAsync();

            Assert.Empty(_context.Set<Entry>());

            await _service.Insert(new Web.Services.Commands.Entries.InsertEntry.Command
            {
                Description = "test",
                Title = "Test",
                FolderId = 1
            });

            var data = _context.Set<Entry>().ToList();
            var references = _context.Set<FolderEntryReference>();

            var dataItem = data.First();
            var dataRef = references.First();

            Assert.NotNull(dataItem);
            Assert.NotNull(dataRef);

            Assert.Equal("test", dataItem.Description);
            Assert.Equal("Test", dataItem.Title);

            Assert.Equal(1, dataRef.FolderId);
            
            _context.RemoveRange(_context.Set<Entry>());
            _context.RemoveRange(_context.Set<FolderEntryReference>());
            _context.AddRange(dataOld);
        }

        [Fact]
        public async Task UpdateEntryPassingTest()
        {
            var dataOld = _context.Set<Entry>().First();
            _context.Add(new FolderEntryReference
            {
                Created = DateTime.Now,
                EntryId = dataOld.Id,
                FolderId = null,
            });

            await _context.SaveChangesAsync();

            Assert.NotNull(dataOld);

            await _service.Update(new Web.Services.Commands.Entries.UpdateEntry.Command
            {
                Id = dataOld.Id,
                Description = "test1",
                Title = "Test1",
                OldFolderId = null,
                NewFolderId = 1
            });

            var newData = await _context.Set<Entry>().ToListAsync();
            var newDataRef = await _context.Set<FolderEntryReference>().FirstAsync();

            Assert.NotNull(newData);
            Assert.NotNull(newDataRef);
            Assert.NotEmpty(newData);
            Assert.Single(newData);

            var item = newData.First();

            Assert.NotNull(item);
            Assert.Equal("Test1", item.Title);
            Assert.Equal("test1", item.Description);

            Assert.Equal(1, newDataRef.FolderId);
        }
    }
}
