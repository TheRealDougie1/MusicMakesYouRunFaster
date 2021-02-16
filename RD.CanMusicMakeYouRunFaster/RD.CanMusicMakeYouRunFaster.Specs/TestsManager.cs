﻿namespace RD.CanMusicMakeYouRunFaster.Specs
{
    using System;
    using System.Threading;
    using BoDi;
    using ClientDrivers;
    using DataSource;
    using FakeResponseServer.DbContext;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Class to set up, tear down and manage specs tests.
    /// </summary>
    [Binding]
    public class TestsManager
    {
        private static IClientDriver clientDriver;
        private static DbContextOptions<DataRetrievalContext> contextOptions;
        private static InMemoryDatabaseRoot databaseRoot;

        /// <summary>
        /// Set up precursors to testing.
        /// </summary>
        /// <param name="objectContainer"> Object container </param>
        [BeforeTestRun]
        public static void TestSetup(IObjectContainer objectContainer)
        {
            databaseRoot = new InMemoryDatabaseRoot();
            contextOptions = new DbContextOptionsBuilder<DataRetrievalContext>()
                .UseInMemoryDatabase("SpecsDatabase", databaseRoot).Options;
            var dataSource = new FakeDataSource(contextOptions);

            clientDriver = new ApiClientDriver();
            clientDriver.SetUp();

            objectContainer.RegisterInstanceAs<IClientDriver>(clientDriver);
            objectContainer.RegisterInstanceAs(dataSource);

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
