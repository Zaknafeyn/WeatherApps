using System;
using System.Collections.Generic;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;
using R = Android.Content.Res.Resources;

namespace Weather.AndroidApp
{
    internal static class Extensions
    {
        public static string GetWeatherIconName(this Services.Portable.DTO.Api.WeatherDetails.Weather weatherStatus)
        {
            var dayOrNight = weatherStatus.Icon.EndsWith("d") ? "d" : "n";
            return $"WeatherStatus{weatherStatus.Id}{dayOrNight}";
        }

        public static int GetWeatherIconResourceId(this R resources, Services.Portable.DTO.Api.WeatherDetails.Weather weatherStatus, string packageName)
        {
            var iconName = weatherStatus.GetWeatherIconName();
            var iconId = resources.GetIdentifier(iconName.ToLower(), "drawable", packageName);
            return iconId;
        }

        public static ICharSequence ToCharSequence(this string str)
        {
            return CharSequence.ArrayFromStringArray(new[] { str })[0];
        }

        public static string GetTimeDiffString(this DateTime dateTime)
        {
            var elapsedTime = DateTime.Now.Subtract(dateTime);
            if (elapsedTime.TotalMinutes < 20)
                return "just now";
            if (elapsedTime.TotalMinutes < 60)
                return $"{elapsedTime.TotalMinutes} minutes ago";
            if (elapsedTime.TotalHours < 24)
                return $"{elapsedTime.TotalHours} hours ago";

            return $"{elapsedTime.TotalDays} days ago";
        }

        public static void ShowFragment(this FragmentManager supportFragmentManager, params int[] fragmentIds)
        {
            var fragments = new List<Fragment>();
            foreach (var fragmentId in fragmentIds)
            {
                fragments.Add(supportFragmentManager.FindFragmentById(fragmentId));
            }
            ShowFragment(supportFragmentManager, fragments.ToArray());
        }

        public static void ShowFragment(this FragmentManager supportFragmentManager, params Fragment[] fragments)
        {
            var transaction = supportFragmentManager
                .BeginTransaction()
                .SetCustomAnimations(Resource.Animator.fade_in, Resource.Animator.fade_out);

            foreach (var fragment in fragments)
            {
                transaction = transaction.Show(fragment);
            }

            transaction.CommitAllowingStateLoss();

            supportFragmentManager.ExecutePendingTransactions();
        }

        public static void HideFragment(this FragmentManager supportFragmentManager, params Fragment[] fragments)
        {
            var transaction = supportFragmentManager.BeginTransaction();
            foreach (var fragment in fragments)
            {
                transaction = transaction.Hide(fragment);
            }
            transaction.CommitAllowingStateLoss();
            supportFragmentManager.ExecutePendingTransactions();
        }

        public static void HideFragment(this FragmentManager supportFragmentManager, params int[] fragmentIds)
        {
            var fragments = new List<Fragment>();
            foreach (var fragmentId in fragmentIds)
            {
                fragments.Add(supportFragmentManager.FindFragmentById(fragmentId));
            }

            HideFragment(supportFragmentManager, fragments.ToArray());
        }
    }
}