using Jobby.HttpApi.Tests.Setup;

namespace Jobby.HttpApi.Tests.Collections;

[CollectionDefinition("SqlCollection")]
public class SqlTestCollection : ICollectionFixture<JobbyHttpApiFactory>;