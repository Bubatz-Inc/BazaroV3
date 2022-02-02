using Bazaro.Web;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Xunit;

namespace Bazaro.Test.Base
{
    [Collection(nameof(TestBase))]
    public abstract class TestBase
    {
        protected readonly Scope _scope;
        protected readonly BazaroContext _context;

        //protected readonly 

        private readonly DatabaseBaseFixture _fixture;

        public TestBase(DatabaseBaseFixture fixture)
        {
            _fixture = fixture;

            _scope = AsyncScopedLifestyle.BeginScope(fixture._container);
            _context = _scope.GetInstance<BazaroContext>();
        }

        protected void CreateDatabaseOnTimeData() => _fixture.CreateDatabaseDataOneTime(() => DatabaseOneTimeData());

        protected virtual void DatabaseOneTimeData()
        {

        }
    }
}
