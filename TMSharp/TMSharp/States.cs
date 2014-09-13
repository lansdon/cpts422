using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class States
    {
        private List<string> states = new List<string>();


        
        /////////////////////////////////////
        // Loads the states from the given definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public void load(ref StreamReader definition, ref bool valid) 
        {
	        bool next_keyword_found = false;
            string line = null;
            while (valid && !next_keyword_found && (line = definition.ReadLine()) != null)
            {
                if(line.ToUpper() != "TAPE_ALPHABET:")  // prior to INPUT_ALPHABET: keyword
                {		
				    if((line.Length == 1) &&
				        (line[0] != '\\') &&
				        (line[0] != '<') &&
				        (line[0] != ']')) {
					    states.Add(line);
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
	            Console.WriteLine("Error reading input alphabet.\n");
                valid = false;
            }
        }

        /////////////////////////////////////
        // displays the current set of states
        /////////////////////////////////////
        void view() {
	        string msg = "States = {";
	        for(int i=0; i<states.Count(); ++i) {
		        if(i>0) msg += ", ";
		        msg += states[i];
	        }
	        msg += "}\n";	
	        Console.WriteLine(msg);
        }

        /////////////////////////////////////
        // Returns true if value is found in the set of states.
        /////////////////////////////////////
        bool is_element(string value) {
            return states.Contains(value);
        }

    }
}
