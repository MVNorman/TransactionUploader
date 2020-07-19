using System.Collections.Generic;
using System.Linq;

namespace TransactionUploader.Common
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
        public bool HasErrors => Errors.Any();
    }
}
