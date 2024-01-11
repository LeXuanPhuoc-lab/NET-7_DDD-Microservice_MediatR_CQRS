namespace Application.Payloads
{
    public class APIRoutes
    {
        public const string Base = "api";
        
        public static class Identity
        {
            public const string SignIn = Base + "/identity/sign-in";
            public const string SignUp = Base + "/identity/sign-up";
            public const string Refresh = Base + "/identity/refresh";
        }

        public static class Accounts
        {
            public const string GetAll = Base + "/accounts";
        }

        public static class UserManagement
        {
            public const string GetAll = Base + "/users";
            public const string Create = Base + "/users";
        }
        
        public static class Roles
        {
            public const string GetAll = Base + "/roles";
        }
    }
}
