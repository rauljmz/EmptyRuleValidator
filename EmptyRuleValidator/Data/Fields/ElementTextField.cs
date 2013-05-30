using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmptyRuleValidator.Data.Fields
{
    public class ElementTextField
    {
        public IEnumerable<string> Macros { get; private set; }


        //parses a text field of the type 
        //where the current database name [operatorid,StringOperator,,compares to] [value,,,value]
        public static ElementTextField Parse(string value)
        {
            var regex = new Regex(@"\[([^,]*),[^\]]*\]");            

            return new ElementTextField()
                {                    
                    Macros = string.IsNullOrEmpty(value) ? new List<string>() : regex.Matches(value).Cast<Match>().Select(m => m.Groups[1].Value)
                };
        }
    }
}