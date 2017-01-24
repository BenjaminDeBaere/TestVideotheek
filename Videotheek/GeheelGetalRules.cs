using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace Videotheek
{
    public class GeheelGetalRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int getal = 0;
            if(!int.TryParse(value.ToString(), out getal))
            {
                return new ValidationResult(false, "Gelieve een geheel getal in te geven");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
