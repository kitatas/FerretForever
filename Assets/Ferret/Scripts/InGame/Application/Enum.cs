namespace Ferret.InGame
{
    public enum GameState
    {
        None,
        Title,
        Main,
        Bridge,
        Finish,
        Result,
    }

    public enum PlayerColor
    {
        None,
        White,
        Gray,
        Brown,
    }

    public enum PlayerStatus
    {
        None,
        Run,
        Jump,
        Jumping,
        Bridge,
        Blow,
    }

    public enum BalloonType
    {
        None,
        Five,
        Ten,
    }

    public enum EnemyType
    {
        None,
        Wolf,
        Hawk,
    }

    public enum EffectType
    {
        None,
        Crash,
        Explode,
    }

    public enum EffectColor
    {
        None,
        White,
        Green,
        Magenta,
    }

    public enum WebviewType
    {
        None,
        Credit,
        License,
        Policy,
    }
}