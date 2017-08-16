using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core.Helper
{
    public class KeywordsAttribute : Attribute
    {
        public string Keywords { get; set; }
        public bool Required { get; set; }
        public bool AllCells { get; set; }
        public KeywordsAttribute(string keywords ,bool required = true,bool allCells= false)
        {
            Keywords = keywords;
            Required = required;
            AllCells = allCells;
        }

       
    }
}
