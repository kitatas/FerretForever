namespace Ferret.Common
{
    public sealed class GameConfig
    {
        public const string GAME_ID = "FERRET_FOREVER";
        public const string APP_URL = "https://play.google.com/store/apps/details?id=com.KitaLab.FerretForever";
        public const string DEVELOPER_APP_URL = "https://play.google.com/store/apps/developer?id=KitaLab";
    }

    public sealed class MasterConfig
    {
        public const string TITLE_ID = "";
        public const string SAVE_KEY = "";
        public const string USER_KEY = "";
        public const string RANKING_NAME = "";
        public const string ACHIEVEMENT_NAME = "";
        public const int SCORE_RATE = 1000;
        public const int SHOW_MAX_RANK = 100;
    }

    public sealed class UiConfig
    {
        public const float POPUP_ANIMATION_TIME = 0.25f;
        public const float BUTTON_ANIMATION_TIME = 0.1f;
    }

    public sealed class WebviewConfig
    {
        public const string URL_BASE = "https://kitatas.github.io/games/ferret_forever/";
        public const string URL_CREDIT = URL_BASE + "credit";
        public const string URL_LICENSE = URL_BASE + "license";
        public const string URL_POLICY = URL_BASE + "policy";
    }

    public sealed class BgmConfig
    {
        public const string CUE_SHEET_NAME = "BGM";
        public const string CUE_NAME_TITLE = "title";
        public const string CUE_NAME_MAIN = "main";
        public const string CUE_NAME_RESULT = "result";
    }

    public sealed class SeConfig
    {
        public const string CUE_SHEET_NAME = "SE";
        public const string CUE_NAME_BUTTON = "button";
        public const string CUE_NAME_JUMP = "jump";
        public const string CUE_NAME_CRASH = "crash";
        public const string CUE_NAME_SCREAM = "scream";
        public const string CUE_NAME_EXPLODE = "explode";
        public const string CUE_NAME_BUILD = "build";
        public const string CUE_NAME_FALL = "fall";
        public const string CUE_NAME_GROUND = "ground";
    }
}