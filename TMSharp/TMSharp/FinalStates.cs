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
        private List<string> final_states = new List<string>();

        public uint size { get { return (uint)(final_states != null ? final_states.Count() : 0); } }
        /////////////////////////////////////
        // Loads the final states from the given definition file and sets valid to true if it was successful, else false.
        /////////////////////////////////////
        public bool load(ref List<string> definition)
        {
            final_states = new List<string>();
            List<string> tmpValues;
            if (TMParse.ParseDefinitionValues(ref definition, null, out tmpValues))
            {
                foreach (string s in tmpValues)
                {
                    if (s.Length > 0)
                        final_states.Add(s);
                }
                return true;
            }
            return false;
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
        public bool is_element(string value) {
            return final_states.Contains(value);
        }

        /////////////////////////////////////
        // Returns true if value is found in the set of final states.
        /////////////////////////////////////
        public string element(uint index)
        {
	        if(index < final_states.Count()) {
		        return final_states[(int)index];
	        }
	        return "FINAL STATES INDEX ERROR";
        }

    }
}
