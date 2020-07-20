using System;
using System.Xml.Serialization;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Models.Xml
{
    [Serializable]
    public class XmlTransactionItem
    {
        [XmlAttribute("id")]
        public string TransactionId { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDate { get; set; }

        [XmlElement("PaymentDetails")]
        public XmlPaymentDetails PaymentDetails { get; set; }

        [XmlElement("Status")]
        public TransactionStatus? Status { get; set; }

        public bool InValid { get; set; }

        public void MarkAsInValid()
        {
            InValid = true;
        }
    }
}
