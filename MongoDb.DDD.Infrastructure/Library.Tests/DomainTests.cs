using NUnit.Framework;
using Test.DAL;
using Test.Domain;

namespace Infrastucture.Tests
{
    public class DomainTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var p = new Price(3);
            var p1 = new Price(3);
            Assert.AreEqual(p, p1);
        }
    }
}