using ServerApiDependency.Enum;
using ServerApiDependency.Enums;
using ServerApiDependency.Interface;
using ServerApiDependency.Utility;
using ServerApiDependency.Utility.CustomException;
using System;
using System.Net;
using ServerApiDependency;

namespace ServerApiDependency
{
    public interface IRecorder
    {
        void RecordError(string message);
    }

    public class Recorder : IRecorder
    {
        public virtual void RecordError(string message)
        {
            TiDebugHelper.Error(message);
        }
    }

    public class ServerApi : IServerApi
    {
        public IRecorder Recorder { get; set; } = new Recorder();

        public ServerResponse CancelGame()
        {
            const string apiPage = "cancel.php";
            try
            {
                var response = PostToThirdParty(ApiType.CancelGame, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    RecordError($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }

                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                RecordError($" WebException: {e}");
                SaveFailRequestToDb(ApiType.CancelGame, apiPage);
                throw e;
            }
        }

        public ServerResponse GameResult()
        {
            const string apiPage = "result.php";
            try
            {
                var response = PostToThirdParty(ApiType.GameResult, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    RecordError($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }
                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                RecordError($" WebException: {e}");
                SaveFailRequestToDb(ApiType.GameResult, apiPage);
                throw e;
            }
        }

        public ServerResponse StartGame()
        {
            const string apiPage = "start.php";
            try
            {
                var response = PostToThirdParty(ApiType.StartGame, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    RecordError($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }
                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                RecordError($" WebException: {e}");
                SaveFailRequestToDb(ApiType.StartGame, apiPage);
                throw e;
            }
        }

        /// <summary>
        /// treat this method is a dependency to connect to third-party server
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        /// <param name="apiPage">The API page.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        internal virtual int PostToThirdParty(ApiType apiType, string apiPage)
        {
            // don't implement
            throw new NotImplementedException();
        }

        internal Action SaveFailRequestToDb()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// treat this method as a dependency to connect to db
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        /// <param name="apiPage">The API page.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal virtual void SaveFailRequestToDb(ApiType apiType, string apiPage)
        {
            // don't implement
            throw new NotImplementedException();
        }

        protected virtual void RecordError(string message)
        {
            Recorder.RecordError(message);
        }
    }
}