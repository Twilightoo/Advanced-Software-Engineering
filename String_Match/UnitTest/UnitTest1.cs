using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringMatchLibrary;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string value = "12345";
            string pattern1 = "12345";
            string pattern2 = "12*345";
            string pattern3 = "12+5";
            string pattern4 = "12?45";
            string pattern5 = "12\\?45";
            string pattern6 = "a*******b";
            string pattern7 = "aaabbbababaaaaabbbbbbbaaaabb";
            StringMatch match = new StringMatch();
            Assert.AreEqual(match.Match(pattern1, value), true);
            Assert.AreEqual(match.Match(pattern2, value), true);
            Assert.AreEqual(match.Match(pattern3, value), true);
            Assert.AreEqual(match.Match(pattern4, value), true);
            Assert.AreEqual(match.Match(pattern5, value), false);
            Assert.AreEqual(match.Match(pattern6, pattern7), true);
        }
    }
}
