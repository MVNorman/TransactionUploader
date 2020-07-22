using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactionUploader.Common
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }
        public bool HasErrors => Errors.Any();

        public override string ToString()
        {
            if (!HasErrors) 
                return base.ToString();

            var stringBuilder = new StringBuilder();
            Errors.ForEach(error =>
            {
                stringBuilder.Append(error);
            });

            return stringBuilder.ToString();
        }
    }
}
