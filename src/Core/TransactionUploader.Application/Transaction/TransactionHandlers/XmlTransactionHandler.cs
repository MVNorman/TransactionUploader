using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;

namespace TransactionUploader.Application.Transaction.TransactionHandlers
{
    public class XmlTransactionHandler: IXmlTransactionHandler
    {
        private ITransactionHandler _successor;

        public XmlTransactionHandler()
        {
        }

        public TransactionExportResult HandleRequest(IFormFile formFile, FileFormat fileFormat)
        {
            if (fileFormat == FileFormat.Xml)
            {
                return null;
            }

            return _successor?.HandleRequest(formFile, fileFormat);
        }

        public void SetSuccessor(ITransactionHandler successor)
        {
            _successor = successor;
        }
    }
}
