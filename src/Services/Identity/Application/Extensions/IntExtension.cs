namespace Identity.Application.Extensions
{
    public static class IntExtension
    {
        // Return true if int value is greater than or equal to 1
        public static bool IsId(this int number)
        {
            return number > 0;
        }

        // Return true if int value is greater than or equal to 1
        public static bool IsId(this int? number)
        {
            if (number == null)
                return false;
            return number > 0;
        }

        // Return true if int value in null or zero.
        public static bool IsZeroOrNull(this int? number)
        {
            if (number == null)
                return true;
            return number == 0;
        }
    }
}