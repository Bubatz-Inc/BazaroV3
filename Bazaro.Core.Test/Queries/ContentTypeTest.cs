using Bazaro.Test.Base;
using Bazaro.Web.Models;
using Bazaro.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bazaro.Core.Test.Queries
{
    public class ContentTypeTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly ContentTypeSevice _service;

        public ContentTypeTest(DatabaseBaseFixture fixture) : base(fixture)
        {
            _service = _scope.GetInstance<ContentTypeSevice>();

            CreateDatabaseOnTimeData();
        }

        protected override async void DatabaseOneTimeData()
        {
            _context.Add(new ContentType
            {
                Title = "Test",
                Created = DateTime.Now,
            });

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetContentTypesPassingTest()
        {
            Assert.NotEmpty(_context.Set<ContentType>());

            var data = await _service.GetContentTypes(new Web.Services.Queries.ContentTypes.GetContentTypes.Query());

            Assert.NotNull(data);
            Assert.NotEmpty(data);

            var item = data.First();

            Assert.NotNull(item);
            Assert.Equal("Test", item.Title);
        }
    }
}
