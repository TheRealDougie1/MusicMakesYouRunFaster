﻿namespace RD.CanMusicMakeYouRunFaster.Rest.DataRetrievalSources
{
    using System.Threading.Tasks;
    using Entity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Interface for data retrieval sources
    /// </summary>
    public interface IDataRetrievalSource
    {
        /// <summary>
        /// Gets the spotify recently played history.
        /// </summary>
        /// <param name="authToken">Authentication token to use.</param>
        /// <returns> Json of recently played music. </returns>
        Task<JsonResult> GetSpotifyRecentlyPlayed(SpotifyAuthenticationToken authToken);

        /// <summary>
        /// Gets the spotify authentication token
        /// </summary>
        /// <returns> Json of a valid Authentication Token</returns>
        Task<JsonResult> GetSpotifyAuthenticationToken();
    }
}
