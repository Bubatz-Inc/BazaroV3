using Bazaro.Test.Base;
using Bazaro.Web.Services;
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

        protected override void DatabaseOneTimeData()
        {
            base.DatabaseOneTimeData();
        }
    }
}
