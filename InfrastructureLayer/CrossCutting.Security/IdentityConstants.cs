using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Security
{
    public static class IdentityConstants
    {
        public class Scopes
        {
            public const string ApiAuthentication = "api_authentication";
            public const string ApiFourFriends = "api_fourfriends";
            public const string ResourceFourFriends = "rc_fourfriends";
            public const string ApiJp = "jp_api.is4";

            /// <summary>
            /// OPTIONAL. This scope value requests access to the End-User's default profile
            ///  Claims, which are: name, nickname, preferred_username, profile, picture, gender, birthdate
            /// </summary>
            public const string ResourceProfile = "rc_profile";
        }

        public class Claims
        {
            public const string FourFriendsPremium = "fourfriends_premium";
            public const string FourFriendsUserId = "fourfriends_userid";
        }

        public class Clients
        {

        }

    }
}
