using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class RegexLib
    {
        private const string V = @"([A-z])*$";
        private const string X = @"^([A-z ])*$";

        public static string AlphaCaseInsensitive
        {
            get { return V; }
        }
        public static Regex AlphaUpperOnly
        {
            get { return new Regex(@"^([A-Z])*$"); }
        }
        public static Regex AlphaLowerOnly
        {
            get { return new Regex(@"^([a-z])*$"); }
        }
        public static string AlphaSpace
        {
            get
            {
                return X;
            }
        }
        public static Regex AlphaNumeric
        {
            get { return new Regex(@"^([A-z0-9])*$"); }
        }
        public static Regex AlphaNumericSpace
        {
            get { return new Regex(@"^([A-z0-9 ])*$"); }
        }


        //A-z alphabet
        //no alphabet(lower upper or both)
        //all alphabets are uppercase
        //all alphabets are lowercase
        //no numbers
        //alphanumerics
        //regex(regex expression)
        //allow special chars
        //no special chars
        //allow white space
        //no white space
        //no specific chars (list of chars)
        //allowed chars(list of chars)
        //email
        //no empty/white spaces
        //no null
    }
}
