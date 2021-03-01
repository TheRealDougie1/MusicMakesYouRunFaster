﻿namespace RD.CanMusicMakeYouRunFaster.Rest.DataRetrievalSources
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using RD.CanMusicMakeYouRunFaster.Rest.Entity;
    using System.Threading.Tasks;
    using SpotifyAPI.Web;
    using System.Web;
    using System.Collections.Generic;

    /// <summary>
    /// Fake data source used for tests.
    /// </summary>
    public class FakeDataRetrievalSource : IDataRetrievalSource
    {
        private readonly FakeResponseServer.Controllers.ExternalAPICaller externalAPICaller;
        private readonly Uri fakeSpotifyAuthUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDataRetrievalSource"/> class.
        /// </summary>
        /// <param name="externalAPICaller"> HTTP encapsulated Client. </param>
        /// <param name="fakeServerUrl"> Fake server url to call.</param>
        public FakeDataRetrievalSource(FakeResponseServer.Controllers.ExternalAPICaller externalAPICaller,  string fakeServerUrl)
        {
            this.externalAPICaller = externalAPICaller;
            this.fakeSpotifyAuthUrl = new Uri(fakeServerUrl);
        }

        /// <inheritdoc/>
        public async Task<JsonResult> GetSpotifyAuthenticationToken()
        {
            var (challenge, verifier) = PKCEUtil.GenerateCodes();
            await Task.Delay(1);
            FakeResponseServer.DTO.Request.PKCETokenRequest tokenRequest = new FakeResponseServer.DTO.Request.PKCETokenRequest
            {
                ClientId = "Some client id",
                Code = "200",
                CodeVerifier = verifier,
                RedirectUri = new Uri("http://localhost:2000/callback/")
            };
            var builder = new UriBuilder(fakeSpotifyAuthUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["ClientId"] = tokenRequest.ClientId;
            query["Code"] = tokenRequest.Code;
            query["CodeVerifier"] = tokenRequest.CodeVerifier;
            query["RedirectUri"] = tokenRequest.RedirectUri.ToString();
            builder.Query = query.ToString();
            var authTokenResponse = externalAPICaller.Get<SpotifyAuthenticationToken>(new Uri("http://localhost:2222/authorize?ClientId=Some+client+id&Code=200&CodeVerifier=KmS_2IQWRizX1bXF5G508LjlbdO2P9432WFf7gKEfD4&RedirectUri=http%3a%2f%2flocalhost%3a2000%2fcallback%2f"));
            return new JsonResult(authTokenResponse);
        }

        /// <inheritdoc/>
        public async Task<JsonResult> GetStravaAuthenticationToken()
        {
            await Task.Delay(1);
            var authTokenResponse = externalAPICaller.Get<StravaAuthenticationToken>(new Uri("http://localhost:2222/stravaAuthorize?"));
            return new JsonResult(authTokenResponse);
        }

        /// <inheritdoc/>
        public async Task<JsonResult> GetSpotifyRecentlyPlayed(SpotifyAuthenticationToken authToken)
        {
            await Task.Delay(1);
            var musicHistory = externalAPICaller.Get<CursorPaging<FakeResponseServer.DTO.PlayHistoryItem>>(new Uri("http://localhost:2222/v1/me/player/recently-played"));
            var correctMusicHistory = new CursorPaging<PlayHistoryItem>
            {
                Items = new List<PlayHistoryItem>()
            };
            foreach (var item in musicHistory.Items)
            {
                correctMusicHistory.Items.Add(new PlayHistoryItem
                {
                    Context = new Context
                    {
                        ExternalUrls = new Dictionary<string, string>(),
                        Href = item.Context.Href,
                        Type = item.Context.Type,
                        Uri = item.Context.Uri
                    },
                    PlayedAt = (DateTime)item.PlayedAt,
                    Track = new SimpleTrack
                    {
                        Artists = new List<SimpleArtist>(),
                        AvailableMarkets = new List<string>(),
                        DiscNumber = item.Track.DiscNumber,
                        DurationMs = item.Track.DurationMs,
                        Explicit = item.Track.Explicit,
                        ExternalUrls = new Dictionary<string, string>(),
                        Href = item.Track.Href,
                        Id = item.Track.Id,
                        IsPlayable = item.Track.IsPlayable,
                        LinkedFrom = new LinkedTrack
                        {
                            ExternalUrls = new Dictionary<string, string>(),
                            Href = item.Track.LinkedFrom.Href,
                            Id = item.Track.LinkedFrom.Id,
                            Type = item.Track.LinkedFrom.Type,
                            Uri = item.Track.LinkedFrom.Uri,
                        },
                        Name = item.Track.Name,
                        PreviewUrl = item.Track.PreviewUrl,
                        TrackNumber = item.Track.TrackNumber,
                        Type = ItemType.Track,
                        Uri = item.Track.Uri
                    }
                });;
            }
            return new JsonResult(correctMusicHistory);
        }

        /// <inheritdoc/>
        public async Task<JsonResult> GetStravaActivityHistory(StravaAuthenticationToken authToken)
        {
            await Task.Delay(1);
            return new JsonResult("");
        }
    }
}
