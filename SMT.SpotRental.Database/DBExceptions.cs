using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Database
{
    [Serializable()]
    public class DBExceptions: ApplicationException
    {
        public DBExceptions(string Message) : base(Message)
        {

        }
        public DBExceptions() : base("An Exception occured in KBConnectDAL.DataAccess")
        {

        }
    }
    [Serializable()]
    public class TransactionHeldException : DBExceptions
    {
        public TransactionHeldException() : base("Transaction is held.")
        {
        }
    }

    [Serializable()]
    public class TransactionPendingException : DBExceptions
    {
        public TransactionPendingException() : base("Transaction Pending")
        {
        }
    }

    [Serializable()]
    public class NoTransactionException : DBExceptions
    {
        public NoTransactionException() : base("No Transaction")
        {
        }
    }

    [Serializable()]
    public class NoConnectionException : DBExceptions
    {
        public NoConnectionException() : base("No Connection")
        {
        }
    }

    [Serializable()]
    public class NoCommandTextException : DBExceptions
    {
        public NoCommandTextException() : base("No Command Text")
        {
        }
    }

    [Serializable()]
    public class AddParamFailedException : DBExceptions
    {
        public AddParamFailedException() : base("Add Param Failed")
        {
        }

        public AddParamFailedException(string ParamName) : base("Add Param Failed: " + ParamName)
        {
        }
    }

    [Serializable()]
    public class ParamNotFoundException : DBExceptions
    {
        public ParamNotFoundException() : base("Param not found")
        {
        }
    }

    [Serializable()]
    public class ConnectionOpenFailedException : DBExceptions
    {
        public ConnectionOpenFailedException() : base("Connection Open Failed")
        {
        }
    }

    [Serializable()]
    public class CommandNotResetException : DBExceptions
    {
        public CommandNotResetException() : base("Command Not Reset")
        {
        }
    }

    [Serializable()]
    public class ConnectionNotOpenException : DBExceptions
    {
        public ConnectionNotOpenException() : base("Connection Not Open")
        {
        }
    }
}
