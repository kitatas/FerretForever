namespace Ferret.InGame
{
    public sealed class InGameConfig
    {
        public const int INIT_PLAYER_COUNT = 5;
        public const int MAX_VICTIM_COUNT = 10;
        public const float CONVERT_BRIDGE_TIME = 0.1f;
        public const float BUILD_BRIDGE_TIME = 1.0f;
    }

    public sealed class TagConfig
    {
        public const string GROUND = "Ground";
    }

    public sealed class SortingLayerConfig
    {
        public const string GROUND = "Ground";
        public const string BRIDGE = "Bridge";
        public const string VICTIM = "Victim";
        public const string CHARA = "Chara";
    }

    public sealed class AchievementConfig
    {
        public const string DETAIL_SECRET = "???";
    }
}