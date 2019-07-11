using XmasChecker;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmasChecker.Tests
{
    [TestClass()]
    public class XmasCheckerTests
    {
        private readonly FakeChecker _xmasChecker = new FakeChecker();

        [TestMethod()]
        public void Today_is_not_xmas()
        {
            _xmasChecker.SetDate(1992, 12, 24);
            var actual = _xmasChecker.IsTodayXmas();
            var a = new Main();
            var result = a.Method();
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Today_is_xmas()
        {
            _xmasChecker.SetDate(1992, 12, 25);
            var actual = _xmasChecker.IsTodayXmas();
            Assert.AreEqual(true, actual);
        }

        public class FakeChecker : XmasChecker
        {
            public void SetDate(int year, int month, int day)
            {
                today = new DateTime(year, month, day);
            }
        }
    }
}