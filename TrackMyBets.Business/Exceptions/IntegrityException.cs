using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMyBets.Business.Exceptions
{
    public class IntegrityException : BusinessException
    {
        internal IntegrityException(string message)
            : base(message) {

        }

        internal IntegrityException(string message, Exception ex)
            : base(message, ex) {

        }
    }

    public class DuplicatedSportException : IntegrityException
    {
        internal DuplicatedSportException(string data)
            : base(string.Format(Descriptions.DuplicateSport, data)) {

        }
    }

    public class NotFoundSportException : IntegrityException
    {
        internal NotFoundSportException(string data)
            : base(string.Format(Descriptions.NotFoundSport, data))
        {

        }
    }

    public class DuplicatedTeamException : IntegrityException
    {
        internal DuplicatedTeamException(string data)
            : base(string.Format(Descriptions.DuplicateTeam, data))
        {

        }
    }

    public class NotFoundTeamException : IntegrityException
    {
        internal NotFoundTeamException(string data)
            : base(string.Format(Descriptions.NotFoundTeam, data))
        {

        }
    }
}
