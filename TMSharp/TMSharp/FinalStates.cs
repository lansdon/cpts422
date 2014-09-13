using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class FinalStates
    {
        private List<string> final_states;

        
        /////////////////////////////////////
        // Loads the final states from the given definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public void load(ref StreamReader definition, ref bool valid) {
	        bool next_keyword_found = false;
            string tempState = null;
            while (valid && !next_keyword_found && (tempState = definition.ReadLine()) != null)
            {
                if(tempState.ToUpper() != "INPUT_ALPHABET:") {		// prior to INPUT_ALPHABET: keyword
                    final_states.Add(tempState);
                } else {
                    next_keyword_found = true;		// INPUT_ALPHABET: found.
                }
            }

            if (tempState == null)
                valid = false;
            //while(definition.good() && !next_keyword_found && valid) {
            //    string temp_state;
            //    if(definition >> temp_state) {
            //        if(uppercase(temp_state) != "INPUT_ALPHABET:") {		// prior to INPUT_ALPHABET: keyword
            //            final_states.push_back(temp_state);
            //        } else {
            //            next_keyword_found = true;		// INPUT_ALPHABET: found.
            //        }
            //    } else {
            //        cout << "Error reading states from definition file.\n";
            //        valid = false;
            //    }
//	        } // end while loop
	
        }

        /////////////////////////////////////
        // displays the current set of final states
        /////////////////////////////////////
        public void view() {
	        string msg = "Final States = {";
	        for(int i=0; i<final_states.Count(); ++i) {
		        if(i>0) msg += ", ";
		        msg += final_states[i];
	        }
	        msg += "}\n";	
	        Console.WriteLine(msg);
        }

        /////////////////////////////////////
        // Returns true if value is found in the set of final states.
        /////////////////////////////////////
        bool is_element(string value) {
            return final_states.Contains(value);
        }

        /////////////////////////////////////
        // Returns true if value is found in the set of final states.
        /////////////////////////////////////
        string element(uint index) {
	        if(index < final_states.Count()) {
		        return final_states[(int)index];
	        }
	        return "FINAL STATES INDEX ERROR";
        }

    }
}
