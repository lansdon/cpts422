using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class TransitionFunction
    {
        private List<Transition> _transitions = new List<Transition>();


        ////////////////////////////////////////////
        // Loads the transitions from the definition_file. Sets valid to true on success, else false.
        ////////////////////////////////////////////
        public bool load(ref List<string> definition)
        {
            _transitions = new List<Transition>();
            List<string> results;
            if(TMParse.ParseDefinitionValues(ref definition, "INITIAL_STATE:", out results))
            {
                for (int i = 0; i < results.Count(); ++i )
                {
                    //                    List<string> splits = new List<string>(line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    if (results.Count() >= 5)
                    {
                        string source = results[i];
                        string current_char = results[++i];
                        string dest = results[++i];
                        string write = results[++i];
                        string move = results[++i];

                        if (
                           (current_char.Length == 1) &&
                           (write.Length == 1) &&
                           (move.Length == 1) &&
                           (move.ToUpper()[0] == 'L' || move.ToUpper()[0] == 'R'))
                        {
                            _transitions.Add(new Transition(source, current_char[0], dest, write[0], move[0]));
                        }
                        else
                        {
                            Console.WriteLine("Invalid transition found.\n");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error reading transition.\n");
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }


        
        ////////////////////////////////////////////
        //	Displays the loaded transitions
        ////////////////////////////////////////////
        public void view()  {
	        string msg = "\n----- Loaded Transitions: -----\n";
            foreach(Transition transition in _transitions)
            {
		        msg += "Transition(" +
		        transition.SourceState + ", " +
		        transition.ReadCharacter + ") = (" +
		        transition.DestinationState + ", " +
		        transition.WriteCharacter + ", " +
		        transition.MoveDirection + ")\n";
            }
	        Console.WriteLine(msg);
        }

        ////////////////////////////////////////////
        // Returns the number of stored transitions
        ////////////////////////////////////////////
        public int size()
        {
	        return (int)_transitions.Count();
        }

        ////////////////////////////////////////////
        // return source state
        ////////////////////////////////////////////
        public string source_state(uint index)
        {
	        return _transitions[(int)index].SourceState;
        }

        ////////////////////////////////////////////
        // return read char
        ////////////////////////////////////////////
        public char read_character(uint index)
        {
	        return _transitions[(int)index].ReadCharacter;
        }

        ////////////////////////////////////////////
        // return detination state
        ////////////////////////////////////////////
        public string destination_state(uint index)
        {
	        return _transitions[(int)index].DestinationState;
        }

        ////////////////////////////////////////////
        // write char of given transition
        ////////////////////////////////////////////
        public char write_character(uint index)
        {
	        return _transitions[(int)index].WriteCharacter;
        }

        ////////////////////////////////////////////
        // move direction of given transition
        ////////////////////////////////////////////
        public char move_direction(uint index)
        {
	        return _transitions[(int)index].MoveDirection;
        }

        ////////////////////////////////////////////
        // Find a matching saved transition
        ////////////////////////////////////////////
        public bool is_defined_transition(string source_state,
						           char read_char,
						           ref string destination_state,
						           ref char write_character,
						           ref char move_direction)
        {
	        for(int i=0; i<_transitions.Count(); ++i) {
		        if((_transitions[i].SourceState == source_state) &&
		           (_transitions[i].ReadCharacter == read_char)) {
			        destination_state = _transitions[i].DestinationState;
			        write_character = _transitions[i].WriteCharacter;
			        return true;
		        }
	        }
	        return false;
        }

        ////////////////////////////////////////////
        //returns true if the given state is a source state for a transition
        ////////////////////////////////////////////
        public bool is_source_state(string state)
        {
            foreach(Transition transition in _transitions)
            {
		        if(state == transition.SourceState) {
			        return true;
		        }
            }
	        return false;
        }

    }
}
