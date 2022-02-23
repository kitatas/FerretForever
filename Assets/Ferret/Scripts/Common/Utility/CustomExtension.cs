using System;

namespace Ferret.Common
{
    public static class CustomExtension
    {
        public static string ConvertCueName(this BgmType type)
        {
            return type switch
            {
                BgmType.Title  => BgmConfig.CUE_NAME_TITLE,
                BgmType.Main   => BgmConfig.CUE_NAME_MAIN,
                BgmType.Result => BgmConfig.CUE_NAME_RESULT,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string ConvertCueName(this SeType type)
        {
            return type switch
            {
                SeType.Button  => SeConfig.CUE_NAME_BUTTON,
                SeType.Jump    => SeConfig.CUE_NAME_JUMP,
                SeType.Crash   => SeConfig.CUE_NAME_CRASH,
                SeType.Scream  => SeConfig.CUE_NAME_SCREAM,
                SeType.Explode => SeConfig.CUE_NAME_EXPLODE,
                SeType.Build   => SeConfig.CUE_NAME_BUILD,
                SeType.Fall    => SeConfig.CUE_NAME_FALL,
                SeType.Ground  => SeConfig.CUE_NAME_GROUND,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string ConvertErrorMessage(this Exception exception)
        {
            if (exception is CustomPlayFabException) return ErrorConfig.CONNECTION;
            return ErrorConfig.DEFAULT_ERROR;
        }
    }
}