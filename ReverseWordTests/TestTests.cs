using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReverseWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseWord.Tests
{
    [TestClass()]
    public class TestTests
    {
        [TestMethod()]
        public void input_empty_should_return_zeroOne()
        {
            //arrange
            var test = new Test();
            //action
            var actual = test.AppdendZeroOne("1");
            //assert
            Assert.AreEqual("101", actual);
        }
    }
}