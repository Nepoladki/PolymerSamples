﻿namespace PolymerSamples.Authorization
{
    public class AuthData
    {
        public const string AccessTokenName = "jwt";
        public const string RefreshTokenName = "jwt_refresh";

        public const string ViwerPolicyName = "ViwerPolicy";
        public const string AdminPolicyName = "AdminPolicy";
        public const string EditorPolicyName = "EditorPolicy";

        public const string RoleClaimType = "clientRole";
        public const string IdClaimType = "userId";
        public const string UserNameClaimType = "userName";

        public const string AdminClaimValue = "admin";
        public const string ViewerClaimValue = "viewer";
        public const string EditorClaimValue = "editor";
    }
}
