namespace FA.JustBlog.Common
{
    public static class ConstantSystem
    {
        public const string AdminRoleId = "4D921CC6-5349-46FB-B62C-EBF293B217A5";
        public const string UserRoleId = "D0C7A5D9-9F7B-4B7B-9C6F-8D6F6F6F6F6F";
        public const string UserId = "F0C7A5D9-9F7B-4B7B-9C6F-8D6F6F6F6F6F";
        public const string AdminId = "4D921CC6-5349-46FB-B62C-EBF293B217A5";
        public const string EmailAdmin = "admin@gmail.com";
        public const string RoleAdminName = "Admin";
        public const string RoleUserName = "User";
        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else
            {
                return c == 'ĵ' ? "j" : "";
            }
        }


    }
}
