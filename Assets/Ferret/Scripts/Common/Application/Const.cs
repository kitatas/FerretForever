namespace Ferret.Common
{
    public sealed class MasterConfig
    {
        public const string TITLE_ID = "";
        public const string SAVE_KEY = "";
        public const string USER_KEY = "";
        public const string RANKING_NAME = "";
        public const int SCORE_RATE = 1000;
    }

    public sealed class UiConfig
    {
        public const float POPUP_ANIMATION_TIME = 0.25f;
    }

    public sealed class WebviewConfig
    {
        public const string URL_BASE = "https://kitatas.github.io/FerretForever/";
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
}