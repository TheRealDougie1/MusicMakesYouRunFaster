﻿namespace RD.CanMusicMakeYouRunFaster.FakeResponseServer.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Converts context entities / models into Data Transfer Objects.
    /// </summary>
    public static class ToDTOConverter
    {
        /// <summary>
        /// Converts context PlayHistoryItems to DTO.
        /// </summary>
        /// <param name="playHistoryItem"></param>
        /// <returns> DTO of PlayHistoryItem.</returns>
        public static DTO.PlayHistoryItem ToDTO(this Spotify.PlayHistoryItem playHistoryItem)
        {
            return playHistoryItem != null ? new DTO.PlayHistoryItem
            {
                Context = new DTO.Context
                {
                    ExternalUrls = playHistoryItem.Context.ExternalUrls,
                    Href = playHistoryItem.Context.Href,
                    Type = playHistoryItem.Context.Type,
                    Uri = playHistoryItem.Context.Uri
                },
                PlayedAt = playHistoryItem.PlayedAt,
                Track = new DTO.SimpleTrack 
                { 
                    Artists = ConvertArtsits(playHistoryItem.Track.Artists),
                    AvailableMarkets = playHistoryItem.Track.AvailableMarkets,
                    DiscNumber = playHistoryItem.Track.DiscNumber,
                    DurationMs = playHistoryItem.Track.DurationMs,
                    Explicit = playHistoryItem.Track.Explicit,
                    ExternalUrls = playHistoryItem.Track.ExternalUrls,
                    Href = playHistoryItem.Track.Href,
                    Id = playHistoryItem.Track.Id,
                    IsPlayable = playHistoryItem.Track.IsPlayable,
                    LinkedFrom = new DTO.LinkedTrack
                    {
                        ExternalUrls = playHistoryItem.Track.LinkedFrom.ExternalUrls,
                        Href = playHistoryItem.Track.LinkedFrom.Href,
                        Id = playHistoryItem.Track.LinkedFrom.Id,
                        Type = playHistoryItem.Track.LinkedFrom.Type,
                        Uri = playHistoryItem.Track.LinkedFrom.Uri
                    },
                    Name = playHistoryItem.Track.Name,
                    PreviewUrl = playHistoryItem.Track.PreviewUrl,
                    TrackNumber = playHistoryItem.Track.TrackNumber,
                    Type = playHistoryItem.Track.Type,
                    Uri = playHistoryItem.Track.Uri
                }

            } : null;
        }

        public static DTO.Activity ToDTO(this Strava.Activity activity)
        {
            return activity != null ? new DTO.Activity
            {
                achievement_count = activity.achievement_count,
                athlete = new DTO.Athlete
                {
                    badge_type_id = activity.athlete.badge_type_id,
                    city = activity.athlete.city,
                    country = activity.athlete.country,
                    created_at = activity.athlete.created_at,
                    firstname = activity.athlete.firstname,
                    follower = activity.athlete.follower,
                    friend = activity.athlete.friend,
                    id = activity.athlete.id,
                    lastname = activity.athlete.lastname,
                    premium = activity.athlete.premium,
                    profile = activity.athlete.profile,
                    profile_medium = activity.athlete.profile_medium,
                    resource_state = activity.athlete.resource_state,
                    sex = activity.athlete.sex,
                    state = activity.athlete.state,
                    summit = activity.athlete.summit,
                    updated_at = activity.athlete.updated_at,
                    username = activity.athlete.username
                },
                athlete_count = activity.athlete_count,
                average_cadence = activity.average_cadence,
                average_heartrate = activity.average_heartrate,
                average_speed = activity.average_speed,
                average_temp = activity.average_temp,
                comment_count = activity.comment_count,
                commute = activity.commute,
                display_hide_heartrate_option = activity.display_hide_heartrate_option,
                distance = activity.distance,
                elapsed_time = activity.elapsed_time,
                elev_high = activity.elev_high,
                elev_low = activity.elev_low,
                end_latlng = activity.end_latlng,
                external_id = activity.external_id,
                flagged = activity.flagged,
                from_accepted_tag = activity.from_accepted_tag,
                gear_id = activity.gear_id,
                has_heartrate = activity.has_heartrate,
                has_kudoed = activity.has_kudoed,
                heartrate_opt_out = activity.heartrate_opt_out,
                id = activity.id,
                kudos_count = activity.kudos_count,
                location_city = activity.location_city,
                location_country = activity.location_country,
                location_state = activity.location_state,
                manual = activity.manual,
                map = new DTO.Map
                {
                    id = activity.map.id,
                    resource_state = activity.map.resource_state,
                    summary_polyline = activity.map.summary_polyline
                },
                max_heartrate = activity.max_heartrate,
                max_speed = activity.max_speed,
                moving_time = activity.moving_time,
                name = activity.name,
                photo_count = activity.photo_count,
                Private = activity.Private,
                pr_count = activity.pr_count,
                resource_state = activity.resource_state,
                start_date = activity.start_date,
                start_date_local = activity.start_date_local,
                start_latitude = activity.start_latitude,
                start_latlng = activity.start_latlng,
                start_longitude = activity.start_longitude,
                timezone = activity.timezone,
                total_elevation_gain = activity.total_elevation_gain,
                total_photo_count = activity.total_photo_count,
                trainer = activity.trainer,
                type = activity.type,
                upload_id = activity.upload_id,
                upload_id_str = activity.upload_id_str,
                utc_offset = activity.utc_offset,
                visibility = activity.visibility,
                workout_type = activity.workout_type,
            } : null;
        }

        private static List<DTO.SimpleArtist> ConvertArtsits(List<Spotify.SimpleArtist> listOfModels)
        {
            if (listOfModels == null)
            {
                return null;
            }

            List<DTO.SimpleArtist> listOfDTOArtists = new List<DTO.SimpleArtist>();
            foreach (var model in listOfModels)
            {
                listOfDTOArtists.Add(new DTO.SimpleArtist
                {
                    ExternalUrls = model.ExternalUrls,
                    Href = model.Href,
                    Id = model.Id,
                    Name = model.Name,
                    Type = model.Type,
                    Uri = model.Uri
                });
            }

            return listOfDTOArtists;
        }
    }
}
