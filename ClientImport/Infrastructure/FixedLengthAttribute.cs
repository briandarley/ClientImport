using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Infrastructure
{
    public class FixedLengthAttribute: Attribute
    {
        private readonly int _length;
        public int Start { get; set; }
        public int Length { get; set; }
        public int? DecimalPlaces { get; set; }

        public FixedLengthAttribute(int start, int length)
        {
            Length = length;
            Start = start;
        }
        public FixedLengthAttribute(int start, int length,int decimalPlaces)
        {
            Length = length;
            Start = start;
            DecimalPlaces = decimalPlaces;
        }
    }
}
