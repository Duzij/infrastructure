using Library.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Tests
{

    public class CoreTests
    {
        private CounterId Id;
        private CounterId Id2;

        [SetUp]
        public void PrepareIds()
        {
            var guid = Guid.NewGuid().ToString();
            Id = new CounterId(guid);
            Id2 = new CounterId(guid);
        }

        [Test]
        public void CompareIds()
        {
            Assert.IsTrue(Id == Id2);
            Assert.IsFalse(Id != Id2);

            Assert.IsFalse(Id == null);
            Assert.IsTrue(Id != null);

            Id = null;
            Assert.IsTrue(Id == null);
        }

        [Test]
        public void ComplareAmounts()
        {
            var bookAmount = new BookAmount(1);
            var bookAmount2 = new BookAmount(1);
            Assert.IsTrue(bookAmount == bookAmount2);
        }
    }
}
