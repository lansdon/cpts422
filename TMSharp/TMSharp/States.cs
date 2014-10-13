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
        public bool load(ref List<string> definition) 
        {
            states = new List<string>();
            List<string> tmpValues;
            if (TMParse.ParseDefinitionValues(ref definition, "INPUT_ALPHABET:", out tmpValues) && tmpValues.Count() >= 1)
            {
                
                foreach (string s in tmpValues)
                {
                    if(s.Length > 0)
                        states.Add(s);
                }
                return true;
            }
            return false;
        }

        /////////////////////////////////////
        // displays the current set of states
        /////////////////////////////////////
        public void view()
        {
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
        public bool is_element(string value) {
            var result = states.Where (str => String.Compare(str, value) == 0);
            return result != null;
        }

    }
}
