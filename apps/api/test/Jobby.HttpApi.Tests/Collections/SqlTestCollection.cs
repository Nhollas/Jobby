using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Collections;

[CollectionDefinition("SqlCollection")]
public class SqlTestCollection : ICollectionFixture<JobbyHttpApiFactory>{ }