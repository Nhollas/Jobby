using Jobby.HttpApi.Tests.Factories;

namespace Jobby.HttpApi.Tests.Collections;

[CollectionDefinition("SqlCollection")]
public class SqlTestCollection : ICollectionFixture<JobbyHttpApiFactory>;