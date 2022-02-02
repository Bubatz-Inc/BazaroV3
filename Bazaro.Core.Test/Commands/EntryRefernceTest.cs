using Bazaro.Test.Base;
using Bazaro.Web.Services;
using Xunit;

namespace Bazaro.Core.Test.Commands
{
    public class EntryRefernceTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly EntryRelationService _service;

        public EntryRefernceTest(DatabaseBaseFixture fixture) : base(fixture)
        {

        }
    }
}
