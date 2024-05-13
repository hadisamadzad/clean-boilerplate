namespace Identity.Domain
{
    public static class Constants
    {
        #region General

        /* DO NOT CHANGE THESE ITEMS TO A LOWER LENGTH */
        public const int MaxLength = 4000;

        public const int Char20Length = 20;
        public const int Char40Length = 40;
        public const int Char60Length = 60;
        public const int Char80Length = 80;
        public const int Char400Length = 400;

        public const int MinPercent = 0;
        public const int MaxPercent = 100;

        #endregion

        #region Constant

        public const int DebitCardNumberLength = 16;
        public const int PostalCodeLength = 10;
        public const int MobileNumberLength = 10;
        public const int TelephoneLength = 10;
        public const int NationalCodeLength = 10;
        public const int IpAddressLength = 15;
        public const int YearLength = 4;

        public const double MinScore = 1.0;
        public const double MaxScore = 5.0;

        #endregion

        #region Business


        public const int TitleLength = 140;
        public const int SubtitleLength = 255;
        public const int ShortDescriptionLength = 800;

        public const int FileSize = 10 * 1024 * 1024;
        public const int FileNameLength = 12;
        public const int FileExtensionLength = 5;

        public const int AddressLength = 1000;
        public const int HouseNoLength = 10;

        public const int EmailPrivateTokenLength = 16;
        public const int SmsTokenLength = 4;
        public const int PasswordHashLength = 128;
        public const int SecurityStampLength = 32;
        public const int GenericHashLength = 128;

        #endregion
    }
}