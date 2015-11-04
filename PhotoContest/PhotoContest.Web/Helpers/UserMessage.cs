using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Helpers
{
    public static class UserMessage
    {
        public const string NoUser = "No such user!";
        public const string CannotInviteYourself = "Cannot invite youself!";
        public const string NoContest = "No such contest!";
        public const string UserIsInTheContest = "User is already in the contest!";
        public const string UserIsInTheCommittee = "User is already in the committee!";
    }
}