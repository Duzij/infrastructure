using Library.Domain;
using NUnit.Framework;

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
            Assert.That(Id == Id2);
            Assert.That(Id != Id2);

            Assert.That(Id == null);
            Assert.That(Id != null);

            Id = null;
            Assert.That(Id == null);
        }

        [Test]
        public void ComplareAmounts()
        {
            var bookAmount = new BookAmount(1);
            var bookAmount2 = new BookAmount(1);
            Assert.That(bookAmount == bookAmount2);
        }
    }
}
