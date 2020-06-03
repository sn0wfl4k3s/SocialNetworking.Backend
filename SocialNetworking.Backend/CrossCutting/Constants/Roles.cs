namespace CrossCutting.Constants
{
    public static class Roles
    {
        public const string User = "user";
        public const string Admin = "admin";
        public const string AdminOrUser = User + "," + Admin;
    }
}
