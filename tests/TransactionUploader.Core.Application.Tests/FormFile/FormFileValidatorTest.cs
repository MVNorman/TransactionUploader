using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using TransactionUploader.Application.FormFile;
using TransactionUploader.Application.FormFile.Contracts;

namespace TransactionUploader.Core.Application.Tests.FormFile
{
    [TestFixture]
    public class FormFileValidatorTest
    {
        private IFormFileValidator _validator;
        private Mock<IFormFile> _formFileMock;

        [SetUp]
        public void SetUp()
        {
            _formFileMock = new Mock<IFormFile>();

            _validator = new FormFileValidator();
        }

        [Test]
        [TestCase("text/csv")]
        [TestCase("text/xml")]
        [TestCase("application/xml")]
        [TestCase("application/vnd.ms-excel")]
        public void Test_Validation_FormFile_With_Supported_ContentType(string contentType)
        {
            // Arrange
            _formFileMock.Setup(x => x.Length).Returns(1024);
            _formFileMock.Setup(x => x.ContentType).Returns(contentType);

            // Act
            var validationResult = _validator.Validate(_formFileMock.Object);

            // Assert
            Assert.AreEqual(false, validationResult.HasErrors);
        }


        [Test]
        [TestCase("application/soap+xml")]
        [TestCase("application/pdf")]
        public void Test_Validation_FormFile_With_Not_Supported_ContentType(string contentType)
        {
            _formFileMock.Setup(x => x.Length).Returns(1024);
            _formFileMock.Setup(x => x.ContentType).Returns(contentType);

            // Act
            var validationResult = _validator.Validate(_formFileMock.Object);

            // Assert
            Assert.AreEqual(true, validationResult.HasErrors);
        }

        [Test]
        public void Test_Validation_FormFile_With_InValid_Size()
        {
            var fileSizeInBytes = 2000000;

            _formFileMock.Setup(x => x.Length).Returns(fileSizeInBytes);
            _formFileMock.Setup(x => x.ContentType).Returns("text/csv");

            // Act
            var validationResult = _validator.Validate(_formFileMock.Object);

            // Assert
            Assert.AreEqual(true, validationResult.HasErrors);
        }
    }
}
