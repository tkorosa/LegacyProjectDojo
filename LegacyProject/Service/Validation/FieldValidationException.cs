using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validation
{
    public class FieldValidationException: Exception
    {
        public string Field { get; }
        public string Error { get; }

        public FieldValidationException(string field, string error)
        {
            Field = field;
            Error = error;
        }
    }
}
