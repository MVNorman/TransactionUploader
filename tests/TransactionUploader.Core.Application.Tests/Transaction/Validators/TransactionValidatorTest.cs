using System;
using NUnit.Framework;
using TransactionUploader.Application.Transaction;
using TransactionUploader.Application.Transaction.Contracts;

namespace TransactionUploader.Core.Application.Tests.Transaction.Validators
{
    [TestFixture]
    public class TransactionValidatorTest
    {
        private ITransactionValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new TransactionValidator();
        }

        [Test]
        public void Test_Validation_Date_Range_With_Valid_Range()
        {
            // Arrange
            var dateFrom = new DateTime(2019, 1, 1);
            var dateTo = new DateTime(2019, 3, 3);

            var validator = new TransactionValidator();

            // Act
            var actual = _validator.IsDateRangeValid(dateFrom, dateTo);

            // Assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void Test_Validation_Date_Range_With_InValid_Range()
        {
            // Arrange
            var dateFrom = new DateTime(2029, 1, 1);
            var dateTo = new DateTime(2019, 1, 1);

            var validator = new TransactionValidator();

            // Act
            var actual = validator.IsDateRangeValid(dateFrom, dateTo);

            // Assert
            Assert.AreEqual(false, actual);
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("U")]
        [TestCase("US")]
        [TestCase("USD1")]
        public void Test_Validation_Currency_Code_With_InValid_Data(string currencyCode)
        {
            // Arrange
            var validator = new TransactionValidator();

            // Act
            var actual = validator.IsCurrencyCodeValid(currencyCode);

            // Assert
            Assert.AreEqual(false, actual);
        }


        [Test]
        [TestCase("USD")]
        [TestCase("eur")]
        public void Test_Validation_Currency_Code_With_Valid_Data(string currencyCode)
        {
            // Arrange
            var validator = new TransactionValidator();

            // Act
            var actual = validator.IsCurrencyCodeValid(currencyCode);

            // Assert
            Assert.AreEqual(true, actual);
        }
    }
}
