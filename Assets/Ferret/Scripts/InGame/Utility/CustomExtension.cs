using System;

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
    }
}