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
        private List<Transition> _transitions;


        ////////////////////////////////////////////
        // Loads the transitions from the definition_file. Sets valid to true on success, else false.
        ////////////////////////////////////////////
        public void load(ref StreamReader definition, ref bool valid)
        {
            bool next_keyword_found = false;
            string source = null;
            while (valid && !next_keyword_found && (source = definition.ReadLine()) != null)
            {
                string current_char, dest, write, move;

                if (source.ToUpper() == "INITIAL_STATE:")  // prior to INPUT_ALPHABET: keyword
                {
				    next_keyword_found = true;
				    break;
                }

                if (!(((current_char = definition.ReadLine()) != null) &&
                    ((dest = definition.ReadLine()) != null) && 
                    ((write = definition.ReadLine()) != null) &&
                    ((move = definition.ReadLine()) != null)) )
                {
                    valid = false;
                    Console.WriteLine("Error reading transition.\n");
                }
                else
                {
                    if ((current_char.ToUpper() != "INITIAL_STATE:") &&
                       (dest.ToUpper() != "INITIAL_STATE:") &&
                       (write.ToUpper() != "INITIAL_STATE:") &&
                       (move.ToUpper() != "INITIAL_STATE:") &&
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
                        valid = false;
                    }
                }
            }

            if (source == null)
            {
                Console.WriteLine("Error reading transition.\n");
                valid = false;
            }
        }


        
        ////////////////////////////////////////////
        //	Displays the loaded transitions
        ////////////////////////////////////////////
        void view()  {
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
        int size()  {
	        return (int)_transitions.Count();
        }

        ////////////////////////////////////////////
        // return source state
        ////////////////////////////////////////////
        string source_state(uint index)  {
	        return _transitions[(int)index].SourceState;
        }

        ////////////////////////////////////////////
        // return read char
        ////////////////////////////////////////////
        char read_character(uint index)  {
	        return _transitions[(int)index].ReadCharacter;
        }



        ////////////////////////////////////////////
        // return detination state
        ////////////////////////////////////////////
        string destination_state(uint index)  {
	        return _transitions[(int)index].DestinationState;
        }



        ////////////////////////////////////////////
        // write char of given transition
        ////////////////////////////////////////////
        char write_character(uint index)  {
	        return _transitions[(int)index].WriteCharacter;
        }

        ////////////////////////////////////////////
        // move direction of given transition
        ////////////////////////////////////////////
        char move_direction(uint index)  {
	        return _transitions[(int)index].MoveDirection;
        }


        ////////////////////////////////////////////
        // Find a matching saved transition
        ////////////////////////////////////////////
        bool is_defined_transition(string source_state,
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
        bool is_source_state(string state)  {
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
