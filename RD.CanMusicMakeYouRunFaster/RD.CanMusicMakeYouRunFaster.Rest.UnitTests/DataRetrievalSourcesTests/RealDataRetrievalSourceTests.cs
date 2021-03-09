﻿namespace RD.CanMusicMakeYouRunFaster.Rest.UnitTests.DataRetrievalSourcesTests
{
    using DataRetrievalSources;
    using FluentAssertions;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using RD.CanMusicMakeYouRunFaster.Rest.Entity;
    using SpotifyAPI.Web;
    using System;
    using System.Collections.Generic;

    public class RealDataRetrievalSourceTests
    {
        private SpotifyAuthenticationToken spotifyAuthenticationToken;
        private StravaAuthenticationToken stravaAuthenticationToken;
        private IDataRetrievalSource sut;

        [OneTimeSetUp]
        public void SetUpTests()
        {
            sut = new RealDataRetrievalSource();
            // Get spotify auth token
            var spotifyTokenAsJson = sut.GetSpotifyAuthenticationToken();
            spotifyTokenAsJson.Result.Should().NotBeNull();
            spotifyTokenAsJson.Result.Value.Should().NotBe(string.Empty);
            spotifyAuthenticationToken = JsonConvert.DeserializeObject<SpotifyAuthenticationToken>((string)spotifyTokenAsJson.Result.Value);
            spotifyAuthenticationToken.AccessToken.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GetSpotifyListeningHistory_ListeningHistoryRetrieved()
        {
            // Get listening history
            var listeningHistory = sut.GetSpotifyRecentlyPlayed(spotifyAuthenticationToken);
            listeningHistory.Result.Value.Should().NotBeNull();
            listeningHistory.Result.Value.Should().NotBe(string.Empty);
        }

        [Test]
        public void GetSpotifyListeningHistoryWithAfterParameter_CorrectListeningHistoryReturned()
        { 
            // Get listening history with after parameter
            var listeningHistory0Task = sut.GetSpotifyRecentlyPlayed(spotifyAuthenticationToken);
            var listeningHistory0Json = JsonConvert.SerializeObject(listeningHistory0Task.Result.Value);
            var listeningHistory0 = JsonConvert.DeserializeObject<CursorPaging<PlayHistoryItem>>(listeningHistory0Json);
            listeningHistory0.Items.Should().NotBeEmpty().And.Should().NotBeNull();

            var ticks = DateTime.UtcNow.AddDays(-7).Ticks;
            var listeningHistory1 = sut.GetSpotifyRecentlyPlayed(spotifyAuthenticationToken, ticks);
            listeningHistory1.Result.Value.Should().NotBeNull();
            listeningHistory1.Result.Value.Should().NotBe(string.Empty);
            listeningHistory1.Result.Value.Should().NotBeEquivalentTo(listeningHistory0.Result.Value);
        }

        [Test]
        public void GetStravaRecentActivities_RunningActivitiesRetrieved()
        {
            // Get Strava Token
            var stravaTokenAsJson = sut.GetStravaAuthenticationToken();
            stravaTokenAsJson.Result.Should().NotBeNull();
            stravaTokenAsJson.Result.Value.Should().NotBe(string.Empty);
            stravaAuthenticationToken = new StravaAuthenticationToken
            {
                access_token = (string)stravaTokenAsJson.Result.Value
            };
            stravaAuthenticationToken.access_token.Should().NotBeNullOrEmpty();

            // Get activities
            var runningHistory = sut.GetStravaActivityHistory(stravaAuthenticationToken);
            runningHistory.Result.Value.Should().NotBeNull();
            runningHistory.Result.Value.Should().NotBe(string.Empty);
            var retrievedActivites = JsonConvert.DeserializeObject<List<Activity>>((string)runningHistory.Result.Value);
            retrievedActivites.Should().NotBeEmpty().And.Should().NotBeNull();
        }
    }
}
