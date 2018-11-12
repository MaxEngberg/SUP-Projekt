using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Inspark.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get{return CrossSettings.Current;}
        }

        private const string UserIdKey = "userid_key";
        private static readonly string UserIdDefault = string.Empty;
        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue(UserIdKey, value); }
        }

        private const string UserRoleKey = "userRole_key";
        private static readonly string UserRoleDefault = string.Empty;
        public static string UserRole
        {
            get { return AppSettings.GetValueOrDefault(UserRoleKey, UserRoleDefault); }
            set { AppSettings.AddOrUpdateValue(UserRoleKey, value); }
        }


        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;
        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue(UserNameKey, value); }
        }
        private const string UserPasswordKey = "userpassword_key";
        private static readonly string UserPasswordDefault = string.Empty;
        public static string UserPassword
        {
            get { return AppSettings.GetValueOrDefault(UserPasswordKey, UserPasswordDefault); }
            set { AppSettings.AddOrUpdateValue(UserPasswordKey, value); }
        }
        
        private const string AccessTokenKey = "AccessToken_key";
        private static readonly string AccessTokenDefault = string.Empty;
        public static string AccessToken
        {
            get { return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault); }
            set { AppSettings.AddOrUpdateValue(AccessTokenKey, value); }
        }

        private const string AccessTokenExpiresKey = "AccessExpiresToken_key";
        private static readonly DateTime AccessTokenExpiresDefault = DateTime.UtcNow;
        public static DateTime AccessTokenExpires
        {
            get { return AppSettings.GetValueOrDefault(AccessTokenExpiresKey, AccessTokenExpiresDefault); }
            set { AppSettings.AddOrUpdateValue(AccessTokenExpiresKey, value); }
        }
    }
}