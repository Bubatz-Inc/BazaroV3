using Bazaro.Web;
using Bazaro.Web.Models;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
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
            _context.Add(new User
            {
                Email = "test@test.test",
                Created = DateTime.Now,
                EmailConfirmed = true,
                NormalizedEmail = "Test@TEST.TEST"
            });
        }
    }
}
