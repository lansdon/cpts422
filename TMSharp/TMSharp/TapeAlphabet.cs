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
        private List<char> _alphabet;



        /////////////////////////////////////
        // Loads the tape alphabet from the given
        // definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public void load(ref StreamReader definition, ref bool valid)
        {
            bool next_keyword_found = false;
            string line = null;
            while (valid && !next_keyword_found && (line = definition.ReadLine()) != null)
            {
                if (line.ToUpper() != "TRANSITION_FUNCTION:")  // prior to INPUT_ALPHABET: keyword
                {
                    if ((line.Length == 1) &&
				       (line[0] != '\\') &&
				       (line[0] != '<') &&
				       (line[0] != ']'))
                    {
                        _alphabet.Add(line[0]);
                    }
                    else
                    {
                        Console.WriteLine("Illegal Tape Alphabet Character.\n");
                        valid = false;
                    }
                }
                else
                {
                    next_keyword_found = true;		// INPUT_ALPHABET: found.
                }
            }

            if (line == null)
            {
                Console.WriteLine("Error reading tape alphabet.\n");
                valid = false;
            }
        }
        

        /////////////////////////////////////
        // displays the current tape alphabet
        /////////////////////////////////////
        void view() {
	        string msg = "Tape alphabet  = {";
	        for(int i=0; i<_alphabet.Count(); ++i) {
		        if(i>0) msg += ", ";
		        msg += _alphabet[i];
	        }
	        msg += "}\n";	
	        Console.WriteLine(msg);
        }

        /////////////////////////////////////
        // Returns true if value is found in the current tape alphabet.
        /////////////////////////////////////
        bool is_element(char val) {
            return _alphabet.Contains(val);
        }

    }
}
