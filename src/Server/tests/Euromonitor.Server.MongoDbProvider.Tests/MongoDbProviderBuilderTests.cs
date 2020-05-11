using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Euromonitor.Server.MongoDbProvider.Tests
{
    public class MongoDbProviderBuilderTests
    {
        MongoDbProviderBuilder _dbProviderBuilder;

        public MongoDbProviderBuilderTests()
        {
            _dbProviderBuilder = new MongoDbProviderBuilder();
        }

        [Fact]
        public void SetCollection_EmptyCollection_NullCollection()
        {
            var actualResult = _dbProviderBuilder.SetCollection("").Build().Collection;

            Assert.Null(actualResult);
        }
    }
}
