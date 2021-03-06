using System;
using Ferret.Common;
using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.InGame
{
    public static class CustomExtension
    {
        public static int ConvertInt(this BalloonType type)
        {
            return type switch
            {
                BalloonType.Five => 5,
                BalloonType.Ten  => 10,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static Color ConvertColor(this EffectColor color)
        {
            return color switch
            {
                EffectColor.White   => Color.white,
                EffectColor.Green   => Color.green,
                EffectColor.Magenta => Color.magenta,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }

        public static string ConvertUrl(this WebviewType type)
        {
            return type switch
            {
                WebviewType.Credit  => WebviewConfig.URL_CREDIT,
                WebviewType.License => WebviewConfig.URL_LICENSE,
                WebviewType.Policy  => WebviewConfig.URL_POLICY,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string ConvertDetail(this AchievementType type, AchievementScreen screen)
        {
            return type switch
            {
                AchievementType.PlayCount   => screen.playCount,
                AchievementType.HighScore   => screen.highScore,
                AchievementType.HighVictim  => screen.highVictimCount,
                AchievementType.TotalScore  => screen.totalScore,
                AchievementType.TotalVictim => screen.totalVictimCount,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}