using System;

namespace Ferret.Common.Data.DataStore
{
    public sealed class LanguageJsonData
    {
        public LanguageType type;
        public BootScene boot;
        public MainScene main;
        public ResultScene result;
        public CommonError error;
    }

    [Serializable]
    public sealed class BootScene
    {
        public string nameRegisterTitle;
        public string placeholder;
        public string caption1;
        public string caption2;
    }

    [Serializable]
    public sealed class MainScene
    {
        public OptionScreen option;
        public UpdateScreen update;
        public AchievementScreen achievement;
        public InformationScreen information;
    }

    [Serializable]
    public sealed class OptionScreen
    {
        public string title;
        public string userName;
        public string placeHolder;
        public string language;
        public string soundVolume;
    }

    [Serializable]
    public sealed class UpdateScreen
    {
        public string updating;
        public string success;
        public string failed;
    }

    [Serializable]
    public sealed class AchievementScreen
    {
        public string title;
        public string playCount;
        public string highScore;
        public string totalScore;
        public string totalVictimCount;
    }

    [Serializable]
    public sealed class InformationScreen
    {
        public string title;
        public string credit;
        public string license;
        public string policy;
    }

    [Serializable]
    public sealed class ResultScene
    {
        public string title;
        public string ranking;
        public string highScore;
        public string score;
        public string tweet;
    }

    [Serializable]
    public sealed class CommonError
    {
        public string registration;
        public string connection;
        public string occurred;
    }
}