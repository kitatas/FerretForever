namespace Ferret.Common
{
    public enum SceneName
    {
        None,
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
        HighScore   = 1,
        TotalScore  = 2,
        TotalVictim = 3,
    }

    public enum AchievementRank
    {
        None   = 0,
        Normal = 1,
        Bronze = 2,
        Silver = 3,
        Gold   = 4,
    }
}