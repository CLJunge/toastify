﻿using SpotifyAPI;
using SpotifyAPI.Local;
using System.Runtime.CompilerServices;
using Toastify.Core;
using Toastify.Model;
using ToastifyAPI.Native;

namespace Toastify.Helpers
{
    internal static class SpotifyLocalAPIExtensions
    {
        public static void IncrementVolume(this SpotifyLocalAPI api)
        {
            try
            {
                float currentVolume = api.GetSpotifyVolume();
                float newVolume = currentVolume + Settings.Current.WindowsVolumeMixerIncrement;
                api.SetSpotifyVolume(newVolume >= 100.0f ? 100.0f : newVolume);
            }
            catch
            {
                Win32API.SendMediaKey(ToastifyAction.VolumeUp);
            }
        }

        public static void DecrementVolume(this SpotifyLocalAPI api)
        {
            try
            {
                float currentVolume = api.GetSpotifyVolume();
                float newVolume = currentVolume - Settings.Current.WindowsVolumeMixerIncrement;
                api.SetSpotifyVolume(newVolume <= 0.0f ? 0.0f : newVolume);
            }
            catch
            {
                Win32API.SendMediaKey(ToastifyAction.VolumeDown);
            }
        }

        public static void ToggleMute(this SpotifyLocalAPI api)
        {
            try
            {
                if (api.IsSpotifyMuted())
                    api.UnMute();
                else
                    api.Mute();
            }
            catch
            {
                Win32API.SendMediaKey(ToastifyAction.Mute);
            }
        }

        public static string ToString(this ProxyConfig proxyConfig, bool objectHash)
        {
            string @string = proxyConfig?.IsValid() == true
                ? $"{(!string.IsNullOrEmpty(proxyConfig.Username) ? $"{proxyConfig.Username}@" : "")}{proxyConfig.Host}:{proxyConfig.Port}"
                : proxyConfig == null ? "null" : "";

            if (objectHash)
            {
                if (proxyConfig != null)
                    @string += $" @ {RuntimeHelpers.GetHashCode(proxyConfig)}";
                @string = @string.Trim();
            }

            return @string;
        }
    }
}