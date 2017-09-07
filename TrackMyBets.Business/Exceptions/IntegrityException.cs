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

    public class DuplicatedBookmakerException : IntegrityException
    {
        internal DuplicatedBookmakerException(string data)
            : base(string.Format(Descriptions.DuplicateSport, data))
        {

        }
    }

    public class NotFoundBookmakerException : IntegrityException
    {
        internal NotFoundBookmakerException(string data)
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

    public class DuplicatedEventException : IntegrityException
    {
        internal DuplicatedEventException(string data)
            : base(string.Format(Descriptions.DuplicateTeam, data))
        {

        }
    }

    public class NotFoundEventException : IntegrityException
    {
        internal NotFoundEventException(string data)
            : base(string.Format(Descriptions.NotFoundTeam, data))
        {

        }
    }

    public class NotFoundPickException : IntegrityException
    {
        internal NotFoundPickException(string data)
            : base(string.Format(Descriptions.NotFoundPick, data))
        {

        }
    }

    public class DuplicatedUserException : IntegrityException
    {
        internal DuplicatedUserException(string data)
            : base(string.Format(Descriptions.DuplicateTeam, data))
        {

        }
    }

    public class NotFoundUserException : IntegrityException
    {
        internal NotFoundUserException(string data)
            : base(string.Format(Descriptions.NotFoundTeam, data))
        {

        }
    }

    public class DuplicatedRelUserBookmakerException : IntegrityException
    {
        internal DuplicatedRelUserBookmakerException(string data)
            : base(string.Format(Descriptions.DuplicateTeam, data))
        {

        }
    }

    public class NotFoundRelUserBookmakerException : IntegrityException
    {
        internal NotFoundRelUserBookmakerException(string data)
            : base(string.Format(Descriptions.NotFoundTeam, data))
        {

        }
    }
}
