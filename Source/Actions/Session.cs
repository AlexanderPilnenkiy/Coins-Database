﻿namespace Coins_Database.Actions
{
    static class Session
    {
        public static ACCESS Access { get; set; }

        public static string Login { get; set; }
        public enum ACCESS
        {
            Teacher = 1,
            Superadmin = 0
        }
    }
}
