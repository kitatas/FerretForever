using System;

namespace Ferret.Common.Utility
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
    }
}