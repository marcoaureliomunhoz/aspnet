namespace BookShop.Domain.Common
{
    public static class IntegerExtensions
    {
        public static bool isGraterThanZero(this int value) => value > 0;
        public static bool isGraterThanOrEqualToZero(this int value) => value >= 0;
        public static bool isLessThanZero(this int value) => value < 0;
        public static bool isLessThanOrEqualToZero(this int value) => value <= 0;
    }
}