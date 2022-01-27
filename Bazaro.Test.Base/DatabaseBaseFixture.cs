using Bazaro.Web;
using Bazaro.Web.Services;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Bazaro.Test.Base
{
    public class DatabaseBaseFixture : IDisposable
    {
        internal readonly Container _container;

        private bool _databaseOnTimeCalled = false;

        public DatabaseBaseFixture()
        {

        }

        private Container BildContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            builder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning));

            container.RegisterInstance(builder.Options);
            container.Register<BazaroContext, BazaroContext>(Lifestyle.Scoped);

            container.Register<EntryService>(Lifestyle.Transient);
            container.Register<FolderService>(Lifestyle.Transient);
            container.Register<ItemService>(Lifestyle.Transient);
            container.Register<EntryRelationService>(Lifestyle.Transient);
            container.Register<StatisticService>(Lifestyle.Transient);
            container.Register<ContentTypeSevice>(Lifestyle.Transient);

            return container;
        }

        internal void CreateDatabaseDataOneTime(Action fillMethod)
        {
            if (_databaseOnTimeCalled)
                return;

            _databaseOnTimeCalled = true;
            fillMethod.Invoke();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
