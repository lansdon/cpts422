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
        private List<char> alphabet;
    
        /////////////////////////////////////
        // Loads the tape alphabet from the given definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public void load(ref StreamReader definition, ref bool valid) 
        {
	        bool next_keyword_found = false;
            string tempChar = null;
            while (valid && !next_keyword_found && (tempChar = definition.ReadLine()) != null)
            {
                if(tempChar.ToUpper() != "TAPE_ALPHABET:")  // prior to INPUT_ALPHABET: keyword
                {		
				    if((tempChar.Length == 1) &&
				        (tempChar[0] != '\\') &&
				        (tempChar[0] != '<') &&
				        (tempChar[0] != ']')) {
					    alphabet.Add(tempChar[0]);
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

            if (tempChar == null)
            {
	            Console.WriteLine("Error reading input alphabet.\n");
                valid = false;
            }
        }

        /////////////////////////////////////
        // displays the current tape alphabet
        /////////////////////////////////////
        void view() 
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
        bool is_element(char val) {
            return alphabet.Contains(val);
        }

        /////////////////////////////////////
        // Returns the character at the specified index
        /////////////////////////////////////
        char element(uint index) {
    	    if(index < alphabet.Count()) {
		        return alphabet[(int)index];
	        }
	        return '/';  // error
        }


    }
}
