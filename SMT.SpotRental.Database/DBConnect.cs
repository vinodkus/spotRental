using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;

namespace SMT.SpotRental.Database
{
    public class DBConnect : IDisposable
    {

        #region INSTANCE DATA MEMBERS
        private bool _disposed;
        private SqlCommand _cmd;
        private SqlConnection _con;
        private SqlTransaction _tx;

        private int _commandTimeout;
        private bool _isCommandExecuted;
        private string _connectionString;

        public DBConnect()
        {
            _cmd = null;
            _con = null;
            _tx = null;
            _isCommandExecuted = false;
            _commandTimeout = 240;
            _connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        }
        #endregion

        #region  CONNECTION HELPERS METHODS
        private void CreateConnection()
        {
            if (_con == null)
            {
                _isCommandExecuted = false;
                _con = new System.Data.SqlClient.SqlConnection();
                _con.ConnectionString = this._connectionString;
            }
        }

        private bool isConnectionValid()
        {
            bool functionReturnValue = false;
            functionReturnValue = true;

            if (_con == null)
            {
                functionReturnValue = false;
                return functionReturnValue;
            }

            if (_con.ConnectionString.Length == 0)
            {
                functionReturnValue = false;
            }
            return functionReturnValue;
        }

        private void ConnectionOpen()
        {
            if (!isConnectionValid())
            {
                throw new NoConnectionException();
            }

            if (_con.State == ConnectionState.Open)
            {
                return;
            }

            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
            }

            if (_con.State != ConnectionState.Open)
            {
                throw new ConnectionOpenFailedException();
            }
        }

        private void ConnectionClose()
        {
            ConnectionClose(false);
        }


        private void ConnectionClose(bool ignoreTransaction)
        {

            if (_con == null)
            {
                return;
            }

            if (_con.State != ConnectionState.Closed)
            {
                _con.Close();
            }
        }

        #endregion

        #region COMMAND HELPERS
        private void CreateCommand()
        {
            if (_isCommandExecuted)
            {
                throw new CommandNotResetException();
            }

            if (_cmd == null)
            {
                _cmd = new System.Data.SqlClient.SqlCommand();
                _cmd.Connection = _con;
                if ((_tx != null))
                {
                    _cmd.Transaction = _tx;
                }
            }
        }

        private void CommandCleanup()
        {
            _cmd.Connection = null;
            _isCommandExecuted = true;
        }

        private void CommandSetup(CommandType cmdType, string cmdText)
        {
            CreateCommand();
            var _with1 = _cmd;
            _with1.Connection = _con;
            _with1.CommandText = cmdText;
            _with1.CommandType = cmdType;
            _with1.CommandTimeout = _commandTimeout;
        }

        public void CommandReset()
        {
            if ((_cmd != null))
            {
                _cmd.Parameters.Clear();
            }
            _cmd = null;
            _isCommandExecuted = false;
        }
        #endregion

        #region TRANSACTION MEETHODS

        /// <summary>
        /// This stored procedure execute stored procedure and return void
        /// </summary>
        /// <param name="SprocName"></param>
        public void ExecuteProc(string SprocName)
        {
            try
            {
                CreateConnection();
                CreateCommand();

                if (SprocName == null || SprocName.Length <= 2)
                {
                    throw new NoCommandTextException();
                }

                ConnectionOpen();
                CommandSetup(CommandType.StoredProcedure, SprocName);

                _cmd.ExecuteNonQuery();

            }
            finally
            {
                CommandCleanup();
                ConnectionClose();
            }
        }

        /// <summary>
        /// This method return generic list of object using dapper
        /// </summary>
        /// <typeparam name="T"> Object name</typeparam>
        /// <param name="SprocName">Name of stored procedure as string</param>
        /// <returns></returns>
        public IList<T> ExecuteProc<T>(string SprocName)
        {
            List<T> resultList = new List<T>();
            try
            {

                CreateConnection();
                ConnectionOpen();

                DefaultTypeMap.MatchNamesWithUnderscores = true;

                resultList = _con.Query<T>(SprocName, commandType: CommandType.StoredProcedure).ToList();
            }
            finally
            {
                ConnectionClose();
            }
            return resultList;
        }

        /// <summary>
        /// This method return generic list of object using dapper with parameter
        /// </summary>
        /// <typeparam name="T"> Object name</typeparam>
        /// <param name="SprocName">Name of stored procedure as string</param>
        /// <param name="param">Name of dynamic param</param>
        /// <returns></returns>
        public IList<T> ExecuteProc<T>(string SprocName, object param)
        {
            List<T> resultList = new List<T>();
            try
            {
                CreateConnection();
                if (string.IsNullOrWhiteSpace(SprocName) && SprocName.Length < 3)
                {
                    throw new NoCommandTextException();
                }

                ConnectionOpen();
                DefaultTypeMap.MatchNamesWithUnderscores = true;

                resultList = _con.Query<T>(SprocName, param, commandType: CommandType.StoredProcedure).ToList();
            }
            finally
            {
                //  CommandCleanup();
                ConnectionClose();
            }
            return resultList;
        }

        /// <summary>
        /// This method return generic object using dapper with parameter
        /// </summary>
        /// <typeparam name="T"> Object name</typeparam>
        /// <param name="SprocName">Name of stored procedure as string</param>
        /// <param name="param">Name of dynamic param</param>
        /// <returns></returns>
        public T ExecuteProc_Object<T>(string SprocName, object param)
        {
            T result = default(T);
            try
            {
                CreateConnection();
                if (string.IsNullOrWhiteSpace(SprocName) && SprocName.Length < 3)
                {
                    throw new NoCommandTextException();
                }

                ConnectionOpen();
                DefaultTypeMap.MatchNamesWithUnderscores = true;

                result = _con.Query<T>(SprocName, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            finally
            {
                //  CommandCleanup();
                ConnectionClose();
            }
            return result;
        }

        /// <summary>
        /// This stored procedure execute stored procedure with required parameter 
        /// </summary>
        /// <param name="SprocName">stored procedure name as string</param>
        /// <param name="dtUserType">list of item as DataTable</param>
        /// <param name="param" >other parameter as Dynamic param</param>
        /// <returns></returns>
        public int ExecuteProc(string SprocName, object param)
        {
            int iRes = 0;
            try
            {
                CreateConnection();
                if (SprocName == null || SprocName.Length <= 2)
                {
                    throw new NoCommandTextException();
                }
                ConnectionOpen();
                iRes = _con.Execute(SprocName, param, commandType: CommandType.StoredProcedure);


            }
            finally
            {
                ConnectionClose();
            }

            return iRes;

        }


        #endregion

       
        /// <summary>
        /// Disposing objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// System.IDisposable.Dispose method implementation.
        /// </summary>
        /// <remarks></remarks> 
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if ((_tx != null))
                    {
                        _tx.Dispose();
                        _tx = null;
                    }

                    if ((_cmd != null))
                    {
                        _cmd.Dispose();
                        _cmd = null;
                    }

                    //If Not Me.IsTransactionActive Then
                    ConnectionClose(true);
                    if ((_con != null))
                    {
                        _con.Dispose();
                        _con = null;
                    }
                    //End If

                    //'Save the expense of the garbage collector calling finalize.
                    //If Not IsNothing(_con) Then
                    //    If _con.State = ConnectionState.Closed Then 'we don't want to suppress finalize if there is an open connection
                    //        GC.SuppressFinalize(Me)
                    //    End If
                    //Else 'no connection open so we should be safe to suppress the finalize method
                    //    GC.SuppressFinalize(Me)
                    //End If
                }
            }
            this._disposed = true;
        }
    }
}
