using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class InputAlphabet
    {
        // A vector containing the set of characters that make up the input alphabet
        private List<char> alphabet = new List<char>();

        public int size() { return alphabet != null ? alphabet.Count() : 0; }

        /////////////////////////////////////
        // Loads the tape alphabet from the given definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public bool load(ref List<string> definition) 
        {
            alphabet = new List<char>();
            string line = null;
            List<string> tmpValues;
            if (TMParse.ParseDefinitionValues(ref definition, "TAPE_ALPHABET:", out tmpValues))
            {
                foreach (string s in tmpValues)
                {
                    if ((s.Length == 1) &&
                        (s[0] != '\\') &&
                        (s[0] != '<') &&
                        (s[0] != ']'))
                    {
                        alphabet.Add(s[0]);
                    }
                    else
                    {
                        Console.WriteLine("Illegal Input Alphabet Character.\n");
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
	        string msg = "Input alphabet = {";
	        for(int i=0; i<alphabet.Count(); ++i) {
		        if(i>0) msg += ", ";
		        msg += alphabet[i];
	        }
	        msg += "}\n";	
	        Console.WriteLine(msg);
        }

        /////////////////////////////////////
        // Returns true if value is found in the current tape alphabet.
        /////////////////////////////////////
        public bool is_element(char val) {
            return alphabet.Contains(val);
        }

        /////////////////////////////////////
        // Returns the character at the specified index
        /////////////////////////////////////
        public char element(uint index)
        {
    	    if(index < alphabet.Count()) {
		        return alphabet[(int)index];
	        }
	        return '/';  // error
        }
    }
}
