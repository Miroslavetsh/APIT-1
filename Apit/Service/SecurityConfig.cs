namespace Apit.Service
{
    public class SecurityConfig
    {
        public LockoutConfig Lockout { get; set; }
        public PasswordConfig Password { get; set; }
        public UserConfig User { get; set; }
        public SignInConfig SignIn { get; set; }


        public class LockoutConfig
        {
            public int LockoutTimeSpan { get; set; }
            public int MaxFailedAccessAttempts { get; set; }
            public bool AllowedForNewUsers { get; set; }
        }

        public class PasswordConfig
        {
            public int RequiredLength { get; set; }
            public int RequiredUniqueChars { get; set; }

            public bool RequireNonAlphanumeric { get; set; }
            public bool RequireLowercase { get; set; }
            public bool RequireUppercase { get; set; }
            public bool RequireDigit { get; set; }
            
            public string HasherCompatibilityMode { get; set; }
            public int HasherIterationCount { get; set; }

        }

        public class UserConfig
        {
            public string CookieName { get; set; }
            public int ExpireTimeSpanMinutes { get; set; }

            public bool SlidingExpiration { get; set; }
            public bool RequireUniqueEmail { get; set; }
        }

        public class SignInConfig
        {
            public bool RequireConfirmedPhoneNumber { get; set; }
            public bool RequireConfirmedAccount { get; set; }
        }
    }
}