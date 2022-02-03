using Bazaro.Test.Base;
using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bazaro.Core.Test.Commands
{
    public class EntryRefernceTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly EntryRelationService _service;

        public EntryRefernceTest(DatabaseBaseFixture fixture) : base(fixture)
        {
            _service = _scope.GetInstance<EntryRelationService>();

            CreateDatabaseOnTimeData();
        }

        protected override async void DatabaseOneTimeData()
        {
            var entry1 = new Entry
            {
                Description = "Test1",
                Created = DateTime.Now,
                Title = "Test1",
            };

            var entry2 = new Entry
            {
                Description = "Test2",
                Created = DateTime.Now,
                Title = "Test2",
            };

            _context.Add(entry1);
            _context.Add(entry2);

            await _context.SaveChangesAsync();

            _context.Add(new EntryReference
            {
                EntryId = entry1.Id,
                ReferenceEntryId = entry2.Id
            });

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task InsertEntryReferencePassingTest()
        {
            var oldData = await _context.Set<EntryReference>().ToListAsync();
            _context.RemoveRange(oldData);
            await _context.SaveChangesAsync();

            Assert.Empty(_context.Set<EntryReference>());

            await _service.Insert(new Web.Services.Commands.EntryReferences.InsertEntryReference.Command
            {
                EntryId = oldData.First().EntryId,
                RefernceEntryId = oldData.First().ReferenceEntryId
            });

            var data = await _context.Set<EntryReference>().ToListAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);

            var item = data.First();

            Assert.Equal(oldData.First().EntryId, item.EntryId);
            Assert.Equal(oldData.First().ReferenceEntryId, item.ReferenceEntryId);

            _context.RemoveRange(data);
            _context.AddRange(oldData);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task DeleteEntryReferencePassingTest()
        {
            Assert.NotEmpty(_context.Set<EntryReference>());

            var oldData = await _context.Set<EntryReference>().ToListAsync();

            await _service.Delete(new Web.Services.Commands.EntryReferences.DeleteEntryRefernce.Command
            {
                EntryId = oldData.First().EntryId,
                RefernceEntryId = oldData.First().ReferenceEntryId
            });

            var data = await _context.Set<EntryReference>().ToListAsync();

            Assert.NotNull(data);
            Assert.Empty(data);

            _context.AddRange(oldData);
            await _context.SaveChangesAsync();
        }
    }
}
