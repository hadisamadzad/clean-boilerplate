using System;

namespace Identity.Application.Configurations
{
    public class LockoutConfig
    {
        public const string Key = "Lockout";

        public int FailedLoginLimit { get; set; }
        public TimeSpan Duration { get; set; }
    }
}