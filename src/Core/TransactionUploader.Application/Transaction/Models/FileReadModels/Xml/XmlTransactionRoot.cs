using System.Xml.Serialization;

namespace TransactionUploader.Application.Transaction.Models.FileReadModels.Xml
{
    [XmlRoot("Transactions")]
    public class XmlTransactionRoot
    {
        [XmlElement("Transaction")]
        public XmlTransactionItem[] Transactions { get; set; }
    }
}
