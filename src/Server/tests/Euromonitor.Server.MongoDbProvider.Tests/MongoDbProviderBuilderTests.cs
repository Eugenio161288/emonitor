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
        public void SetCollection_EmptyCollection_ReturnsNullCollection()
        {
            var actualResult = _dbProviderBuilder.SetCollection("").Build().Collection;

            Assert.Null(actualResult);
        }

        [Fact]
        public void SetCollection_NotEmptyCollection_StoresProvidedCollection()
        {
            var expectedResult = "Collection_Name";

            var actualResult = _dbProviderBuilder.SetCollection("Collection_Name").Build().Collection;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void SetCollection_NotEmptyCollection_ReturnsNotNullCollection()
        {
            var actualResult = _dbProviderBuilder.SetCollection("Collection_Name").Build().Collection;

            Assert.NotNull(actualResult);
        }

        [Fact]
        public void SetConnectionString_NotEmptyConnectionString_StoresProvidedConnectionString()
        {
            var expectedResult = "connectionString";

            var actualResult = _dbProviderBuilder.SetConnectionString("connectionString").Build().ConnectionString;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void SetConnectionString_NotEmptyConnectionString_NotNullConnectionString()
        {
            var actualResult = _dbProviderBuilder.SetConnectionString("connectionString").Build().ConnectionString;

            Assert.NotNull(actualResult);
        }

        [Fact]
        public void SetConnectionString_EmptyConnectionString_ReturnsNullConnectionString()
        {
            var actualResult = _dbProviderBuilder.SetConnectionString("").Build().ConnectionString;

            Assert.Null(actualResult);
        }

        [Fact]
        public void SetDatabase_EmptyDbName_ReturnsNullDatabase()
        {
            var actualResult = _dbProviderBuilder.SetDatabase("").Build().Database;

            Assert.Null(actualResult);
        }

        [Fact]
        public void SetDatabase_NotEmptyDbName_ReturnsNotNullDbName()
        {
            var actualResult = _dbProviderBuilder.SetDatabase("DATABASE_NAME").Build().Database;

            Assert.NotNull(actualResult);
        }

        [Fact]
        public void SetDatabase_NotEmptyDbName_StoresProvidedDbName()
        {
            var expectedResult = "DATABASE_NAME";

            var actualResult = _dbProviderBuilder.SetDatabase("DATABASE_NAME").Build().Database;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Build_DefaultSettings_AlwaysNotNullDbProvider()
        {
            var dbProvider = _dbProviderBuilder.Build();

            Assert.NotNull(dbProvider);
        }

        [Fact]
        public void Build_NotDefaultSettings_ReturnsMongoDbProviderType()
        {
            var dbProvider = _dbProviderBuilder.SetCollection("Collection")
                                .SetDatabase("DATABASE")
                                .Build();

            Assert.IsType<MongoDbProvider>(dbProvider);
        }
    }
}
