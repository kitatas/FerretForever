namespace Ferret.Common
{
    public enum SceneName
    {
        None,
        Boot,
        Main,
        Ranking,
    }

    public enum BgmType
    {
        None,
        Title,
        Main,
        Result,
    }

    public enum SeType
    {
        None,
        Button,
        Jump,
        Crash,
        Scream,
        Explode,
        Build,
        Fall,
        Ground, // 橋の着地音
    }

    public enum AchievementType
    {
        None        = 0,
        PlayCount   = 1,
        HighScore   = 2,
        HighVictim  = 3,
        TotalScore  = 4,
        TotalVictim = 5,
    }

    public enum AchievementRank
    {
        None   = 0,
        Normal = 1,
        Bronze = 2,
        Silver = 3,
        Gold   = 4,
    }

    public enum LanguageType
    {
        None     = 0,
        English  = 1,
        Japanese = 2,
    }

    public enum IconColor
    {
        White,
        Gray,
        Brown,
    }
}