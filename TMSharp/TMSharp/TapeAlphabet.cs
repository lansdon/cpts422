using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class TapeAlphabet
    {
        private List<char> _alphabet = new List<char>();

        /////////////////////////////////////
        // Loads the tape alphabet from the given
        // definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public bool load(ref List<string> definition)
        {
            _alphabet = new List<char>();
            List<string> tmpValues;
            if (TMParse.ParseDefinitionValues(ref definition, "TRANSITION_FUNCTION:", out tmpValues) && tmpValues.Count() >= 1)
            {
                foreach (string s in tmpValues)
                {
                    if ((s.Length == 1) &&
                       (s[0] != '\\') &&
                       (s[0] != '<') &&
                       (s[0] != ']'))
                    {
                        _alphabet.Add(s[0]);
                    }
                    else
                    {
                        Console.WriteLine("Illegal Tape Alphabet Character.\n");
                        break;
                    }
                }
                return true;
            }
            return false;
        }
        

        /////////////////////////////////////
        // displays the current tape alphabet
        /////////////////////////////////////
        public void view() 
        {
	        string msg = "Tape alphabet  = {";
	        for(int i=0; i<_alphabet.Count(); ++i) 
            {
		        if(i>0) msg += ", ";
		        msg += _alphabet[i];
	        }
	        msg += "}\n";	
	        Console.WriteLine(msg);
        }

        /////////////////////////////////////
        // Returns true if value is found in the current tape alphabet.
        /////////////////////////////////////
        public bool is_element(char val) 
        {
            return _alphabet.Contains(val);
        }

    }
}
