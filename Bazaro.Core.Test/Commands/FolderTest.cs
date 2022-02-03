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
    public class FolderTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly FolderService _service;

        public FolderTest(DatabaseBaseFixture fixture) : base(fixture)
        {
            _service = _scope.GetInstance<FolderService>();
        }

        protected override async void DatabaseOneTimeData()
        {
            base.DatabaseOneTimeData();

            var userId = _context.Set<User>().First().Id;

            var folder = new Folder
            {
                PreviousFolderId = null,
                Description = "Test",
                Title = "Test",
                Created = DateTime.Now
            };

            _context.Add(folder);

            await _context.SaveChangesAsync();

            _context.Add(new UserFolderReference
            {
                FolderId = folder.Id,
                UserId = userId,
                IsShared = false,
                IsDeleted = false,
            });

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task InsertFolderPassingTest()
        {
            var oldData = await _context.Set<Folder>().ToListAsync();
            var oldRef = await _context.Set<UserFolderReference>().ToListAsync();

            _context.RemoveRange(_context.Set<Folder>());
            _context.RemoveRange(_context.Set<UserFolderReference>());

            await _context.SaveChangesAsync();

            Assert.Empty(_context.Set<Folder>());

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var userId = (await _context.Set<User>().FirstOrDefaultAsync()).Id;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Assert.NotNull(userId);

            await _service.Insert(new Web.Services.Commands.Folders.InsertFolder.Command
            {
                Title = "Test2",
                Description = "Test",
                PreviousFolder = null,
                UserId = userId
            });

            var data = await _context.Set<Folder>().ToListAsync();
            var references = await _context.Set<UserFolderReference>().ToListAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);

            Assert.NotNull(references);
            Assert.NotEmpty(references);
            Assert.Single(references);

            var item = data.First();
            var refItem = references.First();

            Assert.Equal("Test2", item.Title);
            Assert.Equal("Test", item.Description);
            Assert.Null(item.PreviousFolder);

            Assert.NotNull(refItem);
            Assert.Equal(userId, refItem.UserId);

            _context.RemoveRange(data);
            _context.RemoveRange(references);

            _context.AddRange(oldData);
            _context.AddRange(oldRef);

            await _context.SaveChangesAsync();

        }

        [Fact]
        public async Task UpdateFolderPassingTest()
        {
            var oldData = await _context.Set<Folder>().ToListAsync();

            Assert.NotEmpty(oldData);

            await _service.Update(new Web.Services.Commands.Folders.UpdateFolder.Command
            {
                Id = oldData.First().Id,
                Description = "Test2",
                Title = "Test2",
                PreviousFolderId = null
            });

            var data = await _context.Set<Folder>().ToListAsync();
            var references = await _context.Set<UserFolderReference>().ToListAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);

            Assert.NotNull(references);
            Assert.NotEmpty(references);
            Assert.Single(references);

            var item = data.First();
            var refItem = references.First();

            Assert.Equal("Test2", item.Title);
            Assert.Equal("Test2", item.Description);
            Assert.Null(item.PreviousFolder);

            Assert.NotNull(refItem);

            _context.RemoveRange(data);
            _context.RemoveRange(references);

            _context.AddRange(oldData);

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task DeleteFolderPassingTest()
        {
            var oldData = await _context.Set<Folder>().ToListAsync();

            Assert.NotEmpty(_context.Set<Folder>());
            Assert.NotEmpty(_context.Set<UserFolderReference>());

            await _service.Delete(new Web.Services.Commands.Folders.DeleteFolder.Command
            {
                Id = oldData.First().Id
            });


        }
    }
}
