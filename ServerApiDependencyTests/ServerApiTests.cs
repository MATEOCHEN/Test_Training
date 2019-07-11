using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApiDependency;
using ServerApiDependency.Enums;
using ServerApiDependency.Utility.CustomException;
using System;
using System.Net;
using NSubstitute.Routing.Handlers;
using ServerApiDependency.Enum;

namespace ServerApiDependencyTests
{
    public class FakeRecorder : IRecorder
    {
        public void RecordError(string message)
        {
            //DO NOTHING
        }
    }

    public class FakeServerApi : ServerApi
    {
        public FakeServerApi()
        {
            Recorder = new FakeRecorder();
        }

        public static int SaveFailRequestToDbCalledCount { get; set; }

        internal override int PostToThirdParty(ApiType apiType, string apiPage)
        {
            throw new WebException();
        }

        internal override void SaveFailRequestToDb(ApiType ApiType, string ApiPage)
        {
            SaveFailRequestToDbCalledCount++;
        }
    }

    public class FakeServerApi1 : ServerApi
    {
        private readonly int _returnNumber;

        public FakeServerApi1(int number)
        {
            _returnNumber = number;
            Recorder = new FakeRecorder();
        }

        internal override int PostToThirdParty(ApiType apiType, string apiPage)
        {
            return _returnNumber;
        }
    }

    [TestClass()]
    public class ServerApiTests
    {
        private ServerApi _api;

        /// <summary>
        /// LV 3, verify specific method be called
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_exception_test()
        {
            // Assert SaveFailRequestToDb() be called once
            _api = new FakeServerApi();
            Action action = () => _api.CancelGame();
            action.Should().Throw<WebException>();
            Assert.AreEqual(1, FakeServerApi.SaveFailRequestToDbCalledCount);
        }

        /// <summary>
        /// LV 2, verify specific exception be thrown
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_fail_test()
        {
            // Assert PostToThirdParty() return not correct should throw AuthFailException
            _api = new FakeServerApi1(99);
            Assert.ThrowsException<AuthFailException>(() => _api.CancelGame());
        }

        /// <summary>
        /// LV 1, verify api correct response
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_success_test()
        {
            // Assert success
            _api = new FakeServerApi1(0);
            Assert.AreEqual(ServerResponse.Correct, _api.CancelGame());
        }
    }
}