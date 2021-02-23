﻿namespace RD.CanMusicMakeYouRunFaster.Rest.IntegrationTests.TestUtils
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using NUnit.Framework;
    using RD.CanMusicMakeYouRunFaster.CommonTestUtils.Factories;
    using RD.CanMusicMakeYouRunFaster.FakeResponseServer.Controllers;
    using RD.CanMusicMakeYouRunFaster.FakeResponseServer.DbContext;
    using RD.CanMusicMakeYouRunFaster.FakeResponseServer.Models;
    using RD.CanMusicMakeYouRunFaster.Rest.DataRetrievalSources;

    /// <summary>
    /// Base test class for integration tests.
    /// </summary>
    public class TestsBase
    {
        private const string DatabaseName = "FakeDataRetrievalSourceDatabase";
        private DbContextOptions<DataRetrievalContext> contextOptions;
        public const string FakeServerAddress = "http://localhost:2222";
        public SpotifyClient spotifyClient;

        [OneTimeSetUp]
        public virtual void TestSetup()
        {
            HttpClient httpClient;
            var databaseRoot = new InMemoryDatabaseRoot();
            contextOptions = new DbContextOptionsBuilder<DataRetrievalContext>()
                    .UseInMemoryDatabase(DatabaseName, databaseRoot)
                    .Options;

            var webAppFactory = new InMemoryFactory<FakeResponseServer.Startup>(DatabaseName, databaseRoot);
            httpClient = webAppFactory.CreateClient(FakeServerAddress);

            spotifyClient = new SpotifyClient(httpClient);

            FakeDataRetrievalSourceFactory = () => new FakeDataRetrievalSource(spotifyClient, FakeServerAddress);
        }

        protected Func<FakeDataRetrievalSource> FakeDataRetrievalSourceFactory { get; private set; }

        protected void RegisterMusicHistory(List<PlayHistoryItem> playHistory)
        {
            using var context = new DataRetrievalContext(contextOptions);
            context.PlayHistoryItems.RemoveRange(context.PlayHistoryItems);
            context.PlayHistoryItems.AddRange(playHistory);
            context.SaveChanges();
        }
    }
}
