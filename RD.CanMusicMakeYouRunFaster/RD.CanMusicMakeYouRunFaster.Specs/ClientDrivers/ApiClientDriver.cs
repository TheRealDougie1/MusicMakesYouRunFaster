﻿namespace RD.CanMusicMakeYouRunFaster.Specs.ClientDrivers
{
    /// <summary>
    /// Client driver for testing without a front-end.
    /// </summary>
    public class ApiClientDriver : IClientDriver
    {
        /// <inheritdoc/>
        public void GetRequestedData()
        {
            string spotifyOAuth2Token = string.Empty;

            // Get Oauth token and authenticate.
            // then request data.
        }
    }
}
